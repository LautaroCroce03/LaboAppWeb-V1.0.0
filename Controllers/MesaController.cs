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
        private readonly IResponseApi _responseApi;

        public MesaController(ILogger<MesaController> logger, IMesaBusiness mesaBusiness, IEstadoMesaBusiness estadoMesaBusiness, IResponseApi responseApi)
        {
            _logger = logger;
            _mesaBusiness = mesaBusiness;
            _estadoMesaBusiness = estadoMesaBusiness;
            _responseApi = responseApi;
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
                    return Ok(_responseApi.Msj(200, "OK", "Se agrego correctamente", HttpContext, _result));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Error al realizar el alta", HttpContext, null));
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
                    return Ok(_responseApi.Msj(200, "OK", "Listado obtenido correctamente", HttpContext, _result));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "No hay mesas disponibles", HttpContext, null));
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
                if (!await _mesaBusiness.ExisteAsync(mesa.IdMesa))
                    return BadRequest(_responseApi.Msj(400, "Error", "No existe el id ingresado", HttpContext, null));

                if (!await _estadoMesaBusiness.ExisteAsync(mesa.IdEstado))
                    return BadRequest(_responseApi.Msj(400, "Error", "No existe el id de estado ingresado", HttpContext, null));

                var _result = await _mesaBusiness.ActualizarAsync(mesa);

                if (_result)
                {
                    return Ok(_responseApi.Msj(200, "OK", "Se actualizo correctamente", HttpContext, _result));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Error al actualizar", HttpContext, null));
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
