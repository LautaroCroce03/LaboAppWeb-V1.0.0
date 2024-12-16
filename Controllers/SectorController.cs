using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/sector")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private ILogger<SectorController> _logger;
        private readonly ISectorBusiness _sector;
        private readonly IResponseApi _responseApi;

        public SectorController(ILogger<SectorController> logger, ISectorBusiness sector, IResponseApi responseApi)
        {
            _logger = logger;
            _sector = sector;
            _responseApi = responseApi;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] ModelsDto.SectorDto sector)
        {
            try
            {
                var _result = await _sector.AgregarAsync(sector);

                if (_result > 0)
                {
                    return Ok(_responseApi.Msj(200, "Éxito", "Se agrego correctamente", HttpContext, null));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Error al realizar el alta", HttpContext, null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                return StatusCode(500, _responseApi.Msj(500, "Error", $"Error interno del servidor: {ex.Message}", HttpContext, null));
            }
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var _result = await _sector.ListadoAsync();

                if (_result.Count > 0)
                {
                    return Ok(_responseApi.Msj(200, "Éxito", "Listado de sectores", HttpContext, _result));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "No se encontraron sectores", HttpContext, null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                return StatusCode(500, _responseApi.Msj(500, "Error", $"Error interno del servidor: {ex.Message}", HttpContext, null));
            }
        }
    }
}
