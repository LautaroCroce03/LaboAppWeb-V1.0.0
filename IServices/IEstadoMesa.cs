namespace LaboAppWebV1._0._0.IServices
{
    public interface IEstadoMesaDataAccess
    {
        Task<Int32> AgregarAsync(Models.EstadoMesa estadoMesa);
        Task<List<Models.EstadoMesa>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 idEstado);
    }
    public interface IEstadoMesaBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.EstadoMesaDto estadoMesa);
        Task<List<ModelsDto.EstadoMesaList>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 idEstado);
    }
}
