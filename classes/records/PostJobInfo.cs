namespace JobHunt_API
{
    public record PostJobInfo
    (
        string CompanyName,
        string JobTitle,
        string JobDescription,
        string URL,
        string State, 
        string City,
        bool Remote,
        bool Hybrid,
        bool Onsite,
        DateTime ApplicationDate,
        DateTime ApplicationTime
    );
}