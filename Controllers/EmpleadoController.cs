using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/empleado")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IEmpleadoBusiness _empleadoBusiness;
        private readonly IRolBusiness _rolBusiness;
        private readonly ISectorBusiness _sectorBusiness;

        public EmpleadoController(ILogger<EmpleadoController> logger, IEmpleadoBusiness empleadoBusiness, IRolBusiness rolBusiness, ISectorBusiness sectorBusiness)
        {
            _logger = logger;
            _empleadoBusiness = empleadoBusiness;
            _rolBusiness = rolBusiness;
            _sectorBusiness = sectorBusiness;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                throw;
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post(int id, [FromBody] ModelsDto.EmpleadoDto empleadoDto)
        {
            try
            {
                //Validamos si existe el empleado
                if (!await _empleadoBusiness.ExisteAsync(id))
                {
                    return BadRequest("No existe el empleado ingresado");
                }



                var _result = await _empleadoBusiness.ActualizarAsync(empleadoDto, id);

                if (_result > 0)
                {
                    return Ok("Se actualizo correctamente");
                }
                else
                {
                    return BadRequest("Error al actualizar el alta");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                if (!await _empleadoBusiness.ExisteAsync(id))
                {
                    return BadRequest($"No existe un empleado con ID {id}");
                }

                var eliminado = await _empleadoBusiness.EliminarAsync(id);

                if (eliminado)
                {
                    return Ok($"El empleado con ID {id} fue eliminado correctamente.");
                }
                else
                {
                    return BadRequest($"No se pudo eliminar el empleado con ID {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete");
                return BadRequest($"Por favor intente mas tarde");
            }
        }
        [HttpPut()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                //Validamos si existe el empleado
                if (!await _empleadoBusiness.ExisteAsync(id))
                {
                    return BadRequest("No existe el empleado ingresado");
                }
                else if (!ModelState.IsValid)
                {
                    // Si la validación falla, devolver una respuesta con errores
                    return BadRequest(ModelState);
                }


                await _empleadoBusiness.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw;
            }
        }
    }
}
