using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Empleado : IEmpleadoDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Empleado> _logger;
        public Empleado(LaboAppWebV1Context laboAppWebV1Context, ILogger<Empleado> logger)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
        }

        public async Task<Int32> AgregarAsync(Models.Empleado empleado)
        {

            try
            {
                await _laboAppWebV1Context.Empleados.AddAsync(empleado);
                await _laboAppWebV1Context.SaveChangesAsync();

                return empleado.IdEmpleado;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<Models.Empleado>> ListadoAsync(bool estado)
        {
            try
            {
                var result = await _laboAppWebV1Context.Empleados.Include(e => e.IdRolNavigation).AsNoTrackingWithIdentityResolution().Where(x=> x.Estado.Value.Equals(estado))
                                .ToListAsync();

                if ((result != null) && (result.Count > 0))
                {
                    return result;
                }
                else
                {
                    return new List<Models.Empleado>();
                }

            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "ListadoAsync");
                throw;
            }
        }
        public async Task<bool> ExisteAsync(Int32 codEmpleado)
        {
            try
            {
                return await _laboAppWebV1Context.Empleados.AsNoTrackingWithIdentityResolution()
                            .AnyAsync(e => e.IdEmpleado.Equals(codEmpleado));

            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "ExisteAsync");
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
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "ActualizarAsync");
                throw;
            }
        }
        public async Task<bool> ExisteLoginAsync(Models.Empleado empleado)
        {

            try
            {
                return await _laboAppWebV1Context.Empleados.AsNoTrackingWithIdentityResolution()
                                    .AnyAsync(l => l.Password.Equals(empleado.Password) &&
                                    l.Usuario.Equals(empleado.Usuario));
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "ExisteLoginAsync");
                throw;
            }
        }
        public async Task<Models.Empleado> EmpleadoLoginAsync(Models.Empleado empleado)
        {

            try
            {
                return await _laboAppWebV1Context.Empleados
                                    .Include(e => e.IdRolNavigation) 
                                    .FirstOrDefaultAsync(e => e.Password == empleado.Password &&
                                                              e.Usuario == empleado.Usuario);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "EmpleadoLoginAsync");
                throw;
            }
        }

        public async Task<Models.Empleado> EmpleadoLoginAsync(Int32 idEmpleado)
        {

            try
            {
                return await _laboAppWebV1Context.Empleados.AsNoTrackingWithIdentityResolution()
                                                .FirstOrDefaultAsync(l => l.IdEmpleado.Equals(idEmpleado));
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "EmpleadoLoginAsync");
                throw;
            }
        }
        public async Task<bool> EliminarAsync(Int32 codEmpleado)
        {
            try
            {
                var empleado = await _laboAppWebV1Context.Empleados.FindAsync(codEmpleado);
                if (empleado != null)
                {
                    empleado.Estado = false;
                    //_laboAppWebV1Context.Empleados.Remove(empleado);
                    await _laboAppWebV1Context.SaveChangesAsync();

                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "EliminarAsync");
                throw new Exception("Error al eliminar el empleado", ex);

            }
        }
        public async Task UpdateAsync(Models.Empleado empleado)
        {
            try
            {
                _laboAppWebV1Context.Empleados.Update(empleado);
                await _laboAppWebV1Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "UpdateAsync");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var empleado = _laboAppWebV1Context.Empleados.Find(id);

                if (empleado != null)
                {
                    _laboAppWebV1Context.Empleados.Remove(empleado);
                    await _laboAppWebV1Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "DeleteAsync");
                throw new Exception("Error al eliminar el empleado", ex);

            }
        }
    }
}