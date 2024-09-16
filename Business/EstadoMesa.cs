using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class EstadoMesa : IEstadoMesaBusiness
    {
        private readonly IEstadoMesaDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public EstadoMesa(IEstadoMesaDataAccess dataAccess, IMapper mapper)
        {
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
            catch (Exception)
            {

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
                    //estadoMesaLists = _mapper.Map<List<EstadoMesaList>>(_result);
                    EstadoMesaList estadoMesaList;
                    foreach (var item in _result)
                    {
                        estadoMesaList = new EstadoMesaList();
                        estadoMesaList.IdEstado = item.IdEstado;
                        estadoMesaList.Descripcion = item.Descripcion;
                        estadoMesaLists.Add(estadoMesaList);
                    }

                    return estadoMesaLists;
                }
                else
                {
                    return estadoMesaLists;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
