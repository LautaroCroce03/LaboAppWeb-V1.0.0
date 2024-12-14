using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Producto : IProductoBusiness
    {
        private ILogger<Producto> _logger;
        private readonly IProductoDataAccess _productoData;

        public Producto(ILogger<Producto> logger, IProductoDataAccess productoData)
        {
            _logger = logger;
            _productoData = productoData;
        }

        public async Task<int> AgregarAsync(ProductoDto productoDto)
        {
            try
            {
                var _prod = new Models.Producto
                {
                    Descripcion = productoDto.Descripcion,
                    Precio = productoDto.Precio,
                    Stock = productoDto.Stock,
                    IdSector = productoDto.IdSector
                };

                return await _productoData.AgregarAsync(_prod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<ProductoDto>> ListadoAsync()
        {
            try
            {
                List<ProductoDto> productoDtos = new List<ProductoDto>();

                var _list = await _productoData.ListadoAsync();

                if ((_list != null) && (_list.Count > 0))
                {
                    ProductoDto productoDto;
                    foreach (var item in _list)
                    {
                        productoDto = new ProductoDto
                        {
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Stock = item.Stock,
                            IdSector = item.IdSector,
                            IdProducto = item.IdProducto
                        };
                        productoDtos.Add(productoDto);
                    }

                    return productoDtos;
                }
                else
                {
                    return productoDtos;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(ProductoDto productoDto)
        {
            try
            {
                var productoExistente = await _productoData.ExisteAsync(productoDto.IdProducto);
                if (productoExistente == null) return false;

                productoExistente.Descripcion = productoDto.Descripcion;
                productoExistente.Precio = productoDto.Precio;
                productoExistente.Stock = productoDto.Stock;
                productoExistente.IdSector = productoDto.IdSector;

                return await _productoData.ActualizarAsync(productoExistente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ActualizarAsync");
                throw;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var producto = await _productoData.ExisteAsync(id);
                if (producto == null)
                {
                    _logger.LogWarning($"Producto con ID {id} no existe.");
                    return false;
                }

                return await _productoData.EliminarAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EliminarAsync");
                throw;
            }
        }
    }
}