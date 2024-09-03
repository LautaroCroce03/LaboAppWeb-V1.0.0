using LaboAppWebV1._0._0.IServices;

namespace LaboAppWebV1._0._0.Business
{
    public class Sector: ISectorBusiness
    {
		private readonly ISectorDataAccess _sectorDataAccess;

        public Sector(ISectorDataAccess sectorDataAccess)
        {
            _sectorDataAccess = sectorDataAccess;
        }

        public async Task<Int32> AgregarAsync(ModelsDto.SectorDto sectorDto) 
        {
			try
			{
				var _sector = new Models.Sectore();
                _sector.Descripcion = sectorDto.Descripcion;

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
                    ModelsDto.SectorListDto sectorDto;
                    foreach (var item in _sectorList)
                    {
                        sectorDto = new ModelsDto.SectorListDto();
                        sectorDto.Descripcion = item.Descripcion;
                        sectorDto.IdSector = item.IdSector;
                        sectorDtosList.Add(sectorDto);
                    }

                    return sectorDtosList;
                }

                return new List<ModelsDto.SectorListDto>();

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
