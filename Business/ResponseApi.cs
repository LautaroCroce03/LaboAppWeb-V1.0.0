using LaboAppWebV1._0._0.IServices;

namespace LaboAppWebV1._0._0.Business
{
    public class ResponseApi: IResponseApi
    {
        private ILogger<ResponseApi> _logger;

        public ResponseApi(ILogger<ResponseApi> logger)
        {
            _logger = logger;
        }

        public ModelsDto.ResponseApiDto Msj(Int32 statusCodes, string Title, string Detail, HttpContext Instance, object obj)
        {
            try
			{
                return new ModelsDto.ResponseApiDto() 
                {
                    Status = statusCodes,
                    Title = Title,
                    Detail = Detail,
                    Instance = Instance.Request.Path,
                    Data = obj
                };
			}
			catch (Exception ex)
			{
                _logger.LogError(ex, "Msj");

                throw;
			}
        }
    }
}
