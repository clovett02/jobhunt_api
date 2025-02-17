namespace JobHunt_API
{
    public record UpdateJobDescription
    (
        string ID,
        string JobDescription
    );
    public record UpdateCityandState
    (
        string ID,
        string City,
        string State
    );
    public record UpdateRemoteHybridOnsite
    (
        string ID,
        bool Remote,
        bool Hybrid,
        bool Onsite
    );
    public record UpdateResponded
    (
        string ID,
        bool Responded
    );
}