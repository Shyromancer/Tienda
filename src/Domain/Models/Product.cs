namespace Tienda.src.Domain.Models
{
    /// <summary>
    /// Modelo que representa un producto en el catálogo.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
//        public string Name { get; set; }

        /// <summary>
        /// Descripción del producto.
        /// </summary>
//        public string Description { get; set; }

        /// <summary>
        /// Precio del producto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Precio original antes de descuentos.
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// Descuento aplicable al producto.
        /// </summary>
        public decimal Discount { get; set; } = 0;

        /// <summary>
        /// Cantidad de productos disponibles en inventario.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// URL de la imagen del producto.
        /// </summary>
    //    public string ImageUrl { get; set; }

        /// <summary>
        /// Categoría a la que pertenece el producto.
        /// </summary>
    //    public string Category { get; set; }

        /// <summary>
        /// Estado del producto (Ejemplo: Nuevo, Usado).
        /// </summary>
    //    public string Condition { get; set; }

        /// <summary>
        /// Fecha de creación del producto.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicador de si el producto está disponible para la venta.
        /// </summary>
        public bool IsAvailable { get; set; } = true;
    }
}
