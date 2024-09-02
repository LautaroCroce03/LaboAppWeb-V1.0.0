using LaboAppWebV1._0._0.IServices;

namespace LaboAppWebV1._0._0.Business
{
    public class Empleado
    {
		private readonly IEmpleadoDataAccess _empleadoData;

        public Empleado(IEmpleadoDataAccess empleadoData)
        {
            _empleadoData = empleadoData;
        }

        public async Task<Int32> Agregar(ModelsDto.EmpleadoDto empleado) 
        {
			try
			{
                var _emp = new Models.Empleado();
                _emp.Usuario = empleado.Usuario;
                _emp.Nombre = empleado.Nombre;
                _emp.IdRol = empleado.IdRol;
                _emp.IdSector = empleado.IdSector;

               return await  _empleadoData.AgregarAsync(_emp);

            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
