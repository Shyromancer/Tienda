using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Tienda.src.Domain.Models;

namespace Tienda.src.Infrastructure.Data
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                await context.Database.EnsureCreatedAsync();
                await context.Database.MigrateAsync();

                // Creación de roles
                if (!context.Roles.Any())
                {
                    var roles = new List<IdentityRole<int>>
                    {
                        new IdentityRole<int> { Name = "Admin", NormalizedName = "ADMIN" },
                        new IdentityRole<int> { Name = "Customer", NormalizedName = "CUSTOMER" }
                    };
                    foreach (var role in roles)
                    {
                        var result = await roleManager.CreateAsync(role);
                        if (!result.Succeeded)
                        {
                            Log.Error("Error creando rol {RoleName}: {Errors}", role.Name, string.Join(", ", result.Errors.Select(e => e.Description)));
                            throw new InvalidOperationException($"No se pudo crear el rol {role.Name}.");
                        }
                    }
                    Log.Information("Roles creados con éxito.");
                }

                // Creación de categorías
                if (!context.Categories.Any())
                {
                    var categories = new List<Category>
                    {
                        new Category { Name = "Electronics" },
                        new Category { Name = "Clothing" },
                        new Category { Name = "Home Appliances" },
                        new Category { Name = "Books" },
                        new Category { Name = "Sports" }
                    };
                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                    Log.Information("Categorías creadas con éxito.");
                }

                // Creación de marcas
                if (!await context.Brands.AnyAsync())
                {
                    var brands = new List<Brand>
                    {
                        new Brand { Name = "Sony" },
                        new Brand { Name = "Apple" },
                        new Brand { Name = "HP" }
                    };
                    await context.Brands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                    Log.Information("Marcas creadas con éxito.");
                }

                // Creación de usuarios
                if (!await context.Users.AnyAsync())
                {
                    // Use role names directly to avoid nullable role lookup issues
                    // Roles are created above, so we can reference them by name when assigning to users.
                    const string customerRoleName = "Customer";
                    const string adminRoleName = "Admin";

                    // Creación de usuario administrador
                    User adminUser = new User
                    {
                        FirstName = configuration["User:AdminUser:FirstName"] ?? string.Empty,
                        LastName = configuration["User:AdminUser:LastName"] ?? string.Empty,
                        Email = configuration["User:AdminUser:Email"] ?? string.Empty,
                        EmailConfirmed = true,
                        Gender = Gender.Masculino,
                        Rut = configuration["User:AdminUser:Rut"] ?? string.Empty,
                        BirthDate = DateTime.TryParse(configuration["User:AdminUser:BirthDate"], out var parsedBirthDate) ? parsedBirthDate : DateTime.UtcNow.AddYears(-30),
                        PhoneNumber = configuration["User:AdminUser:PhoneNumber"] ?? string.Empty
                    };
                    adminUser.UserName = adminUser.Email;
                    var adminPassword = configuration["User:AdminUser:Password"];
                    if (string.IsNullOrWhiteSpace(adminPassword))
                    {
                        adminPassword = GenerateRandomPassword();
                        Log.Warning("Admin password not provided in configuration; a temporary password was generated.");
                    }
                    var adminResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (adminResult.Succeeded)
                    {
                        var addRoleResult = await userManager.AddToRoleAsync(adminUser, adminRoleName);
                        if (!addRoleResult.Succeeded)
                        {
                            Log.Error("Error asignando rol de administrador: {Errors}", string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));
                            throw new InvalidOperationException("No se pudo asignar el rol de administrador al usuario.");
                        }
                        Log.Information("Usuario administrador creado con éxito.");
                    }
                    else
                    {
                        Log.Error("No se pudo crear el usuario administrador: {Errors}", string.Join(", ", adminResult.Errors.Select(e => e.Description)));
                        // continue to seed other users even if admin creation fails
                    }

                    var randomPassword = configuration["User:RandomUserPassword"];
                    if (string.IsNullOrWhiteSpace(randomPassword))
                    {
                        randomPassword = GenerateRandomPassword();
                        Log.Warning("Random user password not provided in configuration; generated a temporary password for seeded users.");
                    }

                    var userFaker = new Faker<User>()
                        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                        .RuleFor(u => u.LastName, f => f.Name.LastName())
                        .RuleFor(u => u.Email, f => f.Internet.Email())
                        .RuleFor(u => u.EmailConfirmed, f => true)
                        .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                        .RuleFor(u => u.Rut, f => RandomRut())
                        .RuleFor(u => u.BirthDate, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                        .RuleFor(u => u.PhoneNumber, f => RandomPhoneNumber())
                        .RuleFor(u => u.UserName, (f, u) => u.Email);

                    var users = userFaker.Generate(99);
                    foreach (var user in users)
                    {
                        var result = await userManager.CreateAsync(user, randomPassword);
                        if (result.Succeeded)
                        {
                            var addRoleResult = await userManager.AddToRoleAsync(user, customerRoleName);
                            if (!addRoleResult.Succeeded)
                            {
                                Log.Error("Error asignando rol a {Email}: {Errors}", user.Email, string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));
                            }
                        }
                        else
                        {
                            Log.Error("Error creando usuario {Email}: {Errors}", user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                        }
                    }
                    Log.Information("Usuarios creados con éxito.");
                }

                // Creación de productos
                if (!await context.Products.AnyAsync())
                {
                    var categoryIds = await context.Categories.Select(c => c.Id).ToListAsync();
                    var brandIds = await context.Brands.Select(b => b.Id).ToListAsync();

                    if (categoryIds.Any() && brandIds.Any())
                    {
                        var productFaker = new Faker<Product>()
                            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                            .RuleFor(p => p.Price, f => f.Random.Int(1000, 100000))
                            .RuleFor(p => p.Stock, f => f.Random.Int(1, 100))
                            .RuleFor(p => p.CategoryId, f => f.PickRandom(categoryIds))
                            .RuleFor(p => p.BrandId, f => f.PickRandom(brandIds))
                            .RuleFor(p => p.Status, f => f.PickRandom<Status>());

                        var products = productFaker.Generate(50);
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                        Log.Information("Productos creados con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al inicializar la base de datos: {Message}", ex.Message);
            }
        }

        private static string RandomPhoneNumber()
        {
            var faker = new Faker();
            string firstPartNumber = faker.Random.Int(1000, 9999).ToString();
            string secondPartNumber = faker.Random.Int(1000, 9999).ToString();
            return $"+569 {firstPartNumber}{secondPartNumber}";
        }

        private static string GenerateRandomPassword()
        {
            // Create a reasonably strong password: length + mixed character classes
            const string upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            const string lower = "abcdefghijkmnopqrstuvwxyz";
            const string digits = "23456789";
            const string specials = "!@$?_-";
            var rng = new Random();
            string Pick(string s) => s[rng.Next(s.Length)].ToString();

            // Ensure at least one of each required type
            var passwordChars = new List<char>
            {
                Pick(upper)[0],
                Pick(lower)[0],
                Pick(digits)[0],
                Pick(specials)[0]
            };

            // Fill to desired length
            int desiredLength = 12;
            string all = upper + lower + digits + specials;
            for (int i = passwordChars.Count; i < desiredLength; i++)
            {
                passwordChars.Add(all[rng.Next(all.Length)]);
            }

            // Shuffle
            for (int i = passwordChars.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var tmp = passwordChars[i];
                passwordChars[i] = passwordChars[j];
                passwordChars[j] = tmp;
            }

            return new string(passwordChars.ToArray());
        }

        private static string RandomRut()
        {
            var faker = new Faker();
            int rutNumber = faker.Random.Int(1000000, 25000000);
            int rutCopy = rutNumber;
            int sum = 0;
            int multiplier = 2;
            while (rutCopy > 0)
            {
                sum += (rutCopy % 10) * multiplier;
                multiplier++;
                if (multiplier > 7) multiplier = 2;
                rutCopy /= 10;
            }
            int remainder = 11 - (sum % 11);
            string dv;
            if (remainder == 11) dv = "0";
            else if (remainder == 10) dv = "K";
            else dv = remainder.ToString();
            return $"{rutNumber}-{dv}";
        }
    }
}
