using AutoMapper;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.ModelsDto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LaboAppWebV1._0._0.Business
{
    public class TokenJWT : ITokenJWT
    {
        private readonly ILogger<TokenJWT> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEncriptar _encriptar;
        private readonly IMapper _mapper;

        public TokenJWT(ILogger<TokenJWT> logger, IConfiguration configuration,
                            IEncriptar encriptar, 
                            IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _encriptar = encriptar;
            _mapper = mapper;
        }

        public async Task<ModelsDto.TokenJwtDto> GenerarLoginAsync(ModelsDto.EmpleadoListDto usuario)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                DateTime creacionToken = DateTime.UtcNow;
                DateTime expiracionToken = DateTime.UtcNow.AddMinutes(3);
                DateTime expiracionRefreshToken = DateTime.UtcNow.AddMinutes(13);

                var _historia = new ModelsDto.TokenJwtDto
                {
                    Token = GenerarJwt(usuario, expiracionToken, "true", "false").Token
                };

                //var _result = await _historialRefToken.AgregarAsync(_historia);

                if (_historia != null)
                {
                    return new TokenJwtDto
                    {
                        Token = _historia.Token,
                        Expiration = expiracionToken.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    };
                }
                else
                {
                    return new TokenJwtDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GenerarLoginAsync");
                throw;
            }

        }

        public async Task<ModelsDto.TokenJwtDto> GenerarRefreshAsync(ModelsDto.EmpleadoListDto usuario)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                DateTime creacionToken = DateTime.UtcNow;
                DateTime expiracionToken = DateTime.UtcNow.AddMinutes(1);
                DateTime expiracionRefreshToken = DateTime.UtcNow.AddMinutes(10);

                var _historia = new ModelsDto.TokenJwtDto
                {
                    Token = GenerarJwt(usuario, expiracionToken, "true", "false").Token
                };


                if (_historia != null)
                {
                    return new TokenJwtDto
                    {
                        Token = _historia.Token,
                        Expiration = expiracionToken.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    };
                }
                else
                {
                    return new TokenJwtDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GenerarRefreshAsync");

                throw;
            }


        }
        private UserTokenDto GenerarJwt(ModelsDto.EmpleadoListDto usuarioLogin, DateTime expirationToken, string _token, string refreshToken)
        {
            try
            {
                string _grupo = string.Empty;




                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usuarioLogin.Nombre),
                    new Claim("IdLogin", usuarioLogin.IdEmpleado.ToString()),
                    new Claim("Nick", usuarioLogin.Usuario.ToString()),
                    new Claim("Id", usuarioLogin.IdEmpleado.ToString()),
                    new Claim(ClaimTypes.Role, usuarioLogin.Rol),
                    new Claim("SectorId", usuarioLogin.IdSector.ToString()),
                    new Claim("Token", _token),
                    new Claim("RefreshToken",refreshToken),
                    new Claim("web", "www.LaboAppWeb.com"),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //var expiration = DateTime.UtcNow.AddMinutes(1);// en dos minutos expira

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expirationToken,
                    signingCredentials: creds
                    );

                return new UserTokenDto()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expirationToken
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GenerarJwt");

                throw;
            }
        }
    }
}
