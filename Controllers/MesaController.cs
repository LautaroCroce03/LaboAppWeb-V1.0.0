using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/mesa")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly IMesaBusiness _mesaBusiness;
        private readonly IEstadoMesaBusiness _estadoMesaBusiness;

        public MesaController(IMesaBusiness mesaBusiness, IEstadoMesaBusiness estadoMesaBusiness)
        {
            _mesaBusiness = mesaBusiness;
            _estadoMesaBusiness = estadoMesaBusiness;
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

        [HttpPut()]
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
