namespace LaboAppWebV1._0._0.ModelsDto
{
    public class PedidoListDto
    {
        public int IdPedido { get; set; }
        public int Cantidad { get; set; }
        public int IdProducto { get; set; }
        public string ProductoDescripcion { get; set; }
        public int IdEstadoPedido { get; set; }
        public string EstadoDescripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
    }
}
