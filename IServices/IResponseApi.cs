namespace LaboAppWebV1._0._0.IServices
{
    public interface IResponseApi
    {
        ModelsDto.ResponseApiDto Msj(Int32 statusCodes, string Title, string Detail, HttpContext Instance, object obj);
    }
}
