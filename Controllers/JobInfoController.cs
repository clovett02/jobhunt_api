using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace JobHunt.Controller;

[ApiController]
public class JobInfoController : ControllerBase
{
    [HttpGet("/jobinfo")]
    public string Get()
    {
        return "Hello";
    }

    [HttpPost("/jobinfo")]
    public void Post([FromBody] JobRecord Job)
    {
        void InsertJobs(MySqlConnection con)
        {
            var sql = @"INSERT INTO jobs(CompanyName, JobTitle, JobDescription, State, City, Remote, Hybrid, Onsite, 
            ApplicationDate, ApplicationTime, Responded, ResponseDate, ResponseTime) 
            
            VALUES(@CompanyName, @JobTitle, @JobDescription, @State, @City, @Remote, @Hybrid, @Onsite, 
            @ApplicationDate, @ApplicationTime, @Responded, @ResponseDate, @ResponseTime)";

            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@CompanyName", Job.CompanyName);
            cmd.Parameters.AddWithValue("@JobTitle", Job.JobTitle);
            cmd.Parameters.AddWithValue("@JobDescription", Job.JobDescription);
            cmd.Parameters.AddWithValue("@State", Job.State);
            cmd.Parameters.AddWithValue("@City", Job.City);
            cmd.Parameters.AddWithValue("@Remote", Job.Remote);
            cmd.Parameters.AddWithValue("@Hybrid", Job.Hybrid);
            cmd.Parameters.AddWithValue("@Onsite", Job.Onsite);
            cmd.Parameters.AddWithValue("@ApplicationDate", Job.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTime", Job.ApplicationTime);
            cmd.Parameters.AddWithValue("@Responded", Job.Responded);
            cmd.Parameters.AddWithValue("@ResponseDate", Job.ResponseDate);
            cmd.Parameters.AddWithValue("@ResponseTime", Job.ResponseTime);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        void InsertSkills(MySqlConnection con)
        {
            
        }

        string cs = @"
            server=hulk.mysql;
            userid=root;
            password=mchs2009;
            database=jobhunt
        ";

        using var con = new MySqlConnection(cs);
        con.Open();

        InsertJobs(con);
        InsertSkills(con);

        

        //Skills required attribute will be looped thru and added seperately to the skills table

           

    }
}