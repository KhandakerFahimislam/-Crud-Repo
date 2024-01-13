using Opus_Fahim.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Opus_Fahim.ViewModels
{
    public class EmployeeInputModel
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = default!;
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Salary { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime DoB { get; set; }
        public bool IsActive { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
    }

}
