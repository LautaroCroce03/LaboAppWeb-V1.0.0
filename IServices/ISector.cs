﻿namespace LaboAppWebV1._0._0.IServices
{
    public interface ISectorDataAccess
    {
        Task<Int32> AgregarAsync(Models.Sectore sectore);
        Task<List<Models.Sectore>> ListadoAsync();
        Task<bool> ExisteId(Int32 idSector);
    }
    public interface ISectorBusiness
    {
        Task<Int32> AgregarAsync(ModelsDto.SectorDto sectorDto);
        Task<List<ModelsDto.SectorListDto>> ListadoAsync();
        Task<bool> ExisteId(Int32 idSector);
    }
}
