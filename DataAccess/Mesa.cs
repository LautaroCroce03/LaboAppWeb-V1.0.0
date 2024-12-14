using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Mesa: IMesaDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Mesa> _logger;
        public Mesa(LaboAppWebV1Context laboAppWebV1Context, ILogger<Mesa> logger)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
        }

        public async Task<Int32> AgregarAsync(Models.Mesa mesa) 
        {

            try
            {
                await _laboAppWebV1Context.Mesas.AddAsync(mesa);
                await _laboAppWebV1Context.SaveChangesAsync();

                return mesa.IdMesa;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<Models.Mesa>> ListadoAsync()
        {
            try
            {
                var result = await _laboAppWebV1Context.Mesas
                                    .AsNoTrackingWithIdentityResolution()
                                    .ToListAsync();

                if ((result != null) && (result.Count > 0)) 
                {
                    return result;
                }
                else 
                {
                    return new List<Models.Mesa>();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }
        public async Task<bool> ExisteAsync(Int32 idMesa)
        {
            try
            {
                return  await _laboAppWebV1Context.Mesas.AsNoTrackingWithIdentityResolution()
                                .AnyAsync(x=> x.IdMesa.Equals(idMesa));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteAsync");
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(Models.Mesa _mesa)
        {

            try
            {
                var mesa = await _laboAppWebV1Context.Mesas.FindAsync(_mesa.IdMesa);
                if (mesa != null)
                {
                    mesa.Nombre = _mesa.Nombre;
                    mesa.IdEstado = _mesa.IdEstado;

                    var rowsAffected = await _laboAppWebV1Context.SaveChangesAsync();
                    return rowsAffected > 0;
                }
                return false;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ActualizarAsync");
                throw;
            }
        }

        public async Task UpdateAsync(Models.Mesa mesa)
        {
            try
            {
                _laboAppWebV1Context.Mesas.Update(mesa);
                await _laboAppWebV1Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var mesa = _laboAppWebV1Context.Mesas.Find(id);
                if (mesa != null)
                {
                    _laboAppWebV1Context.Mesas.Remove(mesa);
                    await _laboAppWebV1Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync");
                throw;
            }
        }
    }
}
