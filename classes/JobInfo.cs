namespace JobHunt_API
{
    public class JobInfo
    {
        public JobInfo(string JobID, string CompanyName, string JobTitle, string JobDescription, string State,
        string City, bool Remote, bool Hybrid, bool Onsite, DateTime ApplicationDate,
        DateTime ApplicationTime, bool Responded, DateTime ResponseDate, DateTime ResponseTime, bool Denied)
        {
            this.JobID = JobID;
            this.CompanyName = CompanyName;
            this.JobTitle = JobTitle;
            this.JobDescription = JobDescription;
            this.State = State;
            this.City = City;
            this.Remote = Remote;
            this.Hybrid = Hybrid;
            this.Onsite = Onsite;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTime = ApplicationTime;
            this.Responded = Responded;
            this.ResponseDate = ResponseDate;
            this.ResponseTime = ResponseTime;
            this.Denied = Denied;
        }
        // public Guid Id { get; set; }
        public string JobID { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string State { get; set; }
        public string City { get; set;}
        public bool Remote { get; set; }
        public bool Hybrid { get; set; }
        public bool Onsite { get; set; }
        public string[] SkillsRequired { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime ApplicationTime { get; set; }
        public bool Responded { get; set; }
        public DateTime ResponseDate { get; set; }
        public DateTime ResponseTime { get; set; }
        public bool Denied { get; set; }

    }
}