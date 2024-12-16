using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
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

        
        [HttpGet("cantidadEmpleadosPorSector")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireSocioRole")]
        public async Task<ActionResult<IEnumerable<EmpleadosPorSectorResponseDto>>> CantidadEmpleadosPorSector()
        {

            try
            {
                // Llama al servicio para obtener la cantidad de empleados
                var resultado = await _empleadoBusiness.CantidadEmpleadosPorSector();

                // Si no hay empleados
                if (resultado == null)
                {
                    
                    return NotFound(_responseApi.Msj(404, "Error", "No hay empleados para mostrar...", HttpContext, null));
                }

                // Devuelve el el resultado con un código de estado 200 OK

                
                return Ok(_responseApi.Msj(200, "Correcto", "Cantidad de empleados por sector obtenida exitosamente.", HttpContext, resultado));
            }
            catch (Exception ex)
            {
                return BadRequest(_responseApi.Msj(400, "Error", $"Ocurrió un error al obtener la cantidad de empleados por sector: {ex.Message}", HttpContext, null));
            }


        }

        [HttpGet("cantidadOperacionesPorSector/{idSector}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireSocioRole")]
        public async Task<ActionResult<IEnumerable<OperacionesPorSectorDto>>> CantidadOperacionesPorSector(
             int idSector,
             [FromQuery] DateTime? fechaInicio,
             [FromQuery] DateTime? fechaFin)
        {
            // Verifica que el ID sea válido
            if (idSector <= 0)
            {
                
                return BadRequest(_responseApi.Msj(400, "Error", "El ID del sector debe ser mayor que 0.", HttpContext, null));
            }

            // Validar que fechaInicio no sea mayor que fechaFin
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio.Value > fechaFin.Value)
            {
                
                return BadRequest(_responseApi.Msj(400, "Error", "La fecha de inicio no puede ser mayor que la fecha de fin.", HttpContext, null));
            }

            try
            {
                // Llama al servicio con los parámetros de fechas
                var resultado = await _empleadoBusiness.CantidadOperacionesPorSector(idSector, fechaInicio, fechaFin);



                // Si no se encuentran operaciones
                if (resultado == null || !resultado.Any())
                {
                    string mensaje = fechaInicio.HasValue || fechaFin.HasValue
                        ? $"No se encontraron operaciones para el sector con ID: {idSector} dentro del rango de fechas especificado."
                        : $"No se encontraron operaciones para el sector con ID: {idSector}.";

                    return NotFound(_responseApi.Msj(404, "Error", mensaje, HttpContext, null));
                }

                
                return Ok(_responseApi.Msj(200, "Correcto", "Operaciones obtenidas exitosamente por sector.", HttpContext, resultado));
            }
            catch (Exception ex)
            {
                return BadRequest(_responseApi.Msj(400, "Error", $"Ocurrió un error al obtener la cantidad de operaciones por sector: {ex.Message}", HttpContext, null));

            }
        }

        [HttpGet("operacionesDeTodosLosEmpleados")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireSocioRole")]
        public async Task<ActionResult<IEnumerable<OperacionesEmpleadoDto>>> ObtenerTodasLasOperacionesEmpleados(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Validación de parámetros
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
            {
                
                return BadRequest(_responseApi.Msj(400, "Error", "La fecha de inicio no puede ser posterior a la fecha de fin.", HttpContext, null));
            }

            try
            {
                // Llama al servicio pasándole las fechas de filtro
                var operaciones = await _empleadoBusiness.ObtenerTodasLasOperacionesEmpleados(fechaInicio, fechaFin);

                // Si no se encontraron operaciones
                if (operaciones == null || !operaciones.Any())
                {
                    
                    return NotFound(_responseApi.Msj(404, "Error", "No se encontraron operaciones para los filtros proporcionados.", HttpContext, null));
                }

                
                return Ok(_responseApi.Msj(404, "Correcto", "Operaciones de empleados obtenidas exitosamente.", HttpContext, operaciones));

            }
            catch (Exception ex)
            {
                return BadRequest(_responseApi.Msj(400, "Error", $"Ocurrió un error al obtener las operaciones de empleados: {ex.Message}", HttpContext, null));
            }

        }

    }
}
