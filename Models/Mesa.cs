using LaboAppWebV1._0._0.Enums;

namespace LaboAppWebV1._0._0.Models
{
    public class Mesa
    {
        public int ID_Mesa { get; set; }
        public string Codigo_Mesa { get; set; } // Unique code of 5 characters
        public Enums.EstadoMesa Estado{ get; set; } // Esperando pedido, Comiendo, Pagando, Cerrada
    }
}
