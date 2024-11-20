using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Login: ILogin
    {
        private ILogger<Login> _logger; 
        private readonly IEmpleadoBusiness _empleadoBusiness;
        private readonly ITokenJWT _tokenJWT;

        public Login(ILogger<Login> logger, IEmpleadoBusiness empleadoBusiness, ITokenJWT tokenJWT)
        {
            _logger = logger;
            _empleadoBusiness = empleadoBusiness;
            _tokenJWT = tokenJWT;
        }

        public async Task<TokenJwtDto> Validar(LoginDto login) 
        {
            try
            {
                var _login = new ModelsDto.EmpleadoDto() 
                {
                    Usuario = login.Usuario,
                    Password = login.Password,
                };

                if (await _empleadoBusiness.ExisteLoginAsync(_login)) 
                {
                    var empleado = await _empleadoBusiness.EmpleadoLoginAsync(_login);

                    if (empleado != null) 
                    {
                       var toke =  await _tokenJWT.GenerarLoginAsync(empleado);
                        return toke;
                    }

                }
                return new TokenJwtDto();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
