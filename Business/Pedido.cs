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
        private readonly IProductoBusiness _productoBusiness;
        private readonly IGenerar _generar;

        public Pedido(ILogger<Pedido> logger, IPedidoDataAccess pedidoDataAccess, IMapper mapper, IProductoBusiness productoBusiness, IGenerar generar)
        {
            _logger = logger;
            _pedidoDataAccess = pedidoDataAccess;
            _mapper = mapper;
            _productoBusiness = productoBusiness;
            _generar = generar;
        }

        public async Task<PedidoRespuestaDto> AgregarAsync(PedidoDto pedido)
        {
            try
            {

                if (!await _productoBusiness.ExisteAsync(pedido.IdProducto)) 
                {
                    throw new Exception("No existe el producto ingresado.");
                }

                if (!(pedido.Cantidad <= await _productoBusiness.Disponible(pedido.IdProducto)))
                {
                    throw new Exception("Sin stock disponible.");
                }
                var fechaActual = DateTime.Now;

                //parametros al hacer el mapeo
                var _pedido = _mapper.Map<Models.Pedido>(pedido, opt =>
                {
                    opt.Items["fechaActual"] = fechaActual;
                    opt.Items["idComanda"] = pedido.IdComanda;
                });
                _pedido.IdEstado = 1;
                _pedido.CodigoCliente = _generar.Codigo();
                Int32 idPedido = await _pedidoDataAccess.AgregarAsync(_pedido);
                if (idPedido > 0)
                {
                    await _productoBusiness.ActualizarStockAsync(pedido.IdProducto, pedido.Cantidad);
                    return new PedidoRespuestaDto() { CodigoCliente = _pedido.CodigoCliente, IdPedido = idPedido };
                }
                else 
                {
                    return null;
                }
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

        public async Task UpdateAsync(PedidoListDto pedido)
        {
            try
            {
                var _pedido = _mapper.Map<Models.Pedido>(pedido);
                await _pedidoDataAccess.UpdateAsync(_pedido);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _pedidoDataAccess.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<bool> ExisteIdClienteAsync(string idCliente)
        {
            try
            {
                return await _pedidoDataAccess.ExisteIdClienteAsync(idCliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ExisteAsync(int idPedido)
        {
            try
            {
                return await _pedidoDataAccess.ExisteAsync(idPedido);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CambioEstadoidClienteAsync(string idCliente, int idEstado)
        {
            try
            {
                await _pedidoDataAccess.CambioEstadoidClienteAsync(idCliente, idEstado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CambioEstadoAsync(int idPedido, int idEstado)
        {
            try
            {
                await _pedidoDataAccess.CambioEstadoAsync(idPedido, idEstado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ProductoPendienteDto>> GetProductosPendientesXSector(int sectorId)
        {
            try
            {
                return await _pedidoDataAccess.GetProductosPendientesXSector(sectorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductoVendidoDto> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                return await _pedidoDataAccess.GetProductoMenosVendido(fechaInicio, fechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductoVendidoDto> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                return await _pedidoDataAccess.GetProductoMasVendido(fechaInicio, fechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
