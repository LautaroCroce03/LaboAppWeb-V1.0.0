using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private ILogger<ProductoController> _logger;
        private readonly IProductoBusiness _productoBusiness;
        private readonly ISectorBusiness _sectorBusiness;

        public ProductoController(ILogger<ProductoController> logger, IProductoBusiness productoBusiness, ISectorBusiness sectorBusiness)
        {
            _logger = logger;
            _productoBusiness = productoBusiness;
            _sectorBusiness = sectorBusiness;
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] ModelsDto.ProductoDto productoDto)
        {
            try
            {
                
                if (!await _sectorBusiness.ExisteId(productoDto.IdSector))
                {
                    return BadRequest("No existe el sector ingresado");
                }

                var result = await _productoBusiness.AgregarAsync(productoDto);

                if (result > 0)
                {
                    return Ok("Producto agregado correctamente");
                }
                else
                {
                    return BadRequest("Error al agregar el producto");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
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
                    return Ok(productos);
                }
                else
                {
                    return NoContent(); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDto productoDto)
        {
            try
            {
                if (id != productoDto.IdProducto)
                    return BadRequest("El ID no coincide.");

                var result = await _productoBusiness.ActualizarAsync(productoDto);

                if (result)
                {
                    return Ok("Producto actualizado correctamente.");
                }
                else
                {
                    return BadRequest("Error al actualizar el producto.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Put");
                return StatusCode(500, "Error interno del servidor.");
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
                    return Ok($"Producto con ID {id} eliminado correctamente.");
                }
                else
                {
                    return NotFound($"Producto con ID {id} no encontrado.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
