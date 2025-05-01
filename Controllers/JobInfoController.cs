
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JobHunt_API.Record;

namespace JobHunt_API.Controller;

[ApiController]
public class JobInfoController : ControllerBase
{
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
    public ActionResult<string> AddJob([FromBody] PostJobInfo Job)
    {
        new JobInfo().InsertJob(Job);

        return Ok("201");
    }

    [HttpPost("/api/job/updatedescription")]
    public ActionResult<string> UpdateJobDescription([FromBody] UpdateJobDescription job)
    {
        new JobInfo().UpdateJobDescription(job.ID, job.JobDescription);
        return Ok("201");
    }
    [HttpPost("/api/job/updatelocation")]
    public ActionResult<string> UpdateLocation([FromBody] UpdateCityandState job)
    {
        new JobInfo().UpdateLocation(job.ID, job.City, job.State);
        return Ok("201");
    }
    [HttpPost("/api/job/updateremotehybridonsite")]
    public ActionResult<string> UpdateRemoteHybridOnsite([FromBody] UpdateRemoteHybridOnsite job)
    {
        new JobInfo().UpdateRemoteHybridOnsite(job.ID, job.Remote, job.Hybrid, job.Onsite);
        return Ok("201");
    }
    [HttpPost("/api/job/updateresponded")]
    public ActionResult<string> UpdateResponded([FromBody] UpdateResponded job)
    {
        new JobInfo().UpdateResponded(job.ID, job.Responded);
        return Ok("201");
    }
    [HttpPost("/api/job/updatesitefoundon")]
    public ActionResult<string> UpdateSiteFoundOn([FromBody] UpdateSiteFoundOn job)
    {
        new JobInfo().UpdateSiteFoundOn(job.ID, job.SiteFoundOn);
        return Ok("201");
    }
}