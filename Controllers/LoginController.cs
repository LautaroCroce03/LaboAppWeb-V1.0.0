using LaboAppWebV1._0._0.Business;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogger<Login> _logger;
        private readonly ILogin _login;

        public LoginController(ILogger<Login> logger, ILogin login)
        {
            _logger = logger;
            _login = login;
        }

        [HttpPost()]
        public async Task<ActionResult<TokenJwtDto>> PostAsync(LoginDto userManager)
        {
            try
            {

                if ((!string.IsNullOrEmpty(userManager.Usuario)) && (!string.IsNullOrEmpty(userManager.Password)))
                {
                    var _token = await _login.Validar(userManager);
                    if (_token != null)
                        return Ok(_token);
                    else 
                        return BadRequest("Intento de login fallido");
                }
                else
                {
                    return BadRequest("Por favor complete los campos solicitados");
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}
