using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Rol: IRolBusiness
    {
        private readonly IRolDataAccess _rolDataAccess;

        public Rol(IRolDataAccess rolDataAccess)
        {
            _rolDataAccess = rolDataAccess;
        }

        public async Task<Int32> AgregarAsync(ModelsDto.RolDto rolDto) 
        {
			try
			{
				var _rol = new Models.Role();
                _rol.Descripcion = rolDto.Descripcion;

                return await _rolDataAccess.AgregarAsync(_rol);

            }
			catch (Exception)
			{

				throw;
			}
        }

        public async Task<List<RolListDto>> ListadoAsync()
        {
            try
            {
                List<RolListDto> rolListDtos = new List<RolListDto>();
                var _list = await _rolDataAccess.ListadoAsync();

                if ((_list != null) && (_list.Count > 0))
                {
                    RolListDto rolListDto;
                    foreach (var item in _list)
                    {
                        rolListDto = new RolListDto();

                        rolListDto.Descripcion = item.Descripcion;
                        rolListDto.IdRol = item.IdRol;
                        rolListDtos.Add(rolListDto);
                    }

                    return rolListDtos;
                }
                else 
                {
                    return rolListDtos;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
