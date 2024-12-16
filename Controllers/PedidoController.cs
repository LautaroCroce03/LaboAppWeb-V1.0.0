using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private ILogger<PedidoController> _logger;
        private readonly IPedidoBusiness _pedidoBusiness;

        public PedidoController(ILogger<PedidoController> logger, IPedidoBusiness pedidoBusiness)
        {
            _logger = logger;
            _pedidoBusiness = pedidoBusiness;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireMozoRole")]
        public async Task<IActionResult> Post([FromBody] ModelsDto.PedidoDto pedido)
        {
            try
            {
                var _result = await _pedidoBusiness.AgregarAsync(pedido);

                if (_result != null)
                {
                    return Ok(_result);
                }
                else
                {
                    return BadRequest("Error al realizar el alta");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw;
            }
        }

        [HttpPut("/{idPedido}/{idEstadoNuevo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireBartenderOrCerveceroOrCocineroRole")]
        public async Task<IActionResult> Put(Int32 idPedido, Int32 idEstadoNuevo)
        {
            try
            {
                var _result = await _pedidoBusiness.ExisteAsync(idPedido);

                if (!_result)
                {
                    return BadRequest("No existe el pedido");
                }

                await _pedidoBusiness.CambioEstadoAsync(idPedido, idEstadoNuevo);

                return Ok("Se cambio correctamente el estado");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw;
            }
        }
        [HttpPut("codigoCliente/{codCliente}/{idEstadoNuevo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireBartenderOrCerveceroOrCocineroRole")]
        public async Task<IActionResult> PutCodCliente(string codCliente, Int32 idEstadoNuevo)
        {
            try
            {
                var _result = await _pedidoBusiness.ExisteIdClienteAsync(codCliente);

                if (!_result)
                {
                    return BadRequest("No existe el pedido");
                }

                await _pedidoBusiness.CambioEstadoidClienteAsync(codCliente, idEstadoNuevo);

                return Ok("Se cambio correctamente el estado");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Post");
                throw;
            }
        }


    }
}
