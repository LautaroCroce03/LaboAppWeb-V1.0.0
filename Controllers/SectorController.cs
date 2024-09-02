using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorBusiness _sector;

        public SectorController(ISectorBusiness sector)
        {
            _sector = sector;
        }

        [HttpPost()]
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
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet()]
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
