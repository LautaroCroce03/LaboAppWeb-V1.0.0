using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface IPedidoBusiness
    {
        Task<bool> AgregarAsync(ModelsDto.PedidoDto pedido, Int32 idComanda);
        Task<bool> AgregarAsync(List<PedidoDto> pedidos, Int32 idComanda);
        Task<List<ModelsDto.PedidoListDto>> ListadoAsync(Int32 idComanda);
        Task UpdateAsync(ModelsDto.PedidoListDto pedido);
        Task DeleteAsync(int id);
    }
    public interface IPedidoDataAccess
    {
        Task<bool> AgregarAsync(Models.Pedido pedido);
        Task<bool> AgregarAsync(List<Models.Pedido> pedido);
        Task<List<ModelsDto.PedidoListDto>> ListadoAsync(Int32 idComanda);
        Task UpdateAsync(Models.Pedido pedido);
        Task DeleteAsync(int id);
    }
}
