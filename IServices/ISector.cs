namespace LaboAppWebV1._0._0.IServices
{
    public interface ISectorDataAccess
    {
        Task<Int32> AgregarAsync(Models.Sectore sectore);
        Task<List<Models.Sectore>> Listado();
    }
    public interface ISectorBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.SectorDto sectorDto);
        Task<List<ModelsDto.SectorListDto>> Listado();
    }
}
