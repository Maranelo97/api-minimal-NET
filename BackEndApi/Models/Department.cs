﻿using System;
using System.Collections.Generic;

namespace BackEndApi.Models;

public partial class Department
{
    public int IdDepartament { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();
}
