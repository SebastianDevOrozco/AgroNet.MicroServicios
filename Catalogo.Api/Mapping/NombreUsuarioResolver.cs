using AutoMapper;
using Catalogo.Api.Dtos.Cosechas;
using Catalogo.Api.Models;
using System.Security.Claims;

namespace Catalogo.Api.Mapping
{
    public class NombreUsuarioResolver : IValueResolver<object, CosechaReadDto, string?>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NombreUsuarioResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Resolve(object source, CosechaReadDto destination, string? destMember, ResolutionContext context)
        {
           var nombre = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            
            return string.IsNullOrWhiteSpace(nombre) ? "Usuario Desconocido" : nombre;
        }
    }
}
