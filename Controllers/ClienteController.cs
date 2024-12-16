using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/v1/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private ILogger<ClienteController> _logger;
        private readonly IResponseApi _responseApi;
        private readonly IClienteBusiness _clienteBusiness;
        public ClienteController(ILogger<ClienteController> logger, IResponseApi responseApi, IClienteBusiness clienteBusiness)
        {
            _logger = logger;
            _responseApi = responseApi;
            _clienteBusiness = clienteBusiness;
        }

        [HttpGet("{codigoMesa}/{codigoCliente}")]
        public async Task<IActionResult> GetDemora([Required] string codigoMesa, [Required] string codigoCliente)
        {
            // Validación para verificar que los parámetros tengan exactamente 5 caracteres
            if (string.IsNullOrWhiteSpace(codigoMesa) || codigoMesa.Length != 5 || !codigoMesa.All(char.IsLetterOrDigit))
            {
                return BadRequest(_responseApi.Msj(400,"Error", "El parámetro 'codigoMesa' debe tener exactamente 5 caracteres alfanuméricos.", HttpContext,""));
            }

            if (string.IsNullOrWhiteSpace(codigoCliente) || codigoCliente.Length != 5 || !codigoCliente.All(char.IsLetterOrDigit))
            {
                return BadRequest(_responseApi.Msj(400, "Error", "El parámetro 'CodigoCliente' debe tener exactamente 5 caracteres alfanuméricos.", HttpContext, ""));

            }
            try
            {
                // Llamada al servicio para obtener la demora
                var resultado = await _clienteBusiness.Demora(codigoMesa, codigoCliente);

                // Verifica si el resultado es nulo o vacío
                if (resultado == null)
                {
                    return NotFound(_responseApi.Msj(404, "Error", "El parámetro 'CodigoCliente' debe tener exactamente 5 caracteres alfanuméricos.", HttpContext, ""));
                }

                return Ok(_responseApi.Msj(200, "Correcto", "Demora obtenida con éxito.", HttpContext, resultado));

            }

            catch (Exception ex)
            {

                return BadRequest(_responseApi.Msj(400, "Correcto", "Demora obtenida con éxito.", HttpContext, ""));
            }
        }
    }
}
