using System;
using System.Collections.Generic;

namespace JobHunt_API.models;

public partial class Jobstoskill
{
    public int JobId { get; set; }

    public int SkillId { get; set; }

    public virtual Job Job { get; set; }

    public virtual Skill Skill { get; set; }
}
