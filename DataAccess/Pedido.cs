using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Pedido: IPedidoDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public Pedido(LaboAppWebV1Context laboAppWebV1Context)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<bool> AgregarAsync(Models.Pedido pedido) 
        {
            try
            {
                await _laboAppWebV1Context.Pedidos.AddAsync(pedido);
                await _laboAppWebV1Context.SaveChangesAsync();

                if(pedido.IdEstado > 0)
                    return true;
                else 
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> AgregarAsync(List<Models.Pedido> pedido)
        {
            try
            {
                await _laboAppWebV1Context.Pedidos.AddRangeAsync(pedido);
                var _result = await _laboAppWebV1Context.SaveChangesAsync();

                if (_result > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ModelsDto.PedidoListDto>> ListadoAsync(Int32 idComanda) 
        {
            try
            {
                var pedidos = from p in _laboAppWebV1Context.Pedidos
                              join prod in _laboAppWebV1Context.Productos
                              on p.IdProducto equals prod.IdProducto
                              join ep in _laboAppWebV1Context.EstadoPedidos
                              on p.IdEstado equals ep.IdEstado
                              where p.IdComanda == idComanda
                              select new ModelsDto.PedidoListDto()
                              {
                                  IdPedido = p.IdPedido,
                                  Cantidad = p.Cantidad,
                                  IdProducto = prod.IdProducto,
                                  ProductoDescripcion = prod.Descripcion,
                                  IdEstadoPedido = ep.IdEstado,
                                  EstadoDescripcion = ep.Descripcion,
                                  FechaCreacion = p.FechaCreacion,
                                  FechaFinalizacion = p.FechaFinalizacion
                              };

                return await pedidos.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
