using Microsoft.EntityFrameworkCore;
using BackEndApi.Models;
using BackEndApi.Services.Contrato;

namespace BackEndApi.Services.Implementacion
{
    public class DepartamentoService : IDepartamentoService
    {
        private DbempleadoContext _dbContext;

        public DepartamentoService(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Department>> GetList()
        {
            try {
                List<Department> lista = new List<Department>();
                lista = await _dbContext.Departments.ToListAsync();
                return lista;
            }
            catch (Exception ex) {
                throw ex;
            }

        }
    }
}
