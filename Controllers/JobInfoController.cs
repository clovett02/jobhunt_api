
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JobHunt_API.Record;
using JobHunt_API.models;
using Microsoft.EntityFrameworkCore;

namespace JobHunt_API.Controller;

[ApiController]
public class JobInfoController : ControllerBase
{
    [HttpGet("/api/job/byID/{jobID}")]
    public ActionResult<string> GetJob(int jobID)
    {
        Job result;
        using (JobhuntContext db = new JobhuntContext()){
            result = db.Jobs.Find(jobID);
        }
        // GetJobInfo result = new JobInfo().ReturnJob(jobID);
        return Ok(JsonSerializer.Serialize(result));
        
    }

    [HttpDelete("/api/job/byID/{jobid}")]
    public ActionResult<string> DeleteJob(int jobid)
    {
        using(JobhuntContext db = new JobhuntContext()){
            Job j = db.Jobs.Find(jobid);
            db.Jobs.Remove(j);
            db.SaveChanges();
        }
        return Ok("200");
    }
    
    [HttpPost("/api/job/addjob")]
    public ActionResult<string> AddJob([FromBody] Job job)
    {
        using(JobhuntContext db = new JobhuntContext()){
            db.Add(job);
            db.SaveChanges();
        }

        return Ok("201");
    }

    [HttpGet("/api/jobs/bydate/{begindate}/{enddate}")]
    public ActionResult<string> GetJobs(string begindate, string enddate)
    {
        // using (JobhuntContext db = new JobhuntContext()){
        //     Job[] result1 = db.Jobs.FromSql<string>("");
        // }

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