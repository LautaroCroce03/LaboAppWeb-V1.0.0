using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/sector")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private ILogger<SectorController> _logger;
        private readonly ISectorBusiness _sector;

        public SectorController(ILogger<SectorController> logger, ISectorBusiness sector)
        {
            _logger = logger;
            _sector = sector;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody]ModelsDto.SectorDto sector)
        {
            try
            {
                 var _result = await _sector.AgregarAsync(sector);

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
                var _result = await _sector.ListadoAsync();

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
