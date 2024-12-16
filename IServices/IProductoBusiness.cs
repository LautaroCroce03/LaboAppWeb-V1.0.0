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
        Task<Int32> DisponibleAsync(Int32 idProducto);
        Task<bool> ExisteProductoAsync(Int32 id);
        Task ActualizarStockAsync(int id, Int32 cantidad);
    }

    public interface IProductoBusiness
    {
        Task<int> AgregarAsync(ProductoDto productoDto);
        Task<List<ProductoDto>> ListadoAsync();
        Task<bool> ActualizarAsync(ProductoDto productoDto);
        Task<bool> EliminarAsync(int id);
        Task<Int32> Disponible(Int32 idProducto);
        Task<bool> ExisteAsync(Int32 idProducto);
        Task ActualizarStockAsync(int id, Int32 cantidad);
    }
}
