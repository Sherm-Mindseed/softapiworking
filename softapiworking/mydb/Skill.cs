using System;
using System.Collections.Generic;

namespace softapiworking.mydb;

public partial class Skill
{
    public int Idskills { get; set; }

    public string Name { get; set; } = null!;

    public string? Desc { get; set; }

    public DateTime Creation { get; set; }

    public virtual ICollection<Employee> EmployeeIdemployees { get; set; } = new List<Employee>();

    public virtual ICollection<Job> JobsIdjobs { get; set; } = new List<Job>();
}
