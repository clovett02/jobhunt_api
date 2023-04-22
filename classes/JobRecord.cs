namespace JobHunt
{
    public record JobRecord
    (
        string CompanyName,
        string JobTitle,
        string JobDescription,
        string State, 
        string City,
        bool Remote,
        bool Hybrid,
        bool Onsite,
        List<string> SkillsRequired,
        DateOnly ApplicationDate,
        DateTime ApplicationTime,
        bool Responded,
        DateOnly ResponseDate,
        DateTime ResponseTime
    );
}