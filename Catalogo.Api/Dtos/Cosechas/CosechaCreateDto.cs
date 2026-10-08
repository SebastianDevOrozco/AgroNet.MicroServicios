using System.ComponentModel.DataAnnotations;

namespace Catalogo.Api.Dtos.Cosechas
{
    public class CosechaCreateDto
    {
        [Required(ErrorMessage = "El producto Es Obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El identificador del Producto no es válido.")]
        public int IdProducto { get; set; }


        [Required(ErrorMessage = "El Estado Es Obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El identificador del estado no es válido.")]
        public int IdEstadoCosecha { get; set; }

        [Required(ErrorMessage = "La cantidad disponible Es Obligatoria")]
        [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El precio por unidad Es Obligatorio")]
        [Range(typeof(decimal), "50", "100000000", ErrorMessage = "El precio debe ser mayor a 50 pesos.")]
        public decimal PrecioUnidad { get; set; }

        [Required(ErrorMessage = "La Fecha estimada Es Obligatoria")]
        public DateTime FechaEstimado { get; set; }
    }
}
