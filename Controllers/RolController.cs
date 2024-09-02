using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolBusiness _rolBusiness;

        public RolController(IRolBusiness rolBusiness)
        {
            _rolBusiness = rolBusiness;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ModelsDto.RolDto rolDto)
        {
            try
            {
                var _result = await _rolBusiness.AgregarAsync(rolDto);

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
                var _result = await _rolBusiness.Listado();

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
