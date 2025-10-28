using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tienda.src.Domain.Models;

namespace Tienda.src.Infrastructure.Data
{
    /// <summary>
    /// Contexto de datos para la aplicación, hereda de IdentityDbContext para manejar la identidad de usuarios.
    /// </summary>
//    public class DataContext : IdentityDbContext<User, Role, int>
//    {
//        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Definición de las tablas en la base de datos
//        public DbSet<Product> Products { get; set; }
//        public DbSet<Image> Images { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Brand> Brands { get; set; }
//        public DbSet<Order> Orders { get; set; }
//        public DbSet<OrderItem> OrderItems { get; set; }
//        public DbSet<Cart> Carts { get; set; }
//        public DbSet<CartItem> CartItems { get; set; }
//        public DbSet<VerificationCode> VerificationCodes { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

            // Configuración de las claves compuestas
//            modelBuilder.Entity<OrderItem>()
//                .HasKey(oi => new { oi.OrderId, oi.ProductId });

//            modelBuilder.Entity<CartItem>()
//                .HasKey(ci => new { ci.CartId, ci.ProductId });

            // Otras configuraciones de entidades
        }
//    }
//}
