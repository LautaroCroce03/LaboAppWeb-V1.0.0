using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Rol: IRolBusiness
    {
        private readonly IRolDataAccess _rolDataAccess;
        private readonly IMapper _mapper;
        public Rol(IRolDataAccess rolDataAccess, IMapper mapper)
        {
            _rolDataAccess = rolDataAccess;
            _mapper = mapper;
        }

        public async Task<Int32> AgregarAsync(ModelsDto.RolDto rolDto) 
        {
			try
			{
				var _rol = _mapper.Map<Models.Role>(rolDto);

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
                    rolListDtos = _mapper.Map<List<RolListDto>>(_list);

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

        public async Task<bool> ExisteId(int idRol)
        {
            try
            {
                return await _rolDataAccess.ExisteIdAsync(idRol);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
