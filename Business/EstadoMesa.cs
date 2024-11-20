using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class EstadoMesa : IEstadoMesaBusiness
    {
        private ILogger<EstadoMesa> _logger;
        private readonly IEstadoMesaDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public EstadoMesa(ILogger<EstadoMesa> logger, IEstadoMesaDataAccess dataAccess, IMapper mapper)
        {
            _logger = logger;
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<Int32> AgregarAsync(EstadoMesaDto estadoMesa)
        {

            try
            {
                var _estadoMesa = _mapper.Map<Models.EstadoMesa>(estadoMesa);
                return await _dataAccess.AgregarAsync(_estadoMesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<EstadoMesaList>> ListadoAsync()
        {
            try
            {
                List<EstadoMesaList> estadoMesaLists = new List<EstadoMesaList>();

                var _result = await _dataAccess.ListadoAsync();

                if (_result.Count > 0)
                {
                    estadoMesaLists = _mapper.Map<List<EstadoMesaList>>(_result);

                    return estadoMesaLists;
                }
                else
                {
                    return estadoMesaLists;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }
        public async Task<bool> ExisteAsync(Int32 idEstado)
        {
            try
            {
                return await _dataAccess.ExisteAsync(idEstado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteAsync");
                throw;
            }
        }
    }
}
