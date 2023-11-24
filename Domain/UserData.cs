
using System.ComponentModel.DataAnnotations;
 
namespace O2GEN.Models
{
    public class UserData : Entity
    {
        /// <summary>
        /// Engineers.Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string GivenName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// SA - SuperAdmin | A - Admin | U - User
        /// </summary>
        public string RoleCode { get; set; }
    }
}