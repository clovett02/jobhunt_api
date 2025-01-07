using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;
using JobHunt_API.Record;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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
    public static string SafeGetString(MySqlDataReader rdr, string columnname, int columnindex)
    {
        if(!rdr.IsDBNull(columnindex))
        {
            rdr.GetString(4);
            return rdr.GetString(columnname);
        }
            
        return string.Empty;
    }
    public static GetJobInfo ReturnJob(MySqlConnection con, string ID)
    {
        string sql = $"SELECT * FROM jobs WHERE ID = {ID};";
        using var cmd = new MySqlCommand(sql, con);
        using MySqlDataReader rdr = cmd.ExecuteReader();
        GetJobInfo result;
        while(rdr.Read())
        {
            result = new GetJobInfo(
                JobID: rdr.GetString("ID"),
                CompanyName: rdr.GetString("CompanyName"),
                JobTitle: rdr.GetString("JobTitle"),
                JobDescription: SafeGetString(rdr, "JobDescription", 4),
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
                return result;
        }
        return null;
        
    }
    public static GetJobInfo[] ReturnJobs(MySqlConnection con, string begindate, string enddate)
    {
        string sql = $"SELECT * FROM jobs WHERE ApplicationDate >= {begindate} AND ApplicationDate < {enddate};";
        using var cmd = new MySqlCommand(sql, con);
        List<GetJobInfo> records = new List<GetJobInfo>();
        
        using MySqlDataReader rdr = cmd.ExecuteReader();
        
        while(rdr.Read())
        {
            records.Add(new GetJobInfo(
                JobID: rdr.GetString("ID"),
                CompanyName: rdr.GetString("CompanyName"),
                JobTitle: rdr.GetString("JobTitle"),
                JobDescription: SafeGetString(rdr, "JobDescription", 4),
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
    public static GetJobInfo[] ReturnJobs(MySqlConnection con)
    {
        string sql = $"SELECT * FROM jobs WHERE ApplicationDate > DATE_SUB(NOW(), INTERVAL 1 YEAR);";
        using var cmd = new MySqlCommand(sql, con);
        List<GetJobInfo> records = new List<GetJobInfo>();

        using MySqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
            records.Add(new GetJobInfo(
                JobID: rdr.GetString("ID"),
                CompanyName: rdr.GetString("CompanyName"),
                JobTitle: rdr.GetString("JobTitle"),
                JobDescription: SafeGetString(rdr, "JobDescription", 4),
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

    public static void InsertJob(MySqlConnection con)
    {
        string sql = @"INSERT INTO jobs(CompanyName, JobTitle, URL, State, City, Remote, Hybrid, Onsite, 
        ApplicationDate, ApplicationTime) 
        
        VALUES(@CompanyName, @JobTitle, @URL, @State, @City, @Remote, @Hybrid, @Onsite, 
        @ApplicationDate, @ApplicationTime);";

        using MySqlCommand cmd = new MySqlCommand(sql, con);

        cmd.Parameters.AddWithValue("@CompanyName", Job.CompanyName);
        cmd.Parameters.AddWithValue("@JobTitle", Job.JobTitle);
        cmd.Parameters.AddWithValue("@JobDescription", Job.JobDescription);
        cmd.Parameters.AddWithValue("@URL", Job.JobDescription);
        cmd.Parameters.AddWithValue("@State", Job.State);
        cmd.Parameters.AddWithValue("@City", Job.City);
        cmd.Parameters.AddWithValue("@Remote", Job.Remote);
        cmd.Parameters.AddWithValue("@Hybrid", Job.Hybrid);
        cmd.Parameters.AddWithValue("@Onsite", Job.Onsite);
        cmd.Parameters.AddWithValue("@ApplicationDate", Job.ApplicationDate);
        cmd.Parameters.AddWithValue("@ApplicationTime", Job.ApplicationTime);

        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }

    public static void InsertSkills(MySqlConnection con)
    {
        //Skills required attribute will be looped thru and added seperately to the skills table
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
    }

    [EnableCors("MyPolicy")]
    [HttpGet("/api/job/byID/{jobID}")]
    public string GetJob(string jobID)
    {
        
        using var con = new MySqlConnection(cs);
        GetJobInfo result = ReturnJob(con, jobID);
        return JsonSerializer.Serialize(result);
        
    }

    [EnableCors("MyPolicy")]
    [HttpGet("/api/jobs/bydate/{begindate}/{enddate}")]
    public string GetJobs(string begindate, string enddate)
    {
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
    public String Post([FromBody] PostJobInfo Job)
    {
        using MySqlConnection con = new MySqlConnection(cs);
        con.Open();

        InsertJob(con);

        con.Close();
        return "201";
        
        

           

    }
}