namespace BackEndApi.DTOs
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        public string? NombreCompleto { get; set; }

        public int? IdDepartament { get; set; }

        public string? NombreDepartament { get; set; }

        public int? Sueldo { get; set; }

        public string? FechaContrato { get; set; }
    }
}
