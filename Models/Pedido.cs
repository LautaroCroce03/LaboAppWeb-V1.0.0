namespace LaboAppWebV1._0._0.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdComanda { get; set; }
        public string CodigoPedido { get; set; }
        public Enums.EstadoPedido Estado { get; set; } // Pendiente, En preparación, Listo para servir
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public TimeSpan TiempoEstimado { get; set; }
        public int IdMesa { get; set; }
        public int IdMozo { get; set; }

        public ICollection<ProductoPedido> ProductoPedidos { get; set; } // Relación uno a muchos con ProductoPedido
    }
}
