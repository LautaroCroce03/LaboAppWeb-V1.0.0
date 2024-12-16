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
        private readonly IResponseApi _responseApi;

        public PedidoController(ILogger<PedidoController> logger, IPedidoBusiness pedidoBusiness, IResponseApi responseApi)
        {
            _logger = logger;
            _pedidoBusiness = pedidoBusiness;
            _responseApi = responseApi;
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
                    return Ok(_responseApi.Msj(200, "OK", "Pedido agregado correctamente", HttpContext, _result));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Error al realizar el alta", HttpContext, null));
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
                    return BadRequest(_responseApi.Msj(400, "Error", "No existe el pedido", HttpContext, null));
                }

                await _pedidoBusiness.CambioEstadoAsync(idPedido, idEstadoNuevo);

                return Ok(_responseApi.Msj(200, "OK", "Se cambio correctamente el estado", HttpContext, null));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Put");
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
                    return BadRequest(_responseApi.Msj(400, "Error", "No existe el pedido", HttpContext, null));
                }

                await _pedidoBusiness.CambioEstadoidClienteAsync(codCliente, idEstadoNuevo);

                return Ok(_responseApi.Msj(200, "OK", "Se cambio correctamente el estado", HttpContext, null));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Put");
                throw;
            }
        }
    }
}
