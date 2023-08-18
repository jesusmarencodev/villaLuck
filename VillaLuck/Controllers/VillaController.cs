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
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _apiResponse = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<APIResponse>> GetVillas()
        {

            try
            {
                _logger.LogInformation("Obtener las villas");

                IEnumerable<Villa> villaList = await _villaRepo.ObtenerTodos();

                _apiResponse.Resultado = _mapper.Map<IEnumerable<VillaDto>>(villaList);
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

        [HttpGet("id:int", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int Id)
        {

            try
            {
                if (Id == 0)
                {
                    _logger.LogError("Error al traer Villa con id " + Id);
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsExitoso = false;
                    return BadRequest(_apiResponse);
                }
                // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == Id);
                var villa = await _villaRepo.Obtener(v => v.Id == Id);
                if (villa == null)
                {
                    _apiResponse.IsExitoso = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsExitoso = true;
                _apiResponse.Resultado = _mapper.Map<VillaDto>(villa);
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
        public async Task<ActionResult<APIResponse>> CrearVilla([FromBody] VillaCreateDto villaCreateDto)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsExitoso = false;
                    return BadRequest(_apiResponse);
                }

                if(await _villaRepo.Obtener(v=>v.Nombre.ToLower() == villaCreateDto.Nombre.ToLower()) != null)
                { 
                    ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                    return BadRequest(ModelState);  
                }
                if (villaCreateDto == null) return BadRequest(villaCreateDto);

                Villa modelo = _mapper.Map<Villa>(villaCreateDto);
                await _villaRepo.Crear(modelo);
                _apiResponse.Resultado = modelo;
                _apiResponse.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _apiResponse);
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
        public async Task<IActionResult> DeleteVilla(int id)
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
                var villa = await _villaRepo.Obtener(v=> v.Id == id);
                if (villa == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsExitoso = false;
                    return NotFound(_apiResponse);
                }
                    
 
                //VillaStore.villaList.Remove(villa);
                await _villaRepo.Remover(villa);
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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            if(villaUpdateDto == null || id != villaUpdateDto.Id)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                return BadRequest(_apiResponse);
            }

            //Con Mapper
            Villa modelo = _mapper.Map<Villa>(villaUpdateDto);
            await _villaRepo.Actualizar(modelo);
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            _apiResponse.IsExitoso = true;
            return Ok(_apiResponse);

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async  Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                return BadRequest(_apiResponse);
            }

            var villa = await _villaRepo.Obtener(v => v.Id == id, traked:false);

  

            //Con mapper
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(patchDto);

            if (villa == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsExitoso = false;
                return BadRequest(_apiResponse);
            }


            patchDto.ApplyTo(villaDto, ModelState);
            if(!ModelState.IsValid) return BadRequest(ModelState);

            //Con mapper
            Villa modelo = _mapper.Map<Villa>(villaDto);

            await _villaRepo.Actualizar(modelo);
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            _apiResponse.IsExitoso = true;
            return Ok(_apiResponse);
        }
    }
}
