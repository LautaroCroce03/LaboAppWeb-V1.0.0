using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Business
{
    public class Empleado: IEmpleadoBusiness
    {
        private ILogger<Empleado> _logger;
        private readonly IEmpleadoDataAccess _empleadoData;
        private readonly IMapper _mapper;
        private readonly IEncriptar _encriptar;

        public Empleado(ILogger<Empleado> logger, IEmpleadoDataAccess empleadoData, IMapper mapper, IEncriptar encriptar)
        {
            _logger = logger;
            _empleadoData = empleadoData;
            _mapper = mapper;
            _encriptar = encriptar;
        }

        public async Task<int> AgregarAsync(EmpleadoDto empleadoDto)
        {
            try
            {
                var _empleado = _mapper.Map<Models.Empleado>(empleadoDto);
                _empleado.Password = _encriptar.Entrada(_empleado.Password);

                return await _empleadoData.AgregarAsync(_empleado);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public async Task<List<EmpleadoListDto>> ListadoAsync(bool estado)
        {
            try
            {
                List<EmpleadoListDto> empleadoDtos = new List<EmpleadoListDto>();

                var _list = await _empleadoData.ListadoAsync(estado);

                if ((_list != null) && (_list.Count > 0))
                {
                    EmpleadoListDto empleadoDto;
                    foreach (var item in _list)
                    {
                        empleadoDto = new EmpleadoListDto();
                        empleadoDto.Nombre = item.Nombre;
                        empleadoDto.IdRol = item.IdRol;
                        empleadoDto.IdSector = item.IdSector;
                        empleadoDto.Usuario  = item.Usuario;
                        empleadoDto.IdEmpleado = item.IdEmpleado;
                        empleadoDto.Rol = item.IdRolNavigation.Descripcion;
                        empleadoDtos.Add(empleadoDto);
                    }

                    return empleadoDtos;
                }
                else 
                {
                    return empleadoDtos;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListadoAsync");
                throw;
            }
        }

        public async Task<bool> ExisteAsync(Int32 codEmpleado)
        {
            try
            {
                return await _empleadoData.ExisteAsync(codEmpleado);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteAsync");
                throw;
            }
        }

        public async Task<int> ActualizarAsync(EmpleadoDto empleadoDto, Int32 codEmpleado)
        {
            try
            {
                var _empleado = _mapper.Map<Models.Empleado>(empleadoDto);
                _empleado.IdEmpleado = codEmpleado;

                return await _empleadoData.AgregarAsync(_empleado);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ActualizarAsync");
                throw;
            }
        }

        public async Task<bool> ExisteLoginAsync(EmpleadoDto empleadoDto)
        {
            try
            {
                var _empleado = _mapper.Map<Models.Empleado>(empleadoDto);
                _empleado.Password = _encriptar.Entrada(_empleado.Password);
                return await _empleadoData.ExisteLoginAsync(_empleado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExisteLoginAsync");
                throw;
            }
        }

        public async Task<EmpleadoListDto> EmpleadoLoginAsync(EmpleadoDto empleadoDto)
        {
            try
            {
                var _empleado = _mapper.Map<Models.Empleado>(empleadoDto);
                _empleado.Password = _encriptar.Entrada(_empleado.Password);
                var _empleadoLogin = await _empleadoData.EmpleadoLoginAsync(_empleado);

                if (_empleadoLogin != null)
                {
                    var _loginEmpleado = _mapper.Map<ModelsDto.EmpleadoListDto>(_empleadoLogin);
                    _loginEmpleado.Rol = _empleadoLogin.IdRolNavigation.Descripcion;

                    return _loginEmpleado;
                }
                else 
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EmpleadoLoginAsync");
                throw;
            }
        }

        public async Task<bool> EliminarAsync(int codEmpleado)
        {
            try
            {
                if (await _empleadoData.ExisteAsync(codEmpleado))
                {
                    return await _empleadoData.EliminarAsync(codEmpleado);
                }
                else
                {
                    _logger.LogWarning($"Empleado con ID {codEmpleado} no existe.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EliminarAsync");
                throw;
            }

        }
        public async Task UpdateAsync(EmpleadoDto empleado)
        {
            try
            {
                var _empleado = _mapper.Map<Models.Empleado>(empleado);

                await _empleadoData.UpdateAsync(_empleado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EmpleadoLoginAsync");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _empleadoData.DeleteAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EmpleadosPorSectorResponseDto>> CantidadEmpleadosPorSector()
        {
            try
            {
                return await _empleadoData.CantidadEmpleadosPorSector();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OperacionesPorSectorDto>> CantidadOperacionesPorSector(int idSector, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                return await _empleadoData.CantidadOperacionesPorSector(idSector, fechaInicio, fechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

       
