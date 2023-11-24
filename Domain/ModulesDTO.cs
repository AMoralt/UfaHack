
using System.ComponentModel.DataAnnotations;
 
namespace O2GEN.Models
{
    public class ModulesDTO
    {
        public int Id { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ModuleOrder { get; set; }
    }
}