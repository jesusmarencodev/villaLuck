using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaLuck.Modelos;
using VillaLuck.Modelos.Dto;
using VillaLuck.Repositorio.IRepositorio;

namespace VillaLuck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private APIResponse _apiResponse;
        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _apiResponse = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto modelo)
        {
            var loginResponse = await _usuarioRepo.Login(modelo);
            if(loginResponse.Usuario == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMensajes.Add("UserName o Password son Incorrectos");

                return BadRequest(_apiResponse);
            }

            _apiResponse.IsExitoso = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.Resultado = loginResponse;
            return Ok(_apiResponse);

        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDto modelo)
        {
            bool isUsuario = await _usuarioRepo.IsUsuarioUnico(modelo.UserName);
            if (!isUsuario)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMensajes.Add("UserName existe");

                return BadRequest(_apiResponse);
            }

            var usuario = await _usuarioRepo.Registrar(modelo);
            if (usuario == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMensajes.Add("Error al Registrar");

                return BadRequest(_apiResponse);
            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsExitoso = true;
            return Ok(usuario);


        }
    }
}
