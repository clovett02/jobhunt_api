using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Localization.Routing;
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
    static JobInfo ReturnJob(MySqlConnection con, string ID)
    {
        string sql = $"SELECT * FROM jobs WHERE ID = {ID})";
        using var cmd = new MySqlCommand(sql, con);
        using MySqlDataReader rdr = cmd.ExecuteReader();
        JobInfo record = new JobInfo(
            JobID: rdr.GetString("ID"),
            CompanyName: rdr.GetString("CompanyName"),
            JobTitle: rdr.GetString("JobTitle"),
            JobDescription: rdr.GetString("JobDescription"),
            State: rdr.GetString("State"),
            City:rdr.GetString("City"),
            Remote:rdr.GetBoolean("Remote"),
            Hybrid:rdr.GetBoolean("Hybrid"),
            Onsite:rdr.GetBoolean("Onsite"),
            ApplicationDate: rdr.GetDateTime("ApplicationDate"),
            ApplicationTime: rdr.GetDateTime("ApplicationTime"),
            Responded: rdr.GetBoolean("Responded"),
            ResponseDate: rdr.GetDateTime("ResponseDate"),
            ResponseTime: rdr.GetDateTime("ResponseTime"),
            Denied: rdr.GetBoolean("Denied")
            );
        return record;
    }
    static GetJobInfo[] ReturnJobs(MySqlConnection con, string begindate, string enddate)
    {
        string sql = $"SELECT * FROM jobs WHERE ApplicationDate >= {begindate} AND ApplicationDate < {enddate})";
        using var cmd = new MySqlCommand(sql, con);
        List<GetJobInfo> records = new List<GetJobInfo>();
        
        using MySqlDataReader rdr = cmd.ExecuteReader();
        
        while(rdr.Read())
        {
            records.Add(new GetJobInfo(
                JobID: rdr.GetString("ID"),
                CompanyName: rdr.GetString("CompanyName"),
                JobTitle: rdr.GetString("JobTitle"),
                JobDescription: rdr.GetString("JobDescription"),
                State: rdr.GetString("State"),
                City: rdr.GetString("City"),
                Remote: rdr.GetBoolean("Remote"),
                Hybrid: rdr.GetBoolean("Hybrid"),
                Onsite: rdr.GetBoolean("Onsite"),
                ApplicationDate: rdr.GetDateTime("ApplicationDate"),
                ApplicationTime: rdr.GetDateTime("ApplicationTime"),
                Responded: rdr.GetBoolean("Responded"),
                ResponseDate: rdr.GetDateTime("ResponseDate"),
                ResponseTime: rdr.GetDateTime("ResponseTime"),
                Denied: rdr.GetBoolean("Denied")
            ));
        }
        GetJobInfo[] result = records.ToArray();
        return result;
    }
    static GetJobInfo[] ReturnJobs(MySqlConnection con)
    {
        string sql = $"SELECT * FROM jobs";
        using var cmd = new MySqlCommand(sql, con);
        List<GetJobInfo> records = new List<GetJobInfo>();

        using MySqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
            records.Add(new GetJobInfo(
                JobID: rdr.GetString("ID"),
                CompanyName: rdr.GetString("CompanyName"),
                JobTitle: rdr.GetString("JobTitle"),
                JobDescription: rdr.GetString("JobDescription"),
                State: rdr.GetString("State"),
                City: rdr.GetString("City"),
                Remote: rdr.GetBoolean("Remote"),
                Hybrid: rdr.GetBoolean("Hybrid"),
                Onsite: rdr.GetBoolean("Onsite"),
                ApplicationDate: rdr.GetDateTime("ApplicationDate"),
                ApplicationTime: rdr.GetDateTime("ApplicationTime"),
                Responded: rdr.GetBoolean("Responded"),
                ResponseDate: rdr.GetDateTime("ResponseDate"),
                ResponseTime: rdr.GetDateTime("ResponseTime"),
                Denied: rdr.GetBoolean("Denied")
            ));
        }
        
        GetJobInfo[] result = records.ToArray();
        return result;
    }

    [EnableCors("MyPolicy")]
    [HttpGet("/api/job/{jobID}")]
    public string GetJob(string jobID)
    {
        
        string result = "";
        using var con = new MySqlConnection(cs);
        {
            
        }

        return result;
    }

    [EnableCors("MyPolicy")]
    [HttpGet("/api/jobs/{daterange}")]
    public string GetJobs(string daterange)
    {
        GetJobInfo[] ReturnJobs(MySqlConnection con)
        {
            string sql = "SELECT * FROM jobs WHERE ApplicationDate > DATE_SUB(NOW(), INTERVAL 1 YEAR)";
            using var cmd = new MySqlCommand(sql, con);
            List<GetJobInfo> records = new List<GetJobInfo>();

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                records.Add(new GetJobInfo(
                    CompanyName: rdr.GetString("CompanyName"),
                    JobTitle: rdr.GetString("JobTitle"),
                    JobDescription: rdr.GetString("JobDescription"),
                    State: rdr.GetString("State"),
                    City: rdr.GetString("City"),
                    Remote: rdr.GetBoolean("Remote"),
                    Hybrid: rdr.GetBoolean("Hybrid"),
                    Onsite: rdr.GetBoolean("Onsite"),
                    ApplicationDate: rdr.GetDateTime("ApplicationDate"),
                    ApplicationTime: rdr.GetDateTime("ApplicationTime"),
                    Responded: rdr.GetBoolean("Responded"),
                    ResponseDate: rdr.GetDateTime("ResponseDate"),
                    ResponseTime: rdr.GetDateTime("ResponseTime"),
                    Denied: rdr.GetBoolean("Denied")
                ));
            }
            
            GetJobInfo[] result = records.ToArray();
            return result;
        }

        using var con = new MySqlConnection(cs);
        con.Open();

        GetJobInfo[] jobs = ReturnJobs(con);

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
    [HttpPost("/api/addjob")]
    public String Post([FromBody] JobInfoRecord Job)
    {
        void InsertJobs(MySqlConnection con)
        {
            /*string sql = @"INSERT INTO jobs(CompanyName, JobTitle, State, City, Remote, Hybrid, Onsite, 
            ApplicationDate, ApplicationTime, Responded, ResponseDate, ResponseTime, Denied) 
            
            VALUES(@CompanyName, @JobTitle, @State, @City, @Remote, @Hybrid, @Onsite, 
            @ApplicationDate, @ApplicationTime, @Responded, @ResponseDate, @ResponseTime, @Denied)";*/

            string sql = @"INSERT INTO jobs(CompanyName, JobTitle, State, City, Remote, Hybrid, Onsite, 
            ApplicationDate, ApplicationTime) 
            
            VALUES(@CompanyName, @JobTitle, @State, @City, @Remote, @Hybrid, @Onsite, 
            @ApplicationDate, @ApplicationTime)";

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
            /*cmd.Parameters.AddWithValue("@Responded", Job.Responded);
            cmd.Parameters.AddWithValue("@ResponseDate", Job.ResponseDate);
            cmd.Parameters.AddWithValue("@ResponseTime", Job.ResponseTime);
            cmd.Parameters.AddWithValue("@Denied", Job.Denied);*/

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

        return "201";
        
        //Skills required attribute will be looped thru and added seperately to the skills table

           

    }
}