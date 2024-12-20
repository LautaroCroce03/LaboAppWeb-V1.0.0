﻿using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Mesa: IMesaBusiness
    {
        private ILogger<Mesa> _logger;
        private readonly IMesaDataAccess _mesaData;
        private readonly IMapper _mapper;
        private readonly IGenerar _generar;
        public Mesa(ILogger<Mesa> logger, IMesaDataAccess mesaData, IMapper mapper, IGenerar generar)
        {
            _logger = logger;
            _mesaData = mesaData;
            _mapper = mapper;
            _generar = generar;
        }

        public async Task<Int32> AgregarAsync(ModelsDto.MesaDto estadoMesa)
        {

            try
            {
                var _mesa = _mapper.Map<Models.Mesa>(estadoMesa);
                _mesa.Codigo = _generar.Codigo();
                return await _mesaData.AgregarAsync(_mesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<ModelsDto.MesaListDto>> ListadoAsync()
        {
            try
            {
                List<ModelsDto.MesaListDto> _mesaLists = new List<ModelsDto.MesaListDto>();

                var _result = await _mesaData.ListadoAsync();

                if (_result.Count > 0)
                {
                    _mesaLists = _mapper.Map<List<ModelsDto.MesaListDto>>(_result);

                    return _mesaLists;
                }
                else
                {
                    return _mesaLists;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task<bool> ExisteAsync(Int32 idMesa) 
        {
            try
            {
                return await _mesaData.ExisteAsync(idMesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteAsync");
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(ModelsDto.MesaListDto estadoMesa)
        {

            try
            {
                var _mesa = _mapper.Map<Models.Mesa>(estadoMesa);

                return await _mesaData.ActualizarAsync(_mesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ActualizarAsync");
                throw;
            }
        }

        public async Task UpdateAsync(MesaListDto mesaDto)
        {
            try
            {
                var _mesa = _mapper.Map<Models.Mesa>(mesaDto);

                await _mesaData.ActualizarAsync(_mesa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ActualizarAsync");

            }
        }

        public async Task DeleteAsync(int id) 
        {
            try
            {
                await _mesaData.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
