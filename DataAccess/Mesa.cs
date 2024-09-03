﻿using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{
    public class Mesa: IMesaDataAccess
    {
        private readonly LaboAppWebV1Context _laboAppWebV1Context;

        public Mesa(LaboAppWebV1Context laboAppWebV1Context)
        {
            _laboAppWebV1Context = laboAppWebV1Context;
        }

        public async Task<Int32> AgregarAsync(Models.Mesa mesa) 
        {

            try
            {
                await _laboAppWebV1Context.Mesas.AddAsync(mesa);
                await _laboAppWebV1Context.SaveChangesAsync();

                return mesa.IdMesa;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Models.Mesa>> ListadoAsync()
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