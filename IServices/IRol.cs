namespace LaboAppWebV1._0._0.IServices
{
    public interface IRolDataAccess
    {
        Task<Int32> AgregarAsync(Models.Role rol);
        Task<List<Models.Role>> ListadoAsync();
        Task<bool> ExisteIdAsync(Int32 idRol);
    }
    public interface IRolBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.RolDto rolDto);
        Task<List<ModelsDto.RolListDto>> ListadoAsync();
        Task<bool> ExisteId(Int32 idRol);
    }
}
