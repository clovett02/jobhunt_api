using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using JobHunt_API.models;
using JobHunt_API.DTOs;

namespace JobHunt_API.Controller
{
    [ApiController]
    public class JobInfoController : ControllerBase
    {
        [HttpGet("/api/job/byID/{jobID}")]
        public async Task<ActionResult<string>> GetJob(int jobID)
        {
            JobDTO result;
            using (JobhuntContext db = new JobhuntContext()){
                result = new JobDTO(await db.Jobs.FindAsync(jobID));
            }
            Console.WriteLine(result);
            return Ok(JsonSerializer.Serialize(result));
            
        }

        [HttpDelete("/api/job/byID/{jobid}")]
        public async Task<ActionResult<string>> DeleteJob(int jobid)
        {
            using(JobhuntContext db = new JobhuntContext()){
                Job j = await db.Jobs.FindAsync(jobid);
                db.Jobs.Remove(j);
                await db.SaveChangesAsync();
            }
            return Ok("202");
        }
        
        [HttpPost("/api/job/addjob")]
        public async Task<ActionResult<string>> AddJob([FromBody] Job job)
        {
            using(JobhuntContext db = new JobhuntContext()){
                db.Add(job);
                await db.SaveChangesAsync();
            }

            return Ok("201");
        }

        [HttpGet("/api/jobs/bydate/{begindate}/{enddate}")]
        public ActionResult<string> GetJobs(DateTime begindate, DateTime enddate)
        {
            Job[] result;
            using (JobhuntContext db = new JobhuntContext()){
                result = db.Jobs
                    .Where(j => j.ApplicationDate > begindate && j.ApplicationDate < enddate).ToArray();
            }

            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpGet("/api/jobs/pastyear")]
        public ActionResult<string> GetJobs()
        {
            Job[] jobs;
            
            using (JobhuntContext db = new JobhuntContext()){
                DateTime yearago = DateTime.Now.AddYears(-1);
                jobs = db.Jobs
                .Where(j => j.ApplicationDate > yearago)
                .OrderByDescending(j => j.ApplicationDate).ToArray();
            }
            
            JobDTO[] result = new JobDTO[jobs.Length];

            for (int i = 0; i < jobs.Length; i++)
            {
                result[i] = new JobDTO(jobs[i]);
            }
            
            return Ok(JsonSerializer.Serialize(result));
        }

        /// <summary>
        /// *Unfinished. Will return a summary of jobs replied to
        /// in the past 14 Days. May use a different class/dto with
        /// properties for different data points.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/jobsummary")]
        public ActionResult<string> GetJobSummary()
        {
            

            string result = JsonSerializer.Serialize("");
            return Ok(result);
        }

        [HttpPut("/api/job/update")]
        public async Task<ActionResult<string>> UpdateJob([FromBody] Job job)
        {
            Console.WriteLine(job);
            using (JobhuntContext db = new JobhuntContext()){
                db.Jobs.Update(job);
                await db.SaveChangesAsync();
            }

            return Ok("202");
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
}

