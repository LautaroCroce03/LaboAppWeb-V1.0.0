using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoBusiness _empleadoBusiness;

        public EmpleadoController(IEmpleadoBusiness empleadoBusiness)
        {
            _empleadoBusiness = empleadoBusiness;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ModelsDto.EmpleadoDto empleadoDto)
        {
            try
            {
                var _result = await _empleadoBusiness.AgregarAsync(empleadoDto);

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
                var _result = await _empleadoBusiness.ListadoAsync();

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
