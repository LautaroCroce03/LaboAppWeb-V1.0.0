using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Cliente : IClienteBusiness
    {
        private readonly ILogger<Cliente> _logger;
        private readonly ICliente _cliente;

        public Cliente(ILogger<Cliente> logger, ICliente cliente)
        {
            _logger = logger;
            _cliente = cliente;
        }

        public async Task<ClienteResponseDto> Demora(string codigoMesa, string idPedido)
        {
            try
            {
                return await _cliente.Demora(codigoMesa, idPedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Demora");
                throw;
            }
        }
    }
}
