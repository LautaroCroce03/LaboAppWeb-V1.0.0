using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/estadomesa")]
    [ApiController]
    public class EstadoMesaController : ControllerBase
    {
        private readonly ILogger<EstadoMesaController> _logger;
        private readonly IEstadoMesaBusiness _estadoMesa;

        public EstadoMesaController(ILogger<EstadoMesaController> logger, IEstadoMesaBusiness estadoMesa)
        {
            _logger = logger;
            _estadoMesa = estadoMesa;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] ModelsDto.EstadoMesaDto estadoMesa)
        {
            try
            {
                var _result = await _estadoMesa.AgregarAsync(estadoMesa);

                if (_result > 0)
                {
                    return Ok("Se agrego correctamente");
                }
                else
                {
                    return BadRequest("Error al realizar el alta");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw;
            }
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var _result = await _estadoMesa.ListadoAsync();

                if (_result.Count > 0)
                {
                    return Ok(_result);
                }
                else
                {
                    return BadRequest("Error al realizar el alta");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                throw;
            }
        }
    }
}
