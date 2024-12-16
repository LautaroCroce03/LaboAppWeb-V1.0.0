using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface IPedidoBusiness
    {
        Task<PedidoRespuestaDto> AgregarAsync(PedidoDto pedido);
        Task<bool> AgregarAsync(List<PedidoDto> pedidos, Int32 idComanda);
        Task<List<ModelsDto.PedidoListDto>> ListadoAsync(Int32 idComanda);
        Task UpdateAsync(ModelsDto.PedidoListDto pedido);
        Task DeleteAsync(int id);
        Task<bool> ExisteIdClienteAsync(string idCliente);
        Task<bool> ExisteAsync(Int32 idPedido);
        Task CambioEstadoidClienteAsync(string idCliente, Int32 idEstado);
        Task CambioEstadoAsync(Int32 idPedido, Int32 idEstado);
        Task<List<ModelsDto.ProductoPendienteDto>> GetProductosPendientesXSector(int sectorId);
        Task<ProductoVendidoDto> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin);
        Task<ProductoVendidoDto> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin);
        Task<PedidoDto?> PedidoById(int id);
    }
    public interface IPedidoDataAccess
    {
        Task<Int32> AgregarAsync(Models.Pedido pedido);
        Task<bool> AgregarAsync(List<Models.Pedido> pedido);
        Task<List<ModelsDto.PedidoListDto>> ListadoAsync(Int32 idComanda);
        Task UpdateAsync(Models.Pedido pedido);
        Task DeleteAsync(int id);
        Task CambioEstadoAsync(Int32 idPedido, Int32 idEstado);
        Task<bool> ExisteIdClienteAsync(string idCliente);
        Task<bool> ExisteAsync(Int32 idPedido);
        Task CambioEstadoidClienteAsync(string idCliente, Int32 idEstado);
        Task<List<ModelsDto.ProductoPendienteDto>> GetProductosPendientesXSector(int sectorId);
        Task<ProductoVendidoDto> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin);
        Task<ProductoVendidoDto> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin);
        Task<PedidoDto?> PedidoById(int id);
    }
}
