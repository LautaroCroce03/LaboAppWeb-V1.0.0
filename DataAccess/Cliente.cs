using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Cliente: ICliente
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Cliente> _logger;
        private readonly IMapper _mapper;

        public Cliente(LaboAppWebV1Context laboAppWebV1Context, ILogger<Cliente> logger, IMapper mapper)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ClienteResponseDto> Demora(string codigoMesa, string idPedido) 
        {
			try
			{
                _logger.LogInformation("Iniciando la búsqueda Getdedemora.");
                // Buscar la mesa por código
                var mesa = await _laboAppWebV1Context.Mesas
                    .Where(m => m.Codigo == codigoMesa)
                    .FirstOrDefaultAsync();

                if (mesa == null)
                {
                    _logger.LogWarning($"No se encontró una mesa con el código: {codigoMesa}.");
                    throw new KeyNotFoundException($"No se encontró una mesa con el código: {codigoMesa}.");
                }

                // Buscar el pedido por el ID
                var pedido = await _laboAppWebV1Context.Pedidos
                    .Where(p => p.CodigoCliente == idPedido)
                    .FirstOrDefaultAsync();

                if (pedido == null)
                {
                    _logger.LogWarning($"No se encontró el código cliente: {idPedido}.");
                    throw new KeyNotFoundException($"No se encontró el código cliente: {idPedido}.");
                }

                // Verificar que el pedido esté asociado con la mesa correcta
                var comanda = await _laboAppWebV1Context.Comandas
                    .Where(c => c.IdComandas == pedido.IdComanda && c.IdMesa == mesa.IdMesa)
                    .FirstOrDefaultAsync();

                if (comanda == null)
                {
                    _logger.LogWarning("El pedido no está asociado con la mesa proporcionada.");
                    throw new InvalidOperationException("El pedido no está asociado con la mesa proporcionada.");
                }


                // Mapear usando AutoMapper al DTO de cliente
                var resultadoDto = _mapper.Map<ClienteResponseDto>(pedido);

                return resultadoDto;
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, "Demora");

                throw;
			}
        }
    }
}
