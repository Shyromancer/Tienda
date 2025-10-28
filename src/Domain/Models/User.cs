using Microsoft.AspNetCore.Identity;
using System;

namespace Tienda.src.Domain.Models
{
    /// <summary>
    /// Modelo que representa un usuario del sistema.
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
//        public string FirstName { get; set; }

        /// <summary>
        /// Apellido del usuario.
        /// </summary>
//        public string LastName { get; set; }

        /// <summary>
        /// Género del usuario (Masculino, Femenino, Otro).
        /// </summary>
//        public string Gender { get; set; }

        /// <summary>
        /// Fecha de nacimiento del usuario.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Número de RUT del usuario (formato chileno).
        /// </summary>
//        public string Rut { get; set; }

        /// <summary>
        /// Número de teléfono del usuario.
        /// </summary>
//        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Rol del usuario.
        /// </summary>
//        public string Role { get; set; }
        
        /// <summary>
        /// Fecha de creación del usuario.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indica si el usuario está activo.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
