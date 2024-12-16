using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface IEmpleadoDataAccess
    {
        Task<Int32> AgregarAsync(Models.Empleado empleado);
        Task<List<Models.Empleado>> ListadoAsync(bool estado);
        Task<bool> ExisteAsync(Int32 codEmpleado);
        Task<Int32> ActualizarAsync(Models.Empleado empleado);
        Task<bool> ExisteLoginAsync(Models.Empleado empleado);
        Task<Models.Empleado> EmpleadoLoginAsync(Models.Empleado empleado);

        Task<bool> EliminarAsync(Int32 codEmpleado);
        Task UpdateAsync(Models.Empleado empleado);
        Task DeleteAsync(int id);
        Task<IEnumerable<ModelsDto.EmpleadosPorSectorResponseDto>> CantidadEmpleadosPorSector();
        Task<IEnumerable<ModelsDto.OperacionesPorSectorDto>> CantidadOperacionesPorSector(int idSector, DateTime? fechaInicio, DateTime? fechaFin);
        Task<IEnumerable<ModelsDto.OperacionesEmpleadoDto>> ObtenerTodasLasOperacionesEmpleados(DateTime? fechaInicio, DateTime? fechaFin);
    }
    public interface IEmpleadoBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.EmpleadoDto empleadoDto);
        Task<List<EmpleadoListDto>> ListadoAsync(bool estado);
        Task<bool> ExisteAsync(Int32 codEmpleado);
        Task<int> ActualizarAsync(EmpleadoDto empleadoDto, Int32 codEmpleado);
        Task<bool> ExisteLoginAsync(EmpleadoDto empleadoDto);
        Task<ModelsDto.EmpleadoListDto> EmpleadoLoginAsync(EmpleadoDto empleadoDto);
        Task<bool> EliminarAsync(Int32 codEmpleado);
        Task UpdateAsync(EmpleadoDto empleado);
        Task DeleteAsync(int id);
        Task<IEnumerable<ModelsDto.EmpleadosPorSectorResponseDto>> CantidadEmpleadosPorSector();
        Task<IEnumerable<ModelsDto.OperacionesPorSectorDto>> CantidadOperacionesPorSector(int idSector, DateTime? fechaInicio, DateTime? fechaFin);
        Task<IEnumerable<ModelsDto.OperacionesEmpleadoDto>> ObtenerTodasLasOperacionesEmpleados(DateTime? fechaInicio, DateTime? fechaFin);
    }
}
