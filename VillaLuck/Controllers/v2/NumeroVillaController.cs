using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaLuck.Modelos;
using VillaLuck.Modelos.Dto;
using VillaLuck.Repositorio.IRepositorio;


namespace VillaLuck.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
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
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "valir1", "valor2" };
        }

       
    }
}
