using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class EstadoPedido : IEstadoPedidoBusiness
    {
        private ILogger<EstadoPedido> _logger;
        private readonly IEstadoPedidoDataAccess _estadoPedido;
        private readonly IMapper _mapper;

        public EstadoPedido(ILogger<EstadoPedido> logger, IEstadoPedidoDataAccess estadoPedido, IMapper mapper)
        {
            _logger = logger;
            _estadoPedido = estadoPedido;
            _mapper = mapper;
        }

        public async Task<List<EstadoPedidoDto>> ListadoAsync()
        {
            try
            {
                var result = await _estadoPedido.ListadoAsync();

                if ((result != null) && (result.Count > 0))
                {
                   return _mapper.Map<List<EstadoPedidoDto>>(result);
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
