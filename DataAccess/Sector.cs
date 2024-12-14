using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Sector: ISectorDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Sector> _logger;
        public Sector(LaboAppWebV1Context laboAppWebV1Context, ILogger<Sector> logger)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
        }

        public async Task<Int32> AgregarAsync(Models.Sectore sectore)
        {

            try
            {
                await _laboAppWebV1Context.Sectores.AddAsync(sectore);
                await _laboAppWebV1Context.SaveChangesAsync();

                return sectore.IdSector;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<Models.Sectore>> ListadoAsync()
        {
            try
            {
                var result = await _laboAppWebV1Context.Sectores.ToListAsync();

                if ((result != null) && (result.Count > 0))
                {
                    return result;
                }
                else
                {
                    return new List<Models.Sectore>();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task<bool> ExisteId(Int32 idSector) 
        {
            try
            {
                return await _laboAppWebV1Context.Sectores.AnyAsync(id=> id.IdSector.Equals(idSector));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteId");
                throw;
            }
        } 
    }
}
