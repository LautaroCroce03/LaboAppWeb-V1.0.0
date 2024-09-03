namespace LaboAppWebV1._0._0.IServices
{
    public interface IEstadoMesaDataAccess
    {
        Task<Int32> AgregarAsync(Models.EstadoMesa estadoMesa);
        Task<List<Models.EstadoMesa>> ListadoAsync();
    }
    public interface IEstadoMesaBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.EstadoMesaDto estadoMesa);
        Task<List<ModelsDto.EstadoMesaList>> ListadoAsync();
    }
}
