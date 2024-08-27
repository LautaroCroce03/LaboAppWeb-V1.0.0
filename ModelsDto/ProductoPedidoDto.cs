using LaboAppWebV1._0._0.Models;

namespace LaboAppWebV1._0._0.ModelsDto
{
    public class ProductoPedidoDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        public ProductoDto Producto { get; set; }
        public PedidoDto Pedido { get; set; }
    }
}
