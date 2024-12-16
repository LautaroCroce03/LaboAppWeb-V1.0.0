using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/comanda")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly ILogger<ComandaController> _logger;
        private readonly IComandaBusiness _comandaBusiness;
        private readonly IMesaBusiness _mesaBusiness;
        private readonly IResponseApi _responseApi;

        public ComandaController(ILogger<ComandaController> logger, IComandaBusiness comandaBusiness, IMesaBusiness mesaBusiness, IResponseApi responseApi)
        {
            _logger = logger;
            _comandaBusiness = comandaBusiness;
            _mesaBusiness = mesaBusiness;
            _responseApi = responseApi;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireMozoRole")]
        public async Task<IActionResult> Post([FromBody] ModelsDto.ComandaDto comanda)
        {
            try
            {
                //if (comanda.Pedidos.Count.Equals(0))
                //    return BadRequest("Sin pedido");

                if (!await _mesaBusiness.ExisteAsync(comanda.IdMesa))
                    return BadRequest(_responseApi.Msj(400, "Error", "El número de mesa ingresado no existe", this.HttpContext, ""));

                var idComanda = await _comandaBusiness.AgregarAsync(comanda);
                if (idComanda > 0)
                {
                    return Ok(_responseApi.Msj(200, "Correcto", $"Nro de pedido {idComanda}", this.HttpContext, ""));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Por favor completar los campos", this.HttpContext, ""));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la comanda.");
                return BadRequest(_responseApi.Msj(400, "Error", "Error al procesar la comanda", this.HttpContext, ""));
            }
        }

        [HttpGet("{idComanda}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireMozoRole")]
        public async Task<IActionResult> Get(Int32 idComanda)
        {
            try
            {
                if (idComanda <= 0)
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "ID de comanda inválido", this.HttpContext, ""));
                }

                var _comanda = await _comandaBusiness.ListadoAsync(idComanda);

                if (_comanda != null)
                {
                    return Ok(_responseApi.Msj(200, "Correcto", "Comanda encontrada", this.HttpContext, _comanda));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Sin registros", this.HttpContext, ""));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la comanda.");
                return BadRequest(_responseApi.Msj(400, "Error", "Error al procesar la comanda", this.HttpContext, ""));
            }
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireMozoRole")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var _comanda = await _comandaBusiness.ListadoAsync();

                if (_comanda != null)
                {
                    return Ok(_responseApi.Msj(200, "Correcto", "Listado de comandas obtenido", this.HttpContext, _comanda));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Sin registros", this.HttpContext, ""));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                return BadRequest(_responseApi.Msj(400, "Error", "Error al obtener el listado de comandas", this.HttpContext, ""));
            }
        }

        [HttpDelete("{idComanda}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "RequireMozoRole")]
        public async Task<IActionResult> Delete(int idComanda)
        {
            try
            {
                var result = await _comandaBusiness.EliminarAsync(idComanda);
                if (result)
                {
                    return Ok(_responseApi.Msj(200, "Correcto", $"Comanda con ID {idComanda} eliminada correctamente", this.HttpContext, ""));
                }
                else
                {
                    return NotFound(_responseApi.Msj(404, "Error", $"Comanda con ID {idComanda} no encontrada", this.HttpContext, ""));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete");
                return StatusCode(500, _responseApi.Msj(500, "Error", $"Error interno del servidor: {ex.Message}", this.HttpContext, ""));
            }
        }
    }
}
