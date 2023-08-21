using VillaLuck.Modelos;
using VillaLuck.Modelos.Dto;

namespace VillaLuck.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<bool> IsUsuarioUnico(string userName);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<Usuario> Registrar(RegistroRequestDto registroRequestDto);
    }
}
