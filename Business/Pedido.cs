using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Pedido : IPedidoBusiness
    {
        private readonly IPedidoDataAccess _pedidoDataAccess;

        public Pedido(IPedidoDataAccess pedidoDataAccess)
        {
            _pedidoDataAccess = pedidoDataAccess;
        }

        public async Task<bool> AgregarAsync(PedidoDto pedido, Int32 idComanda)
        {
            try
            {
                DateTime _horaAlta = DateTime.Now;
                var _pedido = new Models.Pedido();
                _pedido.Cantidad = pedido.Cantidad;
                _pedido.IdProducto = pedido.IdProducto;
                _pedido.FechaCreacion = _horaAlta;
                _pedido.FechaFinalizacion = _horaAlta;
                _pedido.IdComanda = idComanda;
                _pedido.IdEstado = pedido.IdEstado;

                return await _pedidoDataAccess.AgregarAsync(_pedido);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AgregarAsync(List<PedidoDto> pedidos, Int32 idComanda)
        {
            try
            {
                DateTime _horaAlta = DateTime.Now;
                var _pedidos = new List<Models.Pedido>();
                Models.Pedido _pedido;
                foreach (var _ped in pedidos) 
                {
                    _pedido = new Models.Pedido();
                    _pedido.Cantidad = _ped.Cantidad;
                    _pedido.IdProducto = _ped.IdProducto;
                    _pedido.FechaCreacion = _horaAlta;
                    _pedido.FechaFinalizacion = _horaAlta;
                    _pedido.IdComanda = idComanda;
                    _pedido.IdEstado = _ped.IdEstado;

                    _pedidos.Add(_pedido);

                }
                return await _pedidoDataAccess.AgregarAsync(_pedidos);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PedidoListDto>> ListadoAsync(Int32 idComanda)
        {
            try
            {
                return await _pedidoDataAccess.ListadoAsync(idComanda);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
