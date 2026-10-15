using Catalogo.Api.Dtos.Cosechas;

namespace Catalogo.Api.Interfaces.Cosechas
{
    public interface ICosechaService
    {

        //Crear
        Task<CosechaReadDto> CrearCosecha(int usuarioId, CosechaCreateDto cosechaCreateDto);

        //Leer
        Task<IEnumerable<CosechaReadDto>> ObtenerCosechasDelUsuario(int usuarioId);
        Task<IEnumerable<CosechaReadDto>> CatalogoDeCosechas(string? producto, decimal? precioMin, decimal? precioMax);
    }
}
