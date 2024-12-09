using System.Collections.Generic;
using System.Threading.Tasks;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface IProductoDataAccess
    {
        Task<int> AgregarAsync(Models.Producto producto);
        Task<List<Models.Producto>> ListadoAsync();
        Task<bool> ActualizarAsync(Models.Producto producto);
        Task<Models.Producto> ExisteAsync(int id);
        Task<bool> EliminarAsync(int id);
    }

    public interface IProductoBusiness
    {
        Task<int> AgregarAsync(ProductoDto productoDto);
        Task<List<ProductoDto>> ListadoAsync();
        Task<bool> ActualizarAsync(ProductoDto productoDto);
        Task<bool> EliminarAsync(int id);
    }
}
