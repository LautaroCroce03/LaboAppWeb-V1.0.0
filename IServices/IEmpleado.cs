using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface IEmpleadoDataAccess
    {
        Task<Int32> AgregarAsync(Models.Empleado empleado);
        Task<List<Models.Empleado>> ListadoAsync();
    }
    public interface IEmpleadoBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.EmpleadoDto empleadoDto);
        Task<List<EmpleadoListDto>> ListadoAsync();
    }
}
