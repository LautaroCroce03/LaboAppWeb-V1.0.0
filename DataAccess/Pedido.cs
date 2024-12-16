using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Pedido: IPedidoDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Pedido> _logger;
        private readonly IMapper _mapper;

        public Pedido(LaboAppWebV1Context laboAppWebV1Context, ILogger<Pedido> logger, IMapper mapper)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
            _mapper = mapper;
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

        public async Task<List<ModelsDto.ProductoPendienteDto>> GetProductosPendientesXSector(int sectorId)
        {
            _logger.LogInformation("Iniciando la búsqueda del productos pendientes por sector.");

            // Agrupa los productos pendientes por su ID y calcula la cantidad pendiente
            var productosPendientes = await _laboAppWebV1Context.Productos
                                  .Join(_laboAppWebV1Context.Pedidos,//join entre tablas
                                        producto => producto.IdProducto,
                                        pedido => pedido.IdPedido,
                                        (producto, pedido) => new { producto, pedido })
                                  .Where(p => p.producto.IdSector == sectorId
                                         && p.pedido.IdEstado == 1) // 1 = Estado Pendiente
                                  .GroupBy(p => new { p.producto.IdProducto, p.producto.Descripcion }) // Agrupamos por ProductoId y Nombre
                                  .Select(g => new
                                  {
                                      ProductoId = g.Key.IdProducto,
                                      Nombre = g.Key.Descripcion,
                                      CantidadPendiente = g.Sum(p => p.pedido.Cantidad) // Sumamos la cantidad pendiente de cada producto
                                  })
                                  .ToListAsync();


            // Mapea los productos pendientes a ProductoVendidoDto
            var productosDto = productosPendientes.Select(p => new ModelsDto.ProductoPendienteDto
            {
                NombreDesc = p.Nombre,
                CantidadPendiente = p.CantidadPendiente // Asigna manualmente la cantidad pendiente

            }).ToList();

            return productosDto;
        }

        public async Task<ProductoVendidoDto> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            _logger.LogInformation("Iniciando la búsqueda del producto menos vendido.");

            // Filtro por fechas, si se proporcionan
            var pedidosFiltrados = _laboAppWebV1Context.Pedidos.AsQueryable();

            if (fechaInicio.HasValue)
            {
                pedidosFiltrados = pedidosFiltrados.Where(p => p.FechaCreacion.Date >= fechaInicio.Value.Date); // Filtro por fecha de inicio
            }

            if (fechaFin.HasValue)
            {
                pedidosFiltrados = pedidosFiltrados.Where(p => p.FechaCreacion.Date <= fechaFin.Value.Date); // Filtro por fecha de fin
            }

            // Agrupa los pedidos filtrados por el ID del producto y calcula la cantidad total vendida por producto
            var productoMenosVendido = await pedidosFiltrados
                .GroupBy(p => p.IdProducto)
                .Select(g => new  // Creamos un nuevo objeto con el ID del producto y la cantidad vendida
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada pedido
                })
                .OrderBy(g => g.CantidadVendida) // Ordenamos por la cantidad vendida (ascendente) para obtener el menos vendido
                .FirstOrDefaultAsync(); // Obtenemos el primer resultado menos vendido o null si no hay datos

            // Si no se encuentra ningún producto vendido, devuelve null
            if (productoMenosVendido == null)
            {
                _logger.LogWarning("No se encontró ningún producto vendido.");
                return null;
            }

            // Busca el producto en la base de datos utilizando el ID obtenido
            var producto = await _laboAppWebV1Context.Productos.FindAsync(productoMenosVendido.ProductoId);

            // Si el producto no existe, devuelve null
            if (producto == null)
            {
                _logger.LogWarning("El producto no existe en la base de datos.");
                return null;
            }

            _logger.LogInformation($"Producto menos vendido encontrado: ID {productoMenosVendido.ProductoId} - Cantidad Vendida {productoMenosVendido.CantidadVendida}");

            // Mapear Producto a ProductoVendidoDto para devolverlo al controlador
            var productoMenosVendidoDto = _mapper.Map<ProductoVendidoDto>(producto);

            // Añadir manualmente la cantidad vendida ya que no está en la entidad producto
            productoMenosVendidoDto.CantidadVendida = productoMenosVendido.CantidadVendida;

            return productoMenosVendidoDto;
        }

        public async Task<ProductoVendidoDto> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            _logger.LogInformation("Iniciando la búsqueda del producto más vendido.");

            // Agrupamos los pedidos por el ID del producto y calculamos la cantidad total vendida por producto
            var productoMasVendido = await _laboAppWebV1Context.Pedidos
                .Where(p =>
                    (!fechaInicio.HasValue || p.FechaCreacion.Date >= fechaInicio.Value.Date)  // Filtramos por fecha de inicio sin hora
                    && (!fechaFin.HasValue || p.FechaCreacion.Date <= fechaFin.Value.Date) // Filtramos por fecha de fin sin hora
                )
                .GroupBy(p => p.IdProducto)
                .Select(g => new  // Creamos un nuevo objeto con el ID del producto y la cantidad vendida
                {
                    ProductoId = g.Key,
                    CantidadVendida = g.Sum(p => p.Cantidad) // Sumamos la cantidad de cada detalle de pedido
                })
                .OrderByDescending(g => g.CantidadVendida) // Ordenamos de forma descendente por la cantidad vendida
                .FirstOrDefaultAsync(); // Obtenemos el producto más vendido o null si no hay datos

            // Si no se encuentra ningún producto vendido, devuelve un mensaje de error
            if (productoMasVendido == null)
            {
                _logger.LogWarning("No se encontró ningún producto vendido.");
                return null; // Si no se encuentra un producto más vendido, retorna null
            }

            // Busca el producto en la BBDD utilizando el ID obtenido
            var producto = await _laboAppWebV1Context.Productos.FindAsync(productoMasVendido.ProductoId);

            if (producto == null)
            {
                _logger.LogWarning("El producto no existe en la BBDD");
                return null; // Si el producto no existe, retorna null
            }

            _logger.LogInformation($"Producto más vendido encontrado: ID {productoMasVendido.ProductoId} - Cantidad Vendida {productoMasVendido.CantidadVendida}");

            // Mapear Producto a ProductoVendidoDto para devolverlo al controller
            var productoMasVendidoDto = _mapper.Map<ProductoVendidoDto>(producto);

            // Añadir manualmente la cantidad vendida ya que no está en la entidad producto
            productoMasVendidoDto.CantidadVendida = productoMasVendido.CantidadVendida;

            return productoMasVendidoDto;
        }


    }
}
