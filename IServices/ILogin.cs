using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface ILogin
    {
        Task<TokenJwtDto> ValidarAsync(LoginDto login);
    }
}
