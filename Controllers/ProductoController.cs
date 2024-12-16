using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private ILogger<ProductoController> _logger;
        private readonly IProductoBusiness _productoBusiness;
        private readonly ISectorBusiness _sectorBusiness;
        private readonly IResponseApi _responseApi;

        public ProductoController(ILogger<ProductoController> logger, IProductoBusiness productoBusiness, ISectorBusiness sectorBusiness, IResponseApi responseApi)
        {
            _logger = logger;
            _productoBusiness = productoBusiness;
            _sectorBusiness = sectorBusiness;
            _responseApi = responseApi;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] ModelsDto.ProductoDto productoDto)
        {
            try
            {
                if (!await _sectorBusiness.ExisteId(productoDto.IdSector))
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "No existe el sector ingresado", HttpContext, null));
                }

                var result = await _productoBusiness.AgregarAsync(productoDto);

                if (result > 0)
                {
                    return Ok(_responseApi.Msj(200, "Éxito", "Producto agregado correctamente", HttpContext, null));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Error al agregar el producto", HttpContext, null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                return StatusCode(500, _responseApi.Msj(500, "Error", $"Error interno del servidor: {ex.Message}", HttpContext, null));
            }
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productos = await _productoBusiness.ListadoAsync();

                if (productos.Count > 0)
                {
                    return Ok(_responseApi.Msj(200, "Éxito", "Listado de productos", HttpContext, productos));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                return StatusCode(500, _responseApi.Msj(500, "Error", $"Error interno del servidor: {ex.Message}", HttpContext, null));
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDto productoDto)
        {
            try
            {
                if (id != productoDto.IdProducto)
                    return BadRequest(_responseApi.Msj(400, "Error", "El ID no coincide.", HttpContext, null));

                var result = await _productoBusiness.ActualizarAsync(productoDto);

                if (result)
                {
                    return Ok(_responseApi.Msj(200, "Éxito", "Producto actualizado correctamente.", HttpContext, null));
                }
                else
                {
                    return BadRequest(_responseApi.Msj(400, "Error", "Error al actualizar el producto.", HttpContext, null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Put");
                return StatusCode(500, _responseApi.Msj(500, "Error", "Error interno del servidor.", HttpContext, null));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productoBusiness.EliminarAsync(id);
                if (result)
                {
                    return Ok(_responseApi.Msj(200, "Éxito", $"Producto con ID {id} eliminado correctamente.", HttpContext, null));
                }
                else
                {
                    return NotFound(_responseApi.Msj(404, "Error", $"Producto con ID {id} no encontrado.", HttpContext, null));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete");
                return StatusCode(500, _responseApi.Msj(500, "Error", $"Error interno del servidor: {ex.Message}", HttpContext, null));
            }
        }
    }
}
