using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Empleado: IEmpleadoDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public Empleado(LaboAppWebV1Context laboAppWebV1Context)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<Int32> AgregarAsync(Models.Empleado empleado)
        {

            try
            {
                await _laboAppWebV1Context.Empleados.AddAsync(empleado);
                await _laboAppWebV1Context.SaveChangesAsync();

                return empleado.IdEmpleado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Models.Empleado>> ListadoAsync()
        {
            try
            {
                var result = await _laboAppWebV1Context.Empleados.ToListAsync();

                if ((result != null) && (result.Count > 0))
                {
                    return result;
                }
                else
                {
                    return new List<Models.Empleado>();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
