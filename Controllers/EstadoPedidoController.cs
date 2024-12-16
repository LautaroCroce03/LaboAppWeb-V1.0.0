using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/estadopedido")]
    [ApiController]
    public class EstadoPedidoController : ControllerBase
    {
        private readonly ILogger<EstadoPedidoController> _logger;
        private readonly IEstadoPedidoBusiness _estadoPedidoBusiness;
        private readonly IResponseApi _responseApi;

        public EstadoPedidoController(ILogger<EstadoPedidoController> logger, IEstadoPedidoBusiness estadoPedidoBusiness, IResponseApi responseApi)
        {
            _logger = logger;
            _estadoPedidoBusiness = estadoPedidoBusiness;
            _responseApi = responseApi;
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var _result = await _estadoPedidoBusiness.ListadoAsync();

                if ((_result != null) && (_result.Count > 0))
                {
                    var response = _responseApi.Msj(200, "Listado de Estados de Pedido", "Se han encontrado los estados de pedido.", HttpContext, _result);
                    return Ok(response);
                }
                else
                {
                    var response = _responseApi.Msj(400, "Error al realizar el alta", "No se encontraron estados de pedido.", HttpContext, null);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                var response = _responseApi.Msj(500, "Error interno", "Ocurrió un error al obtener los estados de pedido.", HttpContext, null);
                return StatusCode(500, response);
            }
        }
    }
}
