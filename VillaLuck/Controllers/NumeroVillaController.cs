using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaLuck.Modelos;
using VillaLuck.Modelos.Dto;
using VillaLuck.Repositorio.IRepositorio;


namespace VillaLuck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IVillaNumeroRepositorio _numeroVillaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, IVillaNumeroRepositorio numeroVillaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroVillaRepo = numeroVillaRepo;
            _mapper = mapper;
            _apiResponse = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {

            try
            {
                _logger.LogInformation("Obtener las Numerovillas");

                IEnumerable<NumeroVilla> numeroNillaList = await _numeroVillaRepo.ObtenerTodos();

                _apiResponse.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroNillaList);
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMensajes = new List<string>() { ex.ToString() };
                return _apiResponse;

            }


        }

        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int Id)
        {

            try
            {
                if (Id == 0)
                {
                    _logger.LogError("Error al traer Numero Villa con id " + Id);
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsExitoso = false;
                    return BadRequest(_apiResponse);
                }
                // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == Id);
                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNo == Id);
                if (numeroVilla == null)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsExitoso = true;
                _apiResponse.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMensajes = new List<string>() { ex.ToString() };
                return _apiResponse;
            }


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto villaCreateDto)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsExitoso = false;
                    return BadRequest(_apiResponse);
                }

                if (await _numeroVillaRepo.Obtener(v => v.VillaNo == villaCreateDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de villa  ya existe");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsExitoso = false;
                    return BadRequest(ModelState);
                }

                if(await _villaRepo.Obtener(v => v.Id == villaCreateDto.VillaNo) != null)
                {
                    ModelState.AddModelError("ClaveForanea", "El id de villa  no existe");
                }

                if (villaCreateDto == null) return BadRequest(villaCreateDto);

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(villaCreateDto);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _numeroVillaRepo.Crear(modelo);
                _apiResponse.Resultado = modelo;
                _apiResponse.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMensajes = new List<string>() { ex.ToString() };
                return _apiResponse;
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {

            try
            {

                if (id == 0)
                {
                    _logger.LogInformation($"{id}");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsExitoso = false;
                    return BadRequest(_apiResponse);
                }
                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNo == id);
                if (numeroVilla == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsExitoso = false;
                    return NotFound(_apiResponse);
                }


                await _numeroVillaRepo.Remover(numeroVilla);
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                _apiResponse.IsExitoso = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsExitoso = false;
                _apiResponse.ErrorMensajes = new List<string>() { ex.ToString() };
                return BadRequest(_apiResponse);
            }


        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto villaUpdateDto)
        {
            if (villaUpdateDto == null || id != villaUpdateDto.VillaNo)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                return BadRequest(_apiResponse);
            }

            if(await _villaRepo.Obtener(v => v.Id == villaUpdateDto.VillaId)==null){
                ModelState.AddModelError("ClaveForanea", "el id de la villa no existe");
                return BadRequest(ModelState);
            }

            //Con Mapper
            NumeroVilla modelo = _mapper.Map<NumeroVilla>(villaUpdateDto);
            modelo.FechaActualizacion = DateTime.Now;
            await _numeroVillaRepo.Actualizar(modelo);
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            _apiResponse.IsExitoso = true;
            return Ok(_apiResponse);

        }
    }
}
