namespace LaboAppWebV1._0._0.IServices
{
    public interface IEncriptar
    {
        string Entrada(string clave);
        string ComputeHash(string password, string salt, string pepper, int iteration);
    }
}
