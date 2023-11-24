
using System.ComponentModel.DataAnnotations;
 
namespace O2GEN.Models
{
    public class UserData
    {
        public int Id { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public DateTime RegistrationDate { get; set; }
        /// <summary>
        /// SA - SuperAdmin | A - Admin | U - User
        /// </summary>
        public string Role { get; set; }
    }
}