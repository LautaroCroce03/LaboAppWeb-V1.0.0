using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Pedido: IPedidoDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Pedido> _logger;
        public Pedido(LaboAppWebV1Context laboAppWebV1Context, ILogger<Pedido> logger)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
        }

        public async Task<Int32> AgregarAsync(Models.Pedido pedido) 
        {
            try
            {
                await _laboAppWebV1Context.Pedidos.AddAsync(pedido);
                await _laboAppWebV1Context.SaveChangesAsync();

                if(pedido.IdPedido > 0)
                    return pedido.IdPedido;
                else 
                    return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
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
                                  IdEstadoPedido = p.IdEstado,
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

        public async Task UpdateAsync(Models.Pedido pedido)
        {
            try
            {
                _laboAppWebV1Context.Pedidos.Update(pedido);
                await _laboAppWebV1Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var pedido = _laboAppWebV1Context.Pedidos.Find(id);
                if (pedido != null)
                {
                    _laboAppWebV1Context.Pedidos.Remove(pedido);
                    await _laboAppWebV1Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync");
                throw;
            }
        }

        public async Task CambioEstadoAsync(Int32 idPedido, Int32 idEstado)
        {
            try
            {
                var _result = await _laboAppWebV1Context.Pedidos.FirstOrDefaultAsync(p=> p.IdPedido.Equals(idPedido));
                
                if (_result != null) 
                {
                    _result.IdEstado = idEstado;
                    await _laboAppWebV1Context.SaveChangesAsync();
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task CambioEstadoidClienteAsync(string idCliente, Int32 idEstado)
        {
            try
            {
                var _result = await _laboAppWebV1Context.Pedidos.FirstOrDefaultAsync(p => p.CodigoCliente.Equals(idCliente));

                if (_result != null)
                {
                    _result.IdEstado = idEstado;
                    await _laboAppWebV1Context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task<bool> ExisteAsync(Int32 idPedido)
        {
            try
            {
                return await _laboAppWebV1Context.Pedidos.AnyAsync(p => p.IdPedido.Equals(idPedido));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }
        public async Task<bool> ExisteIdClienteAsync(string idCliente)
        {
            try
            {
                return await _laboAppWebV1Context.Pedidos.AnyAsync(p => p.CodigoCliente.Equals(idCliente));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }
    }
}
