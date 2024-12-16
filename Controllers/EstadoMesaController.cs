using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/estadomesa")]
    [ApiController]
    public class EstadoMesaController : ControllerBase
    {
        private readonly ILogger<EstadoMesaController> _logger;
        private readonly IEstadoMesaBusiness _estadoMesa;
        private readonly IResponseApi _responseApi;

        public EstadoMesaController(ILogger<EstadoMesaController> logger, IEstadoMesaBusiness estadoMesa, IResponseApi responseApi)
        {
            _logger = logger;
            _estadoMesa = estadoMesa;
            _responseApi = responseApi;
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
                    var response = _responseApi.Msj(200, "Listado de Estados de Mesa", "Se han encontrado los estados de mesa.", HttpContext, _result);
                    return Ok(response);
                }
                else
                {
                    var response = _responseApi.Msj(400, "No hay estados de mesa", "No se encontraron estados de mesa en el sistema.", HttpContext, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                var response = _responseApi.Msj(500, "Error interno", "Ocurrió un error al obtener los estados de mesa.", HttpContext, null);
                return StatusCode(500, response);
            }
        }
    }
}
