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
        public async Task<bool> ExisteAsync(Int32 codEmpleado)
        {
            try
            {
                return await _laboAppWebV1Context.Empleados.AnyAsync(e=> e.IdEmpleado.Equals(codEmpleado));

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Int32> ActualizarAsync(Models.Empleado empleado)
        {

            try
            {
                _laboAppWebV1Context.Empleados.Update(empleado);
                await _laboAppWebV1Context.SaveChangesAsync();

                return empleado.IdEmpleado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> ExisteLoginAsync(Models.Empleado empleado)
        {

            try
            {
                return await _laboAppWebV1Context.Empleados.AnyAsync(l => l.Password.Equals(empleado.Password) && 
                                    l.Usuario.Equals(empleado.Usuario));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Models.Empleado> EmpleadoLoginAsync(Models.Empleado empleado)
        {

            try
            {
                return await _laboAppWebV1Context.Empleados.FirstOrDefaultAsync(l => l.Password.Equals(empleado.Password) &&
                                    l.Usuario.Equals(empleado.Usuario));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
