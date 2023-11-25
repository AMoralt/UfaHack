
using System.ComponentModel.DataAnnotations;
 
namespace O2GEN.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Role { get; set; }
    }
}