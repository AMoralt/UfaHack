
using System.ComponentModel.DataAnnotations;
 
namespace O2GEN.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int ModuleID { get; set; }
        public string Question { get; set; }
        public string[] Options { get; set; }
        public int[] CorrectOption { get; set; }
        public string Explanation { get; set; }
    }
}