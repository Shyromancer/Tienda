namespace Tienda.src.Domain.Models
{
    /// <summary>
    /// Modelo que representa un pedido realizado por un usuario.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Identificador único del pedido.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó el pedido.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lista de productos del pedido.
        /// </summary>
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        /// <summary>
        /// Total del pedido (con descuentos aplicados).
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Dirección de envío del pedido.
        /// </summary>
//        public string ShippingAddress { get; set; }

        /// <summary>
        /// Fecha de creación del pedido.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Estado del pedido (Ejemplo: Pendiente, Completado).
        /// </summary>
        public string Status { get; set; } = "Pendiente";
    }

    /// <summary>
    /// Representa un producto dentro de un pedido.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Identificador del producto.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Cantidad de este producto en el pedido.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio del producto en el momento del pedido.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Subtotal de este producto (Precio * Cantidad).
        /// </summary>
        public decimal Subtotal => Price * Quantity;
    }
}
