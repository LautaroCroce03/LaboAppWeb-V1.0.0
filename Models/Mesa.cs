using LaboAppWebV1._0._0.Enums;

namespace LaboAppWebV1._0._0.Models
{
    public class Mesa
    {
        public int IdMesa { get; set; }
        public string CodigoMesa { get; set; } // Unique code of 5 characters
        public Enums.EstadoMesa Estado{ get; set; } // Esperando pedido, Comiendo, Pagando, Cerrada
    }
}
