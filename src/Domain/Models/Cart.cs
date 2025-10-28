namespace Tienda.src.Domain.Models
{
    /// <summary>
    /// Modelo que representa un carrito de compras de un usuario.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Identificador único del carrito.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador del usuario propietario del carrito.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lista de productos en el carrito.
        /// </summary>
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        /// <summary>
        /// Total calculado para los productos en el carrito.
        /// </summary>
        public decimal Total => Items.Sum(item => item.Subtotal);

        /// <summary>
        /// Fecha de creación del carrito.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Método de pago seleccionado para el carrito.
        /// </summary>
//        public string PaymentMethod { get; set; }

        /// <summary>
        /// Estado del carrito (Ejemplo: Confirmado, Pendiente).
        /// </summary>
        public string Status { get; set; } = "Pendiente";
    }

    /// <summary>
    /// Representa un producto en el carrito.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Identificador del producto.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Cantidad de este producto en el carrito.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio del producto en el carrito.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Subtotal del producto en el carrito (Precio * Cantidad).
        /// </summary>
        public decimal Subtotal => Price * Quantity;
    }
}
