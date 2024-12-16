using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class EstadoPedido: IEstadoPedidoDataAccess
    {
        private ILogger<EstadoPedido> _logger;
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public EstadoPedido(ILogger<EstadoPedido> logger, LaboAppWebV1Context laboAppWebV1Context)
        {
            _logger = logger;
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<List<Models.EstadoPedido>> ListadoAsync() 
        {
            try
            {
                var result = await _laboAppWebV1Context.EstadoPedidos.AsNoTrackingWithIdentityResolution().ToListAsync();

                if ((result != null) && (result.Count > 0))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

    }
}
