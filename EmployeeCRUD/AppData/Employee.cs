using System;
using System.Collections.Generic;

namespace EmployeeCRUD.AppData;

public partial class Employee
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Designation { get; set; }

    public string? Address { get; set; }

    public DateTime? RecordCreatedOn { get; set; }
}
