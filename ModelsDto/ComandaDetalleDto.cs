namespace LaboAppWebV1._0._0.ModelsDto
{
    public class ComandaDetalleDto
    {
        public int IdComandas { get; set; }
        public string NombreCliente { get; set; }
        public int IdMesa { get; set; }
        public string Mesa { get; set; }

        public List<ModelsDto.PedidoListDto> Pedidos { get; set; }
    }
}
