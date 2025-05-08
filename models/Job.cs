using System;
using System.Collections.Generic;

namespace JobHunt_API.models;

public partial class Job
{
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
