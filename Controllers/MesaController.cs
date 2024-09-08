using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/mesa")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly IMesaBusiness _mesaBusiness;

        public MesaController(IMesaBusiness mesaBusiness)
        {
            _mesaBusiness = mesaBusiness;
        }

        [HttpPost()]
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
