using JobHunt_API.models;

namespace JobHunt_API.DTOs{
    /// <summary>
    /// Provides a layer of abstraction for exchanging data with
    /// the database. If the model changes when the DB is 
        /// scaffolded again, code only needs to be changed here to match
        /// the new scaffolding.
    /// </summary>
    public class JobDTO{

        public JobDTO(Job j){
            this.Id = j.Id;
            this.CompanyName = j.CompanyName;
            this.CompanyUrl = j.CompanyUrl;
            this.JobTitle = j.JobTitle;
            this.JobDescription = j.JobDescription;
            this.State = j.State;
            this.City = j.City;
            this.Remote = j.Remote;
            this.Hybrid = j.Hybrid;
            this.Onsite = j.Onsite;
            this.DatePosted = j.DatePosted;
            this.ApplicationDate = j.ApplicationDate;
            this.ApplicationTime = j.ApplicationTime;
            this.ApplicationDay = j.ApplicationDate.DayOfWeek.ToString();
            this.Responded = j.Responded;
            this.ResponseDate = j.ResponseDate;
            this.ResponseTime = j.ResponseTime;
            // this.ResponseDay = j.ResponseDate.DayofWeek.ToString();
            this.Denied = j.Denied;
            this.EasyApply = j.EasyApply;
            this.SiteFoundOn = j.SiteFoundOn;
            this.JobUrl = j.JobUrl;
        }

        public void Update(JobDTO dto)
        {
            
        }

        /// <summary>
        /// Returns scaffolded model version of Job. This provides a layer
        /// of abstraction so that if the model changes when the DB is 
        /// scaffolded again, code only needs to be changed here to match
        /// the new scaffolding.
        /// </summary>
        /// <returns></returns>
        public Job ReturnJobContextJob(){
            Job j = new Job();

            j.Id = this.Id;
            j.CompanyName = this.CompanyName;
            j.CompanyUrl = this.CompanyUrl;
            j.JobTitle = this.JobTitle;
            j.JobDescription = this.JobDescription;
            j.State = this.State;
            j.City = this.City;
            j.Remote = this.Remote;
            j.Hybrid = this.Hybrid;
            j.Onsite = this.Onsite;
            j.DatePosted = this.DatePosted;
            j.ApplicationDate = this.ApplicationDate;
            j.ApplicationTime = this.ApplicationTime;
            // j.ApplicationDay = this.ApplicationDay;
            j.Responded = this.Responded;
            j.ResponseDate = this.ResponseDate;
            j.ResponseTime = this.ResponseTime;
            // j.ResponseDay = this.ResponseDay;
            j.Denied = this.Denied;
            j.EasyApply = this.EasyApply;
            j.SiteFoundOn = this.SiteFoundOn;
            j.JobUrl = this.JobUrl;

            return j;
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool Remote { get; set; }
        public bool Hybrid { get; set; }
        public bool Onsite { get; set; }
        public DateTime? DatePosted { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ApplicationTime { get; set; }
        public string ApplicationDay { get; set; }
        public bool? Responded { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ResponseTime { get; set; }
        // public string ResponseDay { get; set; }
        public bool? Denied { get; set; }
        public bool? EasyApply { get; set; }
        public string SiteFoundOn { get; set; }
        public string JobUrl { get; set; }

    }
}