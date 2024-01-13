using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Opus_Fahim.Models
{
    public enum Gender { Male = 1, Female }
    public class Employee
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
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    }

    public class Department
    {
        public int DepartmentId { get; set; }
        [Required, StringLength(50)]

        public string DepartmentName { get; set; } = default!;


        public int DepartmentMember { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; } = default!;
    }
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; } = default!;

        public DbSet<Department> Departments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Fahim", Gender = Gender.Male, Salary = 30000.00M, DoB = DateTime.Now.AddYears(-23), IsActive = true });

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT", DepartmentMember = 40, EmployeeId = 1 });
        }
    }
}