using System;
using System.Collections.Generic;

namespace softapiworking.mydb;

public partial class Employee
{
    public int Idemployee { get; set; }

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public DateTime Hired { get; set; }

    public string? Skils { get; set; }
}
