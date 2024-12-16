using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/mesa")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private ILogger<MesaController> _logger;
        private readonly IMesaBusiness _mesaBusiness;
        private readonly IEstadoMesaBusiness _estadoMesaBusiness;

        public MesaController(ILogger<MesaController> logger, IMesaBusiness mesaBusiness, IEstadoMesaBusiness estadoMesaBusiness)
        {
            _logger = logger;
            _mesaBusiness = mesaBusiness;
            _estadoMesaBusiness = estadoMesaBusiness;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] ModelsDto.MesaDto mesa)
        {
            try
            {
                var _result = await _mesaBusiness.AgregarAsync(mesa);

                if (_result > 0)
                {
                    return Ok("Se agrego correctamente");
                }
                else
                {
                    return BadRequest("Error al realizar el alta");
                }
            }
            catch (System.Exception ex)
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
                var _result = await _mesaBusiness.ListadoAsync();

                if (_result.Count > 0)
                {
                    return Ok(_result);
                }
                else
                {
                    return BadRequest("Error al realizar el alta");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Get");
                throw;
            }
        }

        [HttpPut()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromBody] ModelsDto.MesaListDto mesa)
        {
            try
            {

                if(!await _mesaBusiness.ExisteAsync(mesa.IdMesa))
                    return BadRequest("No existe el id ingresado");

                if(!await _estadoMesaBusiness.ExisteAsync(mesa.IdEstado))
                    return BadRequest("No existe el id ingresado");

                var _result = await _mesaBusiness.ActualizarAsync(mesa);

                if (_result)
                {
                    return Ok("Se actualizo correctamente");
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Put");
                throw;
            }
        }
    }
}
