namespace LaboAppWebV1._0._0.IServices
{
    public interface IMesaDataAccess
    {
        Task<Int32> AgregarAsync(Models.Mesa mesa);
        Task<List<Models.Mesa>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 idMesa);
    }
    public interface IMesaBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.MesaDto estadoMesa);
        Task<List<ModelsDto.MesaListDto>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 idMesa);
    }
}
