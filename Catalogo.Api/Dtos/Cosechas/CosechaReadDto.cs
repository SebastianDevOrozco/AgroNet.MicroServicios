namespace Catalogo.Api.Dtos.Cosechas
{
    public class CosechaReadDto
    {
        public string UsuarioNombre { get; set; } = string.Empty;
        public string ProductoNombre { get; set; } = string.Empty;
        public string EstadoCosechaNombre { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal PrecioUnidad { get; set; }
        public DateTime FechaEstimado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
