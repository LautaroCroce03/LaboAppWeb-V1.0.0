using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Mesa
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public async Task<Int32> AgregarAsync(Models.Mesa mesa) 
        {

            try
            {
                await _laboAppWebV1Context.AddAsync(mesa);
                await _laboAppWebV1Context.SaveChangesAsync();

                return mesa.IdMesa;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Models.Mesa>> Listado()
        {
            try
            {
                var result = await _laboAppWebV1Context.Mesas.ToListAsync();

                if ((result != null) && (result.Count > 0)) 
                {
                    return result;
                }
                else 
                {
                    return new List<Models.Mesa>();
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
