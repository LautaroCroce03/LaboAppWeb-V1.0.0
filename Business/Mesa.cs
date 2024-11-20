using AutoMapper;
using LaboAppWebV1._0._0.IServices;

namespace LaboAppWebV1._0._0.Business
{
    public class Mesa: IMesaBusiness
    {
        private ILogger<Mesa> _logger;
        private readonly IMesaDataAccess _mesaData;
        private readonly IMapper _mapper;

        public Mesa(ILogger<Mesa> logger, IMesaDataAccess mesaData, IMapper mapper)
        {
            _logger = logger;
            _mesaData = mesaData;
            _mapper = mapper;
        }

        public async Task<Int32> AgregarAsync(ModelsDto.MesaDto estadoMesa)
        {

            try
            {
                var _mesa = _mapper.Map<Models.Mesa>(estadoMesa);

                return await _mesaData.AgregarAsync(_mesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
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
                    _mesaLists = _mapper.Map<List<ModelsDto.MesaListDto>>(_result);

                    return _mesaLists;
                }
                else
                {
                    return _mesaLists;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task<bool> ExisteAsync(Int32 idMesa) 
        {
            try
            {
                return await _mesaData.ExisteAsync(idMesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteAsync");
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(ModelsDto.MesaListDto estadoMesa)
        {

            try
            {
                var _mesa = _mapper.Map<Models.Mesa>(estadoMesa);

                return await _mesaData.ActualizarAsync(_mesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ActualizarAsync");
                throw;
            }
        }
    }
}
