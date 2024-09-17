using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;


namespace LaboAppWebV1._0._0.DataAccess
{
    public class Producto : IProductoDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public Producto(LaboAppWebV1Context laboAppWebV1Context)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<int> AgregarAsync(Models.Producto producto)
        {
            try
            {
                await _laboAppWebV1Context.Productos.AddAsync(producto);
                await _laboAppWebV1Context.SaveChangesAsync();

                return producto.IdProducto;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
