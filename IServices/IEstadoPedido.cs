namespace LaboAppWebV1._0._0.IServices
{
    public interface IEstadoPedidoDataAccess
    {
        Task<List<Models.EstadoPedido>> ListadoAsync();
    }
    public interface IEstadoPedidoBusiness
    {
        Task<List<ModelsDto.EstadoPedidoDto>> ListadoAsync();
    }
}
