using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class EstadoMesa: IEstadoMesaDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public EstadoMesa(LaboAppWebV1Context laboAppWebV1Context)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<Int32> AgregarAsync(Models.EstadoMesa estadoMesa)
        {

            try
            {
                await _laboAppWebV1Context.EstadoMesas.AddAsync(estadoMesa);
                await _laboAppWebV1Context.SaveChangesAsync();

                return estadoMesa.IdEstado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Models.EstadoMesa>> ListadoAsync()
        {
            try
            {
                var result = await _laboAppWebV1Context.EstadoMesas.ToListAsync();

                if ((result != null) && (result.Count > 0))
                {
                    return result;
                }
                else
                {
                    return new List<Models.EstadoMesa>();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ExisteAsync(Int32 idEstado)
        {
            try
            {
                return await _laboAppWebV1Context.EstadoMesas.AnyAsync(x => x.IdEstado.Equals(idEstado));

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
