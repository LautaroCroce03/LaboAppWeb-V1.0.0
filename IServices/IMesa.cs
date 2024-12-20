﻿namespace LaboAppWebV1._0._0.IServices
{
    public interface IMesaDataAccess
    {
        Task<Int32> AgregarAsync(Models.Mesa mesa);
        Task<List<Models.Mesa>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 idMesa);
        Task<bool> ActualizarAsync(Models.Mesa _mesa);
        Task UpdateAsync(Models.Mesa mesa);
        Task DeleteAsync(int id);
    }
    public interface IMesaBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.MesaDto estadoMesa);
        Task<List<ModelsDto.MesaListDto>> ListadoAsync();
        Task<bool> ExisteAsync(Int32 idMesa);
        Task<bool> ActualizarAsync(ModelsDto.MesaListDto estadoMesa);

        Task UpdateAsync(ModelsDto.MesaListDto mesa);
        Task DeleteAsync(int id);
    }
}
