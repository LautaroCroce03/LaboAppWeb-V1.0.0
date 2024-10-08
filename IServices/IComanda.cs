﻿namespace LaboAppWebV1._0._0.IServices
{
    public interface IComandaDataAccess
    {
        Task<Int32> AgregarAsync(Models.Comanda comanda);
        Task<ModelsDto.ComandaDetalleDto> ListadoAsync(Int32 idComanda);
        Task<List<ModelsDto.ComandaDetalleDto>> ListadoAsync();
    }
    public interface IComandaBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.ComandaDto comanda);
        Task<ModelsDto.ComandaDetalleDto> ListadoAsync(Int32 idComanda);
        Task<List<ModelsDto.ComandaDetalleDto>> ListadoAsync();
    }
}
