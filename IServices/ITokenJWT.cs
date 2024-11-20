namespace LaboAppWebV1._0._0.IServices
{
    public interface ITokenJWT
    {
        Task<ModelsDto.TokenJwtDto> GenerarLoginAsync(ModelsDto.EmpleadoListDto usuario);
        Task<ModelsDto.TokenJwtDto> GenerarRefreshAsync(ModelsDto.EmpleadoListDto usuario);
    }
}
