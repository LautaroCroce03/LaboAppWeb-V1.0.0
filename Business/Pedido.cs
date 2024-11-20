using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Pedido : IPedidoBusiness
    {
        private ILogger<Pedido> _logger;
        private readonly IPedidoDataAccess _pedidoDataAccess;
        private readonly IMapper _mapper;

        public Pedido(ILogger<Pedido> logger, IPedidoDataAccess pedidoDataAccess, IMapper mapper)
        {
            _logger = logger;
            _pedidoDataAccess = pedidoDataAccess;
            _mapper = mapper;
        }

        public async Task<bool> AgregarAsync(PedidoDto pedido, Int32 idComanda)
        {
            try
            {
                var fechaActual = DateTime.Now;

                //parametros al hacer el mapeo
                var _pedido = _mapper.Map<Models.Pedido>(pedido, opt =>
                {
                    opt.Items["fechaActual"] = fechaActual;
                    opt.Items["idComanda"] = idComanda;
                });


                return await _pedidoDataAccess.AgregarAsync(_pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<bool> AgregarAsync(List<PedidoDto> pedidos, Int32 idComanda)
        {
            try
            {
             
                var fechaActual = DateTime.Now; 

                //parametros al hacer el mapeo
                var _pedido = _mapper.Map<List<Models.Pedido>>(pedidos, opt =>
                {
                    opt.Items["fechaActual"] = fechaActual;
                    opt.Items["idComanda"] = idComanda;
                });
                return await _pedidoDataAccess.AgregarAsync(_pedido);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<PedidoListDto>> ListadoAsync(Int32 idComanda)
        {
            try
            {
                return await _pedidoDataAccess.ListadoAsync(idComanda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }
    }
}
