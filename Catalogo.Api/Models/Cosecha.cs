namespace Catalogo.Api.Models
{
    public class Cosecha
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdEstadoCosecha { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnidad { get; set; }
        public DateTime FechaEstimado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Producto Producto { get; set; } = null!;
        public EstadoCosecha EstadoCosecha = null!;
        


    }
}
