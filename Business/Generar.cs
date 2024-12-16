using LaboAppWebV1._0._0.IServices;

namespace LaboAppWebV1._0._0.Business
{
    public class Generar: IGenerar
    {
        public string Codigo()
        {
            try
            {
                string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                return new string(Enumerable.Repeat(caracteres, 5)
                                            .Select(s => s[random.Next(s.Length)])
                                            .ToArray());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
