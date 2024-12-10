namespace LaboAppWebV1._0._0.IServices
{
    public interface IComandaDataAccess
    {
        Task<Int32> AgregarAsync(Models.Comanda comanda);
        Task<ModelsDto.ComandaDetalleDto> ListadoAsync(Int32 idComanda);
        Task<List<ModelsDto.ComandaDetalleDto>> ListadoAsync();

        Task<bool> EliminarAsync(int idComanda);

        Task UpdateAsync(Models.Comanda comanda);
        Task DeleteAsync(int id);

    }
    public interface IComandaBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.ComandaDto comanda);
        Task<ModelsDto.ComandaDetalleDto> ListadoAsync(Int32 idComanda);
        Task<List<ModelsDto.ComandaDetalleDto>> ListadoAsync();
        Task<bool> EliminarAsync(int idComanda);
        Task UpdateAsync(ModelsDto.ComandaDto comanda);
        Task DeleteAsync(int id);

    }
}
