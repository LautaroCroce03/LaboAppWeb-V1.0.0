using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class EstadoMesa : IEstadoMesaBusiness
    {
        private readonly IEstadoMesaDataAccess _dataAccess;

        public EstadoMesa(IEstadoMesaDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Int32> AgregarAsync(EstadoMesaDto estadoMesa)
        {

            try
            {
                var _estadoMesa = new Models.EstadoMesa();
                _estadoMesa.Descripcion = estadoMesa.Descripcion;
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
