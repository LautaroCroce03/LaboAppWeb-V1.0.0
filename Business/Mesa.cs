﻿using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Mesa: IMesaBusiness
    {
        private readonly IMesaDataAccess _mesaData;
        private readonly IMapper _mapper;
        public Mesa(IMesaDataAccess mesaData, IMapper mapper)
        {
            _mesaData = mesaData;
            _mapper = mapper;
        }

        public async Task<Int32> AgregarAsync(ModelsDto.MesaDto estadoMesa)
        {

            try
            {
                var _mesa = _mapper.Map<Models.Mesa>(estadoMesa);

                return await _mesaData.AgregarAsync(_mesa);
            }
            catch (Exception)
            {

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
                    ModelsDto.MesaListDto mesaListDto;
                    foreach (var item in _result)
                    {
                        mesaListDto = new MesaListDto();
                        mesaListDto.IdMesa = item.IdMesa;
                        mesaListDto.Nombre = item.Nombre;
                        mesaListDto.IdEstado = item.IdEstado;
                        _mesaLists.Add(mesaListDto);
                    }

                    return _mesaLists;
                }
                else
                {
                    return _mesaLists;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ExisteAsync(Int32 idMesa) 
        {
            try
            {
                return await _mesaData.ExisteAsync(idMesa);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
