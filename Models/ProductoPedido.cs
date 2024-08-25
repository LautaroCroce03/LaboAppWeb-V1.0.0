namespace LaboAppWebV1._0._0.Models
{
    public class ProductoPedido
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        public Producto Producto { get; set; }
        public Pedido Pedido { get; set; }
    }
}
