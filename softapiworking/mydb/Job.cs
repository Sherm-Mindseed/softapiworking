using System;
using System.Collections.Generic;

namespace softapiworking.mydb;

public partial class Job
{
    public int Idjobs { get; set; }

    public string? Jobname { get; set; }

    public virtual ICollection<Skill> SkillsIdskills { get; set; } = new List<Skill>();
}
