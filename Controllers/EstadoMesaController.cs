﻿using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/estadomesa")]
    [ApiController]
    public class EstadoMesaController : ControllerBase
    {
        private readonly ILogger<EstadoMesaController> _logger;
        private readonly IEstadoMesaBusiness _estadoMesa;

        public EstadoMesaController(ILogger<EstadoMesaController> logger, IEstadoMesaBusiness estadoMesa)
        {
            _logger = logger;
            _estadoMesa = estadoMesa;
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                throw;
            }
        }
    }
}
