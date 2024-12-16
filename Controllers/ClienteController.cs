using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //[HttpGet("GetDemora")]
        //public async Task<IActionResult> GetDemora([Required] string codigoMesa, [Required] string codigoCliente)
        //{
        //    // Validación para verificar que los parámetros tengan exactamente 5 caracteres
        //    if (string.IsNullOrWhiteSpace(codigoMesa) || codigoMesa.Length != 5 || !codigoMesa.All(char.IsLetterOrDigit))
        //    {
        //        return BadRequest(new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "El parámetro 'codigoMesa' debe tener exactamente 5 caracteres alfanuméricos.",
        //            Data = null
        //        });
        //    }

        //    if (string.IsNullOrWhiteSpace(codigoCliente) || codigoCliente.Length != 5 || !codigoCliente.All(char.IsLetterOrDigit))
        //    {
        //        return BadRequest(new ApiResponse<object>
        //        {
        //            Success = false,
        //            Message = "El parámetro 'CodigoCliente' debe tener exactamente 5 caracteres alfanuméricos.",
        //            Data = null
        //        });
        //    }
        //    try
        //    {
        //        // Llamada al servicio para obtener la demora
        //        var resultado = await _clienteServicio.GetDemora(codigoMesa, codigoCliente);

        //        // Verifica si el resultado es nulo o vacío
        //        if (resultado == null)
        //        {
        //            return NotFound(new ApiResponse<object>
        //            {
        //                Success = false,
        //                Message = "No se encontró información de demora para la mesa y cliente proporcionados.",
        //                Data = null
        //            });
        //        }

        //        return Ok(new ApiResponse<ClienteResponseDto>
        //        {
        //            Success = true,
        //            Message = "Demora obtenida con éxito.",
        //            Data = resultado
        //        });
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        // Manejo de errores si no se encuentra la mesa o el pedido
        //        return NotFound(new ApiResponse<string>
        //        {
        //            Success = false,
        //            Message = "Mesa o pedido no encontrados.",
        //            Data = ex.Message
        //        });
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        // Manejo de errores si el códigocliente  no está asociado a la mesa
        //        return BadRequest(new ApiResponse<string>
        //        {
        //            Success = false,
        //            Message = "El código del cliente no está asociado a la mesa proporcionada.",
        //            Data = ex.Message
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de errores generales
        //        return StatusCode(500, new ApiResponse<string>
        //        {
        //            Success = false,
        //            Message = "Ocurrió un error inesperado al procesar la solicitud.",
        //            Data = ex.Message
        //        });
        //    }
        //}
    }
}
