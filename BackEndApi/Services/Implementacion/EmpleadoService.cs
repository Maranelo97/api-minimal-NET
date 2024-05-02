using Microsoft.EntityFrameworkCore;
using BackEndApi.Models;
using BackEndApi.Services.Contrato;

namespace BackEndApi.Services.Implementacion
{
    public class EmpleadoService: IEmpleadoService
    {
        private DbempleadoContext _dbContext;


         public EmpleadoService(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Empleado>> GetList()
        {
            try
            {

                List<Empleado> list = new List<Empleado>();
                list = await _dbContext.Empleados.Include(dpt => dpt.IdDepartamentNavigation).ToListAsync();
            
                return list;

            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }

        public async Task<Empleado> Get(int idEmpleado)
        {
            try
            {
                Empleado? encontrado = new Empleado();

                encontrado = await _dbContext.Empleados.Include(dpt => dpt.IdDepartamentNavigation)
                    .Where(e => e.IdEmpleado == idEmpleado).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Empleado> Add(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Empleado modelo)
        {
            try
            {
                _dbContext.Empleados.Remove(modelo);
                    await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
