using LaboAppWebV1._0._0.IServices;
using System.Security.Cryptography;
using System.Text;

namespace LaboAppWebV1._0._0.Business
{
    public class Encriptar : IEncriptar
    {
        private ILogger<Encriptar> _logger;

        public Encriptar(ILogger<Encriptar> logger)
        {
            _logger = logger;
        }

        public string Entrada(string clave)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    // Send a sample text to hash.  
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                    // Get the hashed string.  
                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    // Print the string.   
                    return hash;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public string ComputeHash(string password, string salt, string pepper, int iteration)
        {

            try
            {
                if (iteration <= 0) return password;

                using var sha256 = SHA256.Create();
                var passwordSaltPepper = $"{password}{salt}{pepper}";
                var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
                var byteHash = sha256.ComputeHash(byteValue);
                var hash = Convert.ToBase64String(byteHash);
                return ComputeHash(hash, salt, pepper, iteration - 1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }

        public string GenerateSalt()
        {

            try
            {
                using var rng = RandomNumberGenerator.Create();
                var byteSalt = new byte[16];
                rng.GetBytes(byteSalt);
                var salt = Convert.ToBase64String(byteSalt);
                return salt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AgregarAsync");
                throw;
            }
        }
    }
}
