﻿using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoBusiness _productoBusiness;
        private readonly ISectorBusiness _sectorBusiness;

        public ProductoController(IProductoBusiness productoBusiness, ISectorBusiness sectorBusiness)
        {
            _productoBusiness = productoBusiness;
            _sectorBusiness = sectorBusiness;
        }
        
        [HttpPost()]
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
                
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet()]
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
                
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}