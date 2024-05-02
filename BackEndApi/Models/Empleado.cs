using System;
using System.Collections.Generic;

namespace BackEndApi.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? NombreCompleto { get; set; }

    public int? IdDepartament { get; set; }

    public int? Sueldo { get; set; }

    public DateTime? FechaContrato { get; set; }

    public virtual Department? IdDepartamentNavigation { get; set; }
}
