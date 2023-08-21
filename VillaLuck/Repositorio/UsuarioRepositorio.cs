using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VillaLuck.Datos;
using VillaLuck.Modelos;
using VillaLuck.Modelos.Dto;
using VillaLuck.Repositorio.IRepositorio;

namespace VillaLuck.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AplicationDbContext _db;
        private readonly string secretKey;

        public UsuarioRepositorio(AplicationDbContext db, IConfiguration conf)
        {
            _db = db;
            secretKey = conf.GetValue<string>("ApiSettings:Secret");
        }

        public DateTime Expires { get; private set; }

        public async Task<bool> IsUsuarioUnico(string userName)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            if(usuario == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower() &&
                                                                      u.Password == loginRequestDto.Password );
            if(usuario == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            //generate token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = usuario,
            };

            return loginResponseDto;
        }

        public async Task<Usuario> Registrar(RegistroRequestDto registroRequestDto)
        {
            Usuario usuario = new()
            {
                UserName = registroRequestDto.UserName,
                Password = registroRequestDto.Password,
                Nombres = registroRequestDto.Nombres,
                Role = registroRequestDto.Role
            };

            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = "";
            return usuario;
        }
    }
}
