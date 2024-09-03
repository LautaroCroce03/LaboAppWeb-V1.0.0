using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoMesaController : ControllerBase
    {
        private readonly IEstadoMesaBusiness _estadoMesa;

        public EstadoMesaController(IEstadoMesaBusiness estadoMesa)
        {
            _estadoMesa = estadoMesa;
        }

        [HttpPost()]
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
