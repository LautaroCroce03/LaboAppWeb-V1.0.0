using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/empleado")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoBusiness _empleadoBusiness;
        private readonly IRolBusiness _rolBusiness;
        private readonly ISectorBusiness _sectorBusiness;

        public EmpleadoController(IEmpleadoBusiness empleadoBusiness, IRolBusiness rolBusiness, ISectorBusiness sectorBusiness)
        {
            _empleadoBusiness = empleadoBusiness;
            _rolBusiness = rolBusiness;
            _sectorBusiness = sectorBusiness;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ModelsDto.EmpleadoDto empleadoDto)
        {
            try
            {
                //Validamos si existe el rol
                if (!await _rolBusiness.ExisteId(empleadoDto.IdSector)) 
                {
                    return BadRequest("No existe el rol ingresado");
                }
                //Validamos si existe el sector
                else if (!await _sectorBusiness.ExisteId(empleadoDto.IdSector))
                {
                    return BadRequest("No existe el sector ingresado");
                }

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
