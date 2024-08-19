namespace LaboAppWebV1._0._0.Models
{
    public class Pedido
    {
        public int ID_Pedido { get; set; }
        public int ID_Comanda { get; set; }
        public string Codigo_Pedido { get; set; } // Alphanumeric code of 5 characters
        public Enums.EstadoPedido Estado { get; set; } // Pendiente, En preparación, Listo para servir
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Fecha_Finalizacion { get; set; }
        public TimeSpan Tiempo_Estimado { get; set; }
        public int ID_Mesa { get; set; }
        public int ID_Mozo { get; set; }

        public ICollection<ProductoPedido> ProductoPedidos { get; set; } // Relación uno a muchos con ProductoPedido
    }
}
