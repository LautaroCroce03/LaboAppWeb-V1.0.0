using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Comanda: IComandaDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public Comanda(LaboAppWebV1Context laboAppWebV1Context)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<Int32> AgregarAsync(Models.Comanda comanda) 
        {
            try
            {
                await _laboAppWebV1Context.Comandas.AddAsync(comanda);
                await _laboAppWebV1Context.SaveChangesAsync();
                return comanda.IdComandas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ModelsDto.ComandaDetalleDto> ListadoAsync(Int32 idComanda)
        {
            try
            {
                var _comanda = from c in _laboAppWebV1Context.Comandas join m in _laboAppWebV1Context.Mesas on c.IdMesa equals m.IdMesa
                               where c.IdComandas == idComanda
                               select new ModelsDto.ComandaDetalleDto()
                               {
                                    IdComandas = c.IdComandas,
                                    IdMesa = m.IdMesa,
                                    Mesa = m.Nombre,
                                    NombreCliente = c.NombreCliente
                               };

                return await _comanda.FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ModelsDto.ComandaDetalleDto>> ListadoAsync()
        {
            try
            {
                var _comanda = from c in _laboAppWebV1Context.Comandas
                               join m in _laboAppWebV1Context.Mesas on c.IdMesa equals m.IdMesa
                               select new ModelsDto.ComandaDetalleDto()
                               {
                                   IdComandas = c.IdComandas,
                                   IdMesa = m.IdMesa,
                                   Mesa = m.Nombre,
                                   NombreCliente = c.NombreCliente
                               };

                return await _comanda.ToListAsync();
            }
            catch (TaskCanceledException ex)
            {
                // Manejar el caso de tiempo de espera o tarea cancelada
                throw new Exception("La operación fue cancelada o expiró el tiempo de espera.", ex);
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción
                throw new Exception("Ocurrió un error al obtener el listado de comandas.", ex);
            }
        }
        public async Task<bool> EliminarAsync(int idComanda)
        {
            try
            {
                var comanda = await _laboAppWebV1Context.Comandas.FindAsync(idComanda);
                if (comanda != null)
                {
                    _laboAppWebV1Context.Comandas.Remove(comanda);
                    await _laboAppWebV1Context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la comanda", ex);
            }
        }
    }
}
