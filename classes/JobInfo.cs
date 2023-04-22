namespace JobHunt
{
    public class JobInfo
    {
        // public Guid Id { get; set; }
        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public string? JobDescription { get; set; }
        public string? State { get; set; }
        public string? City { get; set;}
        public bool Remote { get; set; }
        public bool Hybrid { get; set; }
        public bool Onsite { get; set; }
        public string[]? SkillsRequired { get; set; }
        public DateOnly ApplicationDate { get; set; }
        public DateTime ApplicationTime { get; set; }
        public bool Responded { get; set; }
        public DateTime ResponseDate { get; set; }

    }
}