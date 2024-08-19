using LaboAppWebV1._0._0.Enums;

namespace LaboAppWebV1._0._0.Models
{


    public class Empleado
    {
        public int ID_Empleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Enums.Rol Rol { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public DateTime? Fecha_Salida { get; set; }
        public string Estado { get; set; }
        public string Accion { get; set; }
    }
}
