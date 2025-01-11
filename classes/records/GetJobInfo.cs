using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;

namespace JobHunt_API.Record
{
    public record GetJobInfo
    (
        string JobID,
        string CompanyName,
        string CompanyURL,
        string JobTitle,
        string JobDescription,
        string State, 
        string City,
        bool Remote,
        bool Hybrid,
        bool Onsite,
        DateOnly DatePosted,
        DateOnly ApplicationDate,
        TimeOnly ApplicationTime,
        String ApplicationDay,
        bool Responded,
        DateOnly ResponseDate,
        TimeOnly ResponseTime,
        String ResponseDay,
        bool Denied,
        bool EasyApply,
        string SiteFoundOn
    );
}