using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace JobHunt_API.Controller;

[ApiController]
public class JobInfoController : ControllerBase
{
    ///Connection String
    string cs = @"
            server=thor.mysql;
            userid=root;
            password=mchs2009;
            database=jobhunt
        ";

    [HttpGet("/jobinfo")]
    public string Get()
    {
        JobInfo[] ReturnAllJobs(MySqlConnection con)
        {
            string sql = "SELECT * FROM jobs";
            using var cmd = new MySqlCommand(sql, con);
            List<JobInfo> records = new List<JobInfo>();

            using MySqlDataReader rdr = cmd.ExecuteReader();

            int i = 0;
            while(rdr.Read())
            {
                records.Add(new JobInfo());
                records[i].CompanyName = rdr.GetString("CompanyName");
                records[i].JobTitle = rdr.GetString("JobTitle");
                // records[i].JobDescription = rdr.GetString("JobDescription");
                records[i].State = rdr.GetString("State");
                records[i].City = rdr.GetString("City");
                records[i].Remote = rdr.GetBoolean("Remote");
                records[i].Hybrid = rdr.GetBoolean("Hybrid");
                records[i].Onsite = rdr.GetBoolean("Onsite");
                records[i].ApplicationDate = rdr.GetDateTime("ApplicationDate");
                records[i].ApplicationTime = rdr.GetDateTime("ApplicationTime");
                records[i].Responded = rdr.GetBoolean("Responded");
                records[i].ResponseDate = rdr.GetDateTime("ResponseDate");
                records[i].ResponseTime = rdr.GetDateTime("ResponseTime");
                records[i].Denied = rdr.GetBoolean("Denied");

                i++;
            }
            JobInfo[] result = records.ToArray();
            return result;
        }

        using var con = new MySqlConnection(cs);
        con.Open();

        JobInfo[] jobs = ReturnAllJobs(con);

        for (int i = 0; i < jobs.Length; i++)
        {
            Console.WriteLine($@"Job Title: {jobs[i].JobTitle}
                Job Description: {jobs[i].JobDescription}
                Remote: {jobs[i].Remote}
                ApplicationDate: {jobs[i].ApplicationDate}
                ApplicationTime: {jobs[i].ApplicationTime}");
        }

        string result = JsonSerializer.Serialize(jobs);
        return result;
    }

    [HttpGet("/jobsummary")]
    public string GetJobSummary()
    {
        

        string result = JsonSerializer.Serialize("");
        return result;
    }

    [EnableCors("MyPolicy")]
    [HttpPost("/jobinfo")]
    public string Post([FromBody] JobInfoRecord Job)
    {
        void InsertJobs(MySqlConnection con)
        {
            string sql = @"INSERT INTO jobs(CompanyName, JobTitle, State, City, Remote, Hybrid, Onsite, 
            ApplicationDate, ApplicationTime, Responded, ResponseDate, ResponseTime, Denied) 
            
            VALUES(@CompanyName, @JobTitle, @State, @City, @Remote, @Hybrid, @Onsite, 
            @ApplicationDate, @ApplicationTime, @Responded, @ResponseDate, @ResponseTime, @Denied)";

            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@CompanyName", Job.CompanyName);
            cmd.Parameters.AddWithValue("@JobTitle", Job.JobTitle);
            // cmd.Parameters.AddWithValue("@JobDescription", Job.JobDescription);
            cmd.Parameters.AddWithValue("@State", Job.State);
            cmd.Parameters.AddWithValue("@City", Job.City);
            cmd.Parameters.AddWithValue("@Remote", Job.Remote);
            cmd.Parameters.AddWithValue("@Hybrid", Job.Hybrid);
            cmd.Parameters.AddWithValue("@Onsite", Job.Onsite);
            cmd.Parameters.AddWithValue("@ApplicationDate", Job.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTime", Job.ApplicationTime);
            // cmd.Parameters.AddWithValue("@Responded", Job.Responded);
            //cmd.Parameters.AddWithValue("@ResponseDate", Job.ResponseDate);
            //cmd.Parameters.AddWithValue("@ResponseTime", Job.ResponseTime);
            //cmd.Parameters.AddWithValue("@Denied", Job.Denied);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        /*void InsertSkills(MySqlConnection con)
        {
            string sql = $"INSERT IGNORE INTO skills(Name) VALUES(@Name{0})";
            for (int i = 1; i < Job.SkillsRequired.Count; i++)
            {
                sql+= $", (@Name{i})";
            }

            using var cmd = new MySqlCommand(sql, con);

            for (int i = 0; i < Job.SkillsRequired.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@Name{i}", Job.SkillsRequired[i]); 
            }
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }*/

        using var con = new MySqlConnection(cs);
        con.Open();

        InsertJobs(con);
        //InsertSkills(con);

        return "Job Added to DB";
        
        //Skills required attribute will be looped thru and added seperately to the skills table

           

    }
}