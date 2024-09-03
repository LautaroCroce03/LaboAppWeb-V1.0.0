using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Mesa: IMesaBusiness
    {
        private readonly IMesaDataAccess _mesaData;

        public Mesa(IMesaDataAccess mesaData)
        {
            _mesaData = mesaData;
        }

        public async Task<Int32> AgregarAsync(ModelsDto.MesaDto estadoMesa)
        {

            try
            {
                var _mesa = new Models.Mesa();
                _mesa.IdEstado = estadoMesa.IdEstado;
                _mesa.Nombre = estadoMesa.Nombre;

                return await _mesaData.AgregarAsync(_mesa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ModelsDto.MesaListDto>> ListadoAsync()
        {
            try
            {
                List<ModelsDto.MesaListDto> _mesaLists = new List<ModelsDto.MesaListDto>();

                var _result = await _mesaData.ListadoAsync();

                if (_result.Count > 0)
                {
                    ModelsDto.MesaListDto mesaListDto;
                    foreach (var item in _result)
                    {
                        mesaListDto = new MesaListDto();
                        mesaListDto.IdMesa = item.IdMesa;
                        mesaListDto.Nombre = item.Nombre;
                        mesaListDto.IdEstado = item.IdEstado;
                        _mesaLists.Add(mesaListDto);
                    }

                    return _mesaLists;
                }
                else
                {
                    return _mesaLists;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
