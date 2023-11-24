
using System.ComponentModel.DataAnnotations;
 
namespace O2GEN.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string PhotoData { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}