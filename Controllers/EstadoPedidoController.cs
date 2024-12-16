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

        public EstadoPedidoController(ILogger<EstadoPedidoController> logger, IEstadoPedidoBusiness estadoPedidoBusiness)
        {
            _logger = logger;
            _estadoPedidoBusiness = estadoPedidoBusiness;
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
