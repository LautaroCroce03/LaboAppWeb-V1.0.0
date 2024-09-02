namespace LaboAppWebV1._0._0.IServices
{
    public interface IEmpleadoDataAccess
    {
        Task<Int32> AgregarAsync(Models.Empleado empleado);
        Task<List<Models.Empleado>> Listado();
    }
}
