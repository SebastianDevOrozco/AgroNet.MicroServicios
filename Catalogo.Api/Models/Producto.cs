namespace Catalogo.Api.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public int IdUnidadMedida { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion {  get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;

        public Categoria categoria { get; set; } = null!;
        public UnidadMedida unidadMedida { get; set; } = null!;
        public ICollection<Cosecha> cosechas { get; set; } = new HashSet<Cosecha>();

    }
}
