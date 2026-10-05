namespace Catalogo.Api.Models
{
    public class EstadoCosecha
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Cosecha> Cosechas { get; set; } = new HashSet<Cosecha>();
    }
}
