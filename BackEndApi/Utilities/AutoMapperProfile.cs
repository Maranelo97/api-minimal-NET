using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Models;
using System.Globalization;

namespace BackEndApi.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Department
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            #endregion

            #region Empleado
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(destino => destino.NombreDepartament, opt => opt.MapFrom(origen => origen.IdDepartamentNavigation.Nombre))
                .ForMember(destino => destino.FechaContrato, opt => opt.MapFrom(origen => origen.FechaContrato.Value.ToString("dd/MM/yyyy")));

            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(destino =>
                    destino.IdDepartamentNavigation,
                    opt => opt.Ignore()
                    )
                   .ForMember(destino =>
                    destino.FechaContrato,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato,"dd/MM/yyyy",CultureInfo.InvariantCulture)
                    ));
            #endregion
        }
    }
}
