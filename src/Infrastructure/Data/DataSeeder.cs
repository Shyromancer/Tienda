using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tienda.src.Domain.Models;

namespace Tienda_UCN_api.src.Infrastructure.Data
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
//                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
//                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

//                await context.Database.MigrateAsync();

                // Crear roles
//                if (!context.Roles.Any())
                {
//                    var roles = new List<Role>
                    {
//                        new Role { Name = "Admin", NormalizedName = "ADMIN" },
//                        new Role { Name = "Customer", NormalizedName = "CUSTOMER" }
                    };
//                    foreach (var role in roles)
                    {
//                        var result = await roleManager.CreateAsync(role);
//                        if (!result.Succeeded)
                        {
//                            Log.Error("Error creando rol {RoleName}: {Errors}", role.Name, string.Join(", ", result.Errors.Select(e => e.Description)));
//                            throw new InvalidOperationException($"No se pudo crear el rol {role.Name}.");
                        }
                    }
                    Log.Information("Roles creados con éxito.");
                }

                // Crear productos
//                if (!await context.Products.AnyAsync())
                {
                    var productFaker = new Faker<Product>()
//                        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
//                        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(10, 1000))) // Convertimos a decimal
                        .RuleFor(p => p.Stock, f => f.Random.Int(1, 100))
//                        .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
//                        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
//                        .RuleFor(p => p.Condition, f => f.PickRandom("Nuevo", "Usado"))
                        .RuleFor(p => p.OriginalPrice, f => decimal.Parse(f.Commerce.Price(10, 1000))) // Convertimos a decimal
                        .RuleFor(p => p.Discount, f => f.Random.Number(0, 50));

                    var products = productFaker.Generate(10);
//                    await context.Products.AddRangeAsync(products);
//                    await context.SaveChangesAsync();
                    Log.Information("Productos creados con éxito.");
                }

                // Crear usuarios
//                if (!await context.Users.AnyAsync())
                {
//                    var customerRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
//                    var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

                    // Crear usuario admin
                    var adminUser = new User
                    {
//                        FirstName = "Admin",
//                        LastName = "User",
                        Email = "admin@tienda.com",
                        EmailConfirmed = true,
                        UserName = "admin@tienda.com",
//                        Gender = Gender.Masculino,
//                        Rut = "12345678-9",
                        BirthDate = new DateTime(1990, 1, 1),
                        PhoneNumber = "+56912345678"
                    };

                    var adminPassword = "Admin@123";
                    var adminResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (adminResult.Succeeded)
                    {
//                        await userManager.AddToRoleAsync(adminUser, adminRole.Name);
                        Log.Information("Usuario administrador creado con éxito.");
                    }
                    else
                    {
                        Log.Error("Error creando usuario administrador: {Errors}", string.Join(", ", adminResult.Errors.Select(e => e.Description)));
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al inicializar la base de datos.");
            }
        }
    }
}
