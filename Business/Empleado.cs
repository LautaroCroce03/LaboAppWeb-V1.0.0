using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Empleado: IEmpleadoBusiness
    {
		private readonly IEmpleadoDataAccess _empleadoData;

        public Empleado(IEmpleadoDataAccess empleadoData)
        {
            _empleadoData = empleadoData;
        }

        public async Task<int> AgregarAsync(EmpleadoDto empleadoDto)
        {
            try
            {
                var _emp = new Models.Empleado();
                _emp.Usuario = empleadoDto.Usuario;
                _emp.Nombre = empleadoDto.Nombre;
                _emp.IdRol = empleadoDto.IdRol;
                _emp.IdSector = empleadoDto.IdSector;
                _emp.Password = empleadoDto.Password;

                return await _empleadoData.AgregarAsync(_emp);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<EmpleadoListDto>> ListadoAsync()
        {
            try
            {
                List<EmpleadoListDto> empleadoDtos = new List<EmpleadoListDto>();

                var _list = await _empleadoData.ListadoAsync();

                if ((_list != null) && (_list.Count > 0))
                {
                    EmpleadoListDto empleadoDto;
                    foreach (var item in _list)
                    {
                        empleadoDto = new EmpleadoListDto();
                        empleadoDto.Nombre = item.Nombre;
                        empleadoDto.IdRol = item.IdRol;
                        empleadoDto.IdSector = item.IdSector;
                        empleadoDto.Usuario  = item.Usuario;
                        empleadoDto.IdEmpleado = item.IdEmpleado;
                        empleadoDtos.Add(empleadoDto);
                    }

                    return empleadoDtos;
                }
                else 
                {
                    return empleadoDtos;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
