namespace JobHunt_API
{
    public record GetJobInfo
    (
        string JobID,
        string CompanyName,
        string JobTitle,
        string JobDescription,
        string State, 
        string City,
        bool Remote,
        bool Hybrid,
        bool Onsite,
        DateTime ApplicationDate,
        DateTime ApplicationTime,
        bool Responded,
        DateTime ResponseDate,
        DateTime ResponseTime,
        bool Denied
    );
}