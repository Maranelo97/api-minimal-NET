using BackEndApi.Models;
using Microsoft.EntityFrameworkCore;

using BackEndApi.Services.Contrato;
using BackEndApi.Services.Implementacion;

using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Utilities;

using Microsoft.OpenApi.Models;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
});
builder.Services.AddDbContext<DbempleadoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("newPolicy", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

#region SwaggerConfig
    app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API");
});
#endregion


#region Peticiones
app.MapGet("/departamento/list", async (
    IDepartamentoService _departamentoServicio,
    IMapper _mapper
    ) => 
{
    List<Department> listaDepartment = await _departamentoServicio.GetList();
    List<DepartmentDTO> listaDeaprtmentDTO = _mapper.Map<List<DepartmentDTO>>(listaDepartment);

    if (listaDeaprtmentDTO.Count > 0)
  
        return Results.Ok(listaDeaprtmentDTO);
  
    else 
        return Results.NotFound();
});

app.MapGet("/empleado/list", async (
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) =>
{
    List<Empleado> listaEmpleado = await _empleadoServicio.GetList();
    List<EmpleadoDTO> listaEmpleadoDTO = _mapper.Map<List<EmpleadoDTO>>(listaEmpleado);

    if (listaEmpleadoDTO.Count > 0)

        return Results.Ok(listaEmpleadoDTO);

    else
        return Results.NotFound();
});

app.MapPost("/empleado/guardar", async (
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) =>
    {
        var _empleado = _mapper.Map<Empleado>(modelo);
        var _empleadoCreado = await _empleadoServicio.Add(_empleado);

        if(_empleadoCreado.IdEmpleado != 0)
            return Results.Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
        else 
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });
app.MapPut("/empleado/actualizar/{idEmpleado}", async (
    int idEmpleado,
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoServicio,
    IMapper _mapper
    ) =>
{
    var _encontrado = await _empleadoServicio.Get(idEmpleado);
    if(_encontrado is null) return Results.NotFound();
    
    var _empleado = _mapper.Map<Empleado>(modelo);

    _encontrado.NombreCompleto = _empleado.NombreCompleto;
    _encontrado.IdDepartament = _empleado.IdDepartament;
    _encontrado.Sueldo = _empleado.Sueldo;
    _encontrado.FechaContrato = _empleado.FechaContrato;

    var respuesta = await _empleadoServicio.Update(_encontrado);

    if(respuesta)
        return Results.Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});
app.MapDelete("/empleado/eliminar/{idEmpleado}", async (
    int idEmpleado,
    IEmpleadoService _empleadoServicio
    ) =>
{
    var _encontrado = await _empleadoServicio.Get(idEmpleado);
    if (_encontrado is null) return Results.NotFound();

    var respuesta = await _empleadoServicio.Delete(_encontrado);

    if (respuesta)
        return Results.Ok();
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

#endregion

app.UseCors("newPolicy");
app.Run();

