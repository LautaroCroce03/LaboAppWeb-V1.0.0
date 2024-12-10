using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Comanda : IComandaBusiness
    {
        private ILogger<Comanda> _logger;
        private readonly IComandaDataAccess _comandaDataAccess;
        private readonly IPedidoBusiness _pedidoBusiness;
        private readonly IMapper _mapper;
        public Comanda(ILogger<Comanda> logger, IComandaDataAccess comandaDataAccess, IPedidoBusiness pedidoBusiness, IMapper mapper)
        {
            _logger = logger;
            _comandaDataAccess = comandaDataAccess;
            _pedidoBusiness = pedidoBusiness;
            _mapper = mapper;
        }

        public async Task<int> AgregarAsync(ComandaDto comanda)
        {
			try
			{
                var _comanda = new Models.Comanda();

                _comanda.NombreCliente = comanda.NombreCliente;
                _comanda.IdMesa = comanda.IdMesa;

                var codComanda = await _comandaDataAccess.AgregarAsync(_comanda);

                if (codComanda > 0)
                {
                    List<ModelsDto.PedidoDto> _pedidos = new List<ModelsDto.PedidoDto>();
                    ModelsDto.PedidoDto pedido = new ModelsDto.PedidoDto();
                    

                    foreach (var _pedido in comanda.Pedidos)
                    {
                        pedido.Cantidad = _pedido.Cantidad;
                        pedido.IdProducto = _pedido.IdProducto;
                        pedido.IdEstado = _pedido.IdEstado;

                        _pedidos.Add(pedido);
                    }

                    //Guardamos el pedido
                    var _result = await _pedidoBusiness.AgregarAsync(_pedidos, codComanda);

                    if (_result)
                    {
                        return codComanda;
                    }
                    else 
                    {
                        return 0;
                    }
                }
                else 
                {
                    return 0;
                }

            }
            catch (Exception ex)
			{
                _logger.LogError(ex, "AgregarAsync");
                throw;
			}
        }

        public async Task<ComandaDetalleDto> ListadoAsync(int idComanda)
        {
            try
            {
                var _comanda = await _comandaDataAccess.ListadoAsync(idComanda);
                if (_comanda != null)
                {
                    var _pedido = await _pedidoBusiness.ListadoAsync(idComanda);

                    if ((_pedido != null) && (_pedido.Count > 0))
                    {
                        _comanda.Pedidos = new List<PedidoListDto>();
                        _comanda.Pedidos.AddRange(_pedido);
                    }

                    return _comanda;
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

        public async Task<List<ModelsDto.ComandaDetalleDto>> ListadoAsync()
        {
            try
            {
                List<ModelsDto.ComandaDetalleDto> comandaDetalleDtos;
                var _comanda = await _comandaDataAccess.ListadoAsync();
                if ((_comanda == null) && (_comanda.Count.Equals(0)))
                    return null;

                comandaDetalleDtos = new List<ComandaDetalleDto>();
                ComandaDetalleDto _comandaDetalleDto;
                foreach (var _com in _comanda)
                {
                    _comandaDetalleDto = new ComandaDetalleDto();
                    _comandaDetalleDto = _com;

                    var _pedido = await _pedidoBusiness.ListadoAsync(_com.IdComandas);
                    if ((_pedido != null) && (_pedido.Count > 0))
                    {
                        _com.Pedidos = new List<PedidoListDto>();
                        _com.Pedidos.AddRange(_pedido);
                    }

                    comandaDetalleDtos.Add(_com);
                }

                return comandaDetalleDtos;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }
        public async Task<bool> EliminarAsync(int idComanda)
        {
            try
            {
                var comanda = await _comandaDataAccess.ListadoAsync(idComanda);
                if (comanda == null)
                {
                    _logger.LogWarning($"Comanda con ID {idComanda} no existe.");
                    return false;
                }

                return await _comandaDataAccess.EliminarAsync(idComanda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EliminarAsync");
                return false;
            }
        }
        public async Task UpdateAsync(ModelsDto.ComandaDto comanda)
        {
            try
            {
                var _comanda = _mapper.Map<Models.Comanda>(comanda);

                await _comandaDataAccess.UpdateAsync(_comanda);

            }
            catch (Exception)
            {

                throw;
            };
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _comandaDataAccess.DeleteAsync(id);
            }
            catch (Exception)
            {


                throw;
            }
        }

    }
}
