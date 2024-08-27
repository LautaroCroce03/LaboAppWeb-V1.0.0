namespace LaboAppWebV1._0._0.ModelsDto
{
    public class EmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Enums.Rol Rol { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string Estado { get; set; }
        public string Accion { get; set; }
    }
}
