using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Rol: IRolDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public Rol(LaboAppWebV1Context laboAppWebV1Context)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<Int32> AgregarAsync(Models.Role rol)
        {

            try
            {
                await _laboAppWebV1Context.Roles.AddAsync(rol);
                await _laboAppWebV1Context.SaveChangesAsync();

                return rol.IdRol;
            }
            catch (Exception)
            {

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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
