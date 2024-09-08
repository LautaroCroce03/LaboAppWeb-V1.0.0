using LaboAppWebV1._0._0.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LaboAppWebV1._0._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaBusiness _comandaBusiness;
        private readonly IMesaBusiness _mesaBusiness;

        public ComandaController(IComandaBusiness comandaBusiness, IMesaBusiness mesaBusiness)
        {
            _comandaBusiness = comandaBusiness;
            _mesaBusiness = mesaBusiness;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ModelsDto.ComandaDto comanda)
        {
            try
            {

                if (comanda.Pedidos.Count.Equals(0))
                    return BadRequest("Sin pedido");

                if (!await _mesaBusiness.ExisteAsync(comanda.IdMesa))
                    return BadRequest("El numero de mesa ingresado no existe");

                var idComanda = await _comandaBusiness.AgregarAsync(comanda);
                if (idComanda > 0)
                {
                    return Ok($"Nro de pedido {idComanda}");
                }
                else
                {
                    return BadRequest("Por favor completar los campos");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{idComanda}")]
        public async Task<IActionResult> Get(Int32 idComanda)
        {
            try
            {
                if (idComanda <= 0) 
                {
                    return BadRequest();
                }       
                var _comanda = _comandaBusiness.ListadoAsync(idComanda);

                if (_comanda != null)
                {
                    return Ok(_comanda);
                }
                else 
                {
                    return BadRequest("Sin registros");
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {

                var _comanda = _comandaBusiness.ListadoAsync();

                if (_comanda != null)
                {
                    return Ok(_comanda);
                }
                else
                {
                    return BadRequest("Sin registros");
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
