using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

                return Ok(_responseApi.Msj(200, "OK", "Se cambio correctamente el estado", HttpContext, ""));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Put");
                throw;
            }
        }

        //// INFORMES PEDIDOS A -  producto más vendido
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("productoMasVendido")]

        public async Task<IActionResult> GetProductoMasVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                // Llama al servicio para obtener el producto más vendido con el filtro de fechas
                var productoMasVendido = await _pedidoBusiness.GetProductoMasVendido(fechaInicio, fechaFin);

                // Si el producto no existe en la base de datos, devuelve un mensaje de error
                if (productoMasVendido == null)
                {
                    return NotFound(_responseApi.Msj(404, "Error", "No se encontró ningún producto vendido.", HttpContext, null));
                }

                // Devuelve el producto más vendido con un código de estado 200 OK

                
                return Ok(_responseApi.Msj(200, "Correcto", "Producto más vendido obtenido exitosamente..", HttpContext, productoMasVendido));
            }
            catch (Exception ex)
            {
                return BadRequest(_responseApi.Msj(400, "Error", $"Ocurrió un error inesperado al obtener el producto más vendido: {ex.Message}", 
                    HttpContext, null));
            }
        }


        // INFORMES PEDIDOS B -  producto menos vendido
        [Authorize(Policy = "RequireSocioRole")]
        [HttpGet("productoMenosVendido")]
        public async Task<IActionResult> GetProductoMenosVendido(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                // Llama al servicio para obtener el producto menos vendido, pasando las fechas como parámetros
                var productoMenosVendido = await _pedidoBusiness.GetProductoMenosVendido(fechaInicio, fechaFin);

                // Si no se encuentra ningún producto menos vendido, devuelve un mensaje de error
                if (productoMenosVendido == null)
                {
                    return NotFound(_responseApi.Msj(404, "Error", "No se encontró ningún producto vendido.", HttpContext, null));
                }

                // Devuelve el producto menos vendido con un código de estado 200 OK
                return Ok(_responseApi.Msj(200, "Correcto", "Producto menos vendido obtenido exitosamente.", HttpContext, productoMenosVendido));

            }
            catch (Exception ex)
            {
                return BadRequest(_responseApi.Msj(400, "Error", $"Ocurrió un error inesperado al obtener el producto menos vendido: {ex.Message}",
                    HttpContext, null));
            }
        }

        [Authorize]
        [HttpGet("GetProductosEnEstadoPendientePorSector")]
        public async Task<IActionResult> GetProductosxSector(int sectorId)
        {

            // Obtenemos el rol del usuario autenticado
            var userRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userSectorIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SectorId")?.Value;

            // Si el usuario no tiene el claim de sector o rol, retornamos no autorizado
            if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userSectorIdClaim))
            {
                
                return Unauthorized(_responseApi.Msj(403, "Error", "No tiene permisos para realizar esta operación.", HttpContext, null));
            }

            // Convertimos el claim de sector a entero
            if (!int.TryParse(userSectorIdClaim, out var userSectorId))
            {
                return Unauthorized(_responseApi.Msj(403, "Error", "No tiene permisos para realizar esta operación.", HttpContext, null));
            }

            // Verificamos permisos según el rol
            if (userRole != "Socio" && userSectorId != sectorId)
            {
                return Unauthorized(_responseApi.Msj(403, "Error", "No tiene permisos para visualizar los pedidos del sector solicitado.", HttpContext, null));
            }

            // Validar que el sectorId sea mayor a 0
            if (sectorId <= 0)
            {
                
                return BadRequest(_responseApi.Msj(400, "Error", "El ID del sector proporcionado no es válido. Debe ser un número mayor que 0.", HttpContext, null));
            }

            try
            {
                // Llamamos al servicio para obtener los productos por sector en estado pendiente
                var productos = await _pedidoBusiness.GetProductosPendientesXSector(sectorId);

                // Si no hay productos, retornamos un mensaje de error
                if (productos == null || !productos.Any())
                {
                    
                    return NotFound(_responseApi.Msj(404, "Error", "No se encontró ningún producto en estado pendiente para este sector.", HttpContext, null));
                }

                
                return Ok(_responseApi.Msj(404, "Error", "Productos en estado pendiente encontrados exitosamente.", HttpContext, productos));
            }
            catch (Exception ex)
            {
                return BadRequest(_responseApi.Msj(400, "Error", $"Ocurrió un error al obtener los productos pendientes: {ex.Message}", HttpContext, null));
            }
        }
    }
}
