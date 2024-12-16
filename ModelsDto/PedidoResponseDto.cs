namespace LaboAppWebV1._0._0.ModelsDto
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public string CodigoCliente { get; set; }
        public string? CodigoMesa { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string? Observaciones { get; set; }


    }
}
