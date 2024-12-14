using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Rol: IRolDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Rol> _logger;
        public Rol(LaboAppWebV1Context laboAppWebV1Context, ILogger<Rol> logger)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
        }

        public async Task<Int32> AgregarAsync(Models.Role rol)
        {

            try
            {
                await _laboAppWebV1Context.Roles.AddAsync(rol);
                await _laboAppWebV1Context.SaveChangesAsync();

                return rol.IdRol;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<Models.Role>> ListadoAsync()
        {
            try
            {
                var result = await _laboAppWebV1Context.Roles.ToListAsync();

                if ((result != null) && (result.Count > 0))
                {
                    return result;
                }
                else
                {
                    return new List<Models.Role>();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task<bool> ExisteIdAsync(Int32 idRol)
        {
            try
            {
                return await _laboAppWebV1Context.Roles.AnyAsync(id => id.IdRol.Equals(idRol));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteIdAsync");
                throw;
            }
        }
    }
}
