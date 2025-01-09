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
    /*public static void InsertSkills(MySqlConnection con, PostJobSkills Job)
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
    }*/

    [HttpGet("/api/job/byID/{jobID}")]
    public ActionResult<string> GetJob(string jobID)
    {
        GetJobInfo result = new JobInfo().ReturnJob(jobID);
        return Ok(JsonSerializer.Serialize(result));
        
    }

    [HttpGet("/api/jobs/bydate/{begindate}/{enddate}")]
    public ActionResult<string> GetJobs(string begindate, string enddate)
    {
        GetJobInfo[] jobs = new JobInfo().ReturnJobs(begindate, enddate);

        string result = JsonSerializer.Serialize(jobs);
        return Ok(result);
    }

    [HttpGet("/api/jobs/pastyear")]
    public ActionResult<string> GetJobs()
    {
        GetJobInfo[] jobs = new JobInfo().ReturnJobs();

        string result = JsonSerializer.Serialize(jobs);

        return Ok(result);
    }

    [HttpGet("/jobsummary")]
    public ActionResult<string> GetJobSummary()
    {
        

        string result = JsonSerializer.Serialize("");
        return Ok(result);
    }


    [HttpPost("/api/job/addjob")]
    public ActionResult<string> Post([FromBody] PostJobInfo Job)
    {
        new JobInfo().InsertJob(Job);

        return Ok("201");
    }
}