using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogger<LoginController> _logger;
        private readonly ILogin _login;
        private readonly IResponseApi _responseApi;
        public LoginController(ILogger<LoginController> logger, ILogin login, IResponseApi responseApi)
        {
            _logger = logger;
            _login = login;
            _responseApi = responseApi;
        }

        [HttpPost()]
        public async Task<ActionResult<TokenJwtDto>> PostAsync(LoginDto userManager)
        {
            try
            {

                if ((!string.IsNullOrEmpty(userManager.Usuario)) && (!string.IsNullOrEmpty(userManager.Password)))
                {
                    var _token = await _login.ValidarAsync(userManager);
                    if (_token != null)
    
                        return Ok(_responseApi.Msj(200, "OK", "", HttpContext, _token));
                    else 
                        return BadRequest(_responseApi.Msj(400, "Error Login", "Intento de login fallido", HttpContext, _token));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error Login", "Por favor complete los campos solicitados", HttpContext, ""));
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "PostAsync");
                throw;
            }
        }
    }
}
