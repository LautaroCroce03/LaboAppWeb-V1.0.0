using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;


namespace LaboAppWebV1._0._0.DataAccess
{
    public class Producto : IProductoDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;
        private readonly ILogger<Producto> _logger;
        public Producto(LaboAppWebV1Context laboAppWebV1Context, ILogger<Producto> logger)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
            _logger = logger;
        }

        public async Task<int> AgregarAsync(Models.Producto producto)
        {
            try
            {
                await _laboAppWebV1Context.Productos.AddAsync(producto);
                await _laboAppWebV1Context.SaveChangesAsync();

                return producto.IdProducto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<Models.Producto>> ListadoAsync()
        {
            try
            {
                var result = await _laboAppWebV1Context.Productos.ToListAsync();

                if ((result != null) && (result.Count > 0))
                {
                    return result;
                }
                else
                {
                    return new List<Models.Producto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }
        public async Task<bool> ActualizarAsync(Models.Producto producto)
        {
            try
            {
                _laboAppWebV1Context.Productos.Update(producto);
                await _laboAppWebV1Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ActualizarAsync");
                throw;
            }
        }

        public async Task<Models.Producto> ExisteAsync(int id)
        {
            try
            {
                return await _laboAppWebV1Context.Productos.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteAsync");
                throw;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var producto = await _laboAppWebV1Context.Productos.FindAsync(id);
                if (producto != null)
                {
                    _laboAppWebV1Context.Productos.Remove(producto);
                    await _laboAppWebV1Context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EliminarAsync");
                throw new Exception("Error al eliminar el producto", ex);
            }
        }
    }
}
