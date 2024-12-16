using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface ICliente
    {
        Task<ClienteResponseDto> Demora(string codigoMesa, string idPedido);
    }
    public interface IClienteBusiness
    {
        Task<ClienteResponseDto> Demora(string codigoMesa, string idPedido);
    }
}
