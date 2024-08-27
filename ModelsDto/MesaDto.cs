namespace LaboAppWebV1._0._0.ModelsDto
{
    public class MesaDto
    {
        public int IdMesa { get; set; }
        public string CodigoMesa { get; set; } // Unique code of 5 characters
        public Enums.EstadoMesa Estado { get; set; } // Esperando pedido, Comiendo, Pagando, Cerrada
    }
}
