using Catalogo.Api.Dtos.Cosechas;
using Catalogo.Api.Interfaces.Cosechas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CosechaController : ControllerBase
    {
        private readonly ICosechaService _cosechaService;

        public CosechaController(ICosechaService cosechaService)
        {
            _cosechaService = cosechaService;
        }


        //metodo para refactorizar el codigo y obtener el token
        private int ObtenerUsuarioIdDelToken()
        {
            var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier)
         ?? throw new UnauthorizedAccessException("El token no contiene un identificador válido.");

            return int.Parse(claimValue);
        }
        

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<CosechaReadDto>> RegistrarCosecha(CosechaCreateDto cosechaCreateDto)
        {
            //le asigno el claim a la variable usuarioId
            int usuarioId = ObtenerUsuarioIdDelToken();
            
            //usamos el metodo del servicio para crear la cosecha
            var NuevaCosecha = await _cosechaService.CrearCosecha(usuarioId, cosechaCreateDto);

            return Ok(NuevaCosecha);
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<CosechaReadDto>>> VerTodasLasCosechas()
        {
            var usuarioId = ObtenerUsuarioIdDelToken();

            var cosechas = await _cosechaService.ObtenerCosechasDelUsuario(usuarioId);

            return Ok(cosechas);
        }

        [HttpGet("Catalogo")]
        public async Task<ActionResult<IEnumerable<CosechaReadDto>>> CatalogoCosechas([FromQuery] string? producto, [FromQuery] decimal? precioMin, [FromQuery] decimal? precioMax)
        {
            var cosechas = await _cosechaService.CatalogoDeCosechas(producto, precioMin, precioMax);

            return Ok(cosechas);
        }

    }
}
