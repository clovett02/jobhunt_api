using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;

namespace JobHunt_API.Record
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