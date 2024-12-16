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
        private readonly IResponseApi _responseApi;

        public EmpleadoController(ILogger<EmpleadoController> logger, IEmpleadoBusiness empleadoBusiness, IRolBusiness rolBusiness, ISectorBusiness sectorBusiness, IResponseApi responseApi)
        {
            _logger = logger;
            _empleadoBusiness = empleadoBusiness;
            _rolBusiness = rolBusiness;
            _sectorBusiness = sectorBusiness;
            _responseApi = responseApi;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireAdministradorRole")]
        public async Task<IActionResult> Post([FromBody] ModelsDto.EmpleadoDto empleadoDto)
        {
            try
            {
                // Validamos si existe el rol
                if (!await _rolBusiness.ExisteId(empleadoDto.IdSector))
                {
                    var response = _responseApi.Msj(400, "No existe el rol ingresado", "Verifique el ID del sector proporcionado.", HttpContext, null);
                    return BadRequest(response);
                }

                // Validamos si existe el sector
                else if (!await _sectorBusiness.ExisteId(empleadoDto.IdSector))
                {
                    var response = _responseApi.Msj(400, "No existe el sector ingresado", "Verifique el ID del sector proporcionado.", HttpContext, null);
                    return BadRequest(response);
                }

                var _result = await _empleadoBusiness.AgregarAsync(empleadoDto);

                if (_result > 0)
                {
                    var response = _responseApi.Msj(200, "Se agregó correctamente", "El empleado fue agregado con éxito.", HttpContext, null);
                    return Ok(response);
                }
                else
                {
                    var response = _responseApi.Msj(400, "Error al realizar el alta", "Hubo un problema al agregar el empleado.", HttpContext, null);
                    return BadRequest(response);
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
        [Authorize(Policy = "RequireAdministradorRole")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var _result = await _empleadoBusiness.ListadoAsync(true);

                if (_result.Count > 0)
                {
                    return Ok(_result);
                }
                else
                {
                    var response = _responseApi.Msj(400, "No se encontraron empleados activos", "No hay empleados activos en el sistema.", HttpContext, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                throw;
            }
        }

        [HttpGet("inActivo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireAdministradorRole")]
        public async Task<IActionResult> GetInActivo()
        {
            try
            {
                var _result = await _empleadoBusiness.ListadoAsync(false);

                if (_result.Count > 0)
                {
                    return Ok(_result);
                }
                else
                {
                    var response = _responseApi.Msj(400, "No se encontraron empleados inactivos", "No hay empleados inactivos en el sistema.", HttpContext, null);
                    return BadRequest(response);
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
        [Authorize(Policy = "RequireAdministradorRole")]
        public async Task<IActionResult> Put(int id, [FromBody] ModelsDto.EmpleadoDto empleadoDto)
        {
            try
            {
                if (!await _empleadoBusiness.ExisteAsync(id))
                {
                    var response = _responseApi.Msj(400, "No existe el empleado ingresado", "Verifique el ID del empleado proporcionado.", HttpContext, null);
                    return BadRequest(response);
                }

                var _result = await _empleadoBusiness.ActualizarAsync(empleadoDto, id);

                if (_result > 0)
                {
                    var response = _responseApi.Msj(200, "Se actualizó correctamente", "El empleado fue actualizado con éxito.", HttpContext, null);
                    return Ok(response);
                }
                else
                {
                    var response = _responseApi.Msj(400, "Error al actualizar el empleado", "Hubo un problema al actualizar el empleado.", HttpContext, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Put");
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireAdministradorRole")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await _empleadoBusiness.ExisteAsync(id))
                {
                    var response = _responseApi.Msj(400, $"No existe un empleado con ID {id}", "Verifique el ID del empleado proporcionado.", HttpContext, null);
                    return BadRequest(response);
                }

                var eliminado = await _empleadoBusiness.EliminarAsync(id);

                if (eliminado)
                {
                    var response = _responseApi.Msj(200, $"El empleado con ID {id} fue eliminado correctamente.", "El empleado ha sido eliminado con éxito.", HttpContext, null);
                    return Ok(response);
                }
                else
                {
                    var response = _responseApi.Msj(400, $"No se pudo eliminar el empleado con ID {id}", "Hubo un problema al intentar eliminar el empleado.", HttpContext, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete");
                var response = _responseApi.Msj(400, "Error interno al intentar eliminar el empleado", "Por favor, intente más tarde.", HttpContext, null);
                return BadRequest(response);
            }
        }
    }
}
