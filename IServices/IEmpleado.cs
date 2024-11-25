﻿using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.IServices
{
    public interface IEmpleadoDataAccess
    {
        Task<Int32> AgregarAsync(Models.Empleado empleado);
        Task<List<Models.Empleado>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 codEmpleado);
        Task<Int32> ActualizarAsync(Models.Empleado empleado);
        Task<bool> ExisteLoginAsync(Models.Empleado empleado);
        Task<Models.Empleado> EmpleadoLoginAsync(Models.Empleado empleado);
    }
    public interface IEmpleadoBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.EmpleadoDto empleadoDto);
        Task<List<EmpleadoListDto>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 codEmpleado);
        Task<int> ActualizarAsync(EmpleadoDto empleadoDto, Int32 codEmpleado);
        Task<bool> ExisteLoginAsync(EmpleadoDto empleadoDto);
        Task<ModelsDto.EmpleadoListDto> EmpleadoLoginAsync(EmpleadoDto empleadoDto);
    }
}
