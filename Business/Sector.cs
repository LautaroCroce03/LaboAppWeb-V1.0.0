using AutoMapper;
using LaboAppWebV1._0._0.IServices;

namespace LaboAppWebV1._0._0.Business
{
    public class Sector: ISectorBusiness
    {
		private readonly ISectorDataAccess _sectorDataAccess;
        private readonly IMapper _mapper;
        public Sector(ISectorDataAccess sectorDataAccess, IMapper mapper)
        {
            _sectorDataAccess = sectorDataAccess;
            _mapper = mapper;   
        }

        public async Task<Int32> AgregarAsync(ModelsDto.SectorDto sectorDto)
        {
            try
            {
				var _sector = _mapper.Map< Models.Sectore>(sectorDto);
                return await _sectorDataAccess.AgregarAsync(_sector);

            }
			catch (Exception)
			{

				throw;
			}
        }
        public async Task<List<ModelsDto.SectorListDto>> ListadoAsync()
        {
            try
            {
                List<ModelsDto.SectorListDto> sectorDtosList = new List<ModelsDto.SectorListDto>();

                var _sectorList = await _sectorDataAccess.ListadoAsync();


                if (_sectorList.Count > 0) 
                {

                    sectorDtosList = _mapper.Map<List<ModelsDto.SectorListDto>>(_sectorList);

                    return sectorDtosList;
                }

                return sectorDtosList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ExisteId(int idSector)
        {
            try
            {
                return await _sectorDataAccess.ExisteId(idSector);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
