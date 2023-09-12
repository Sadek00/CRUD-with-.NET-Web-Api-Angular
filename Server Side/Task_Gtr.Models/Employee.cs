using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task_Gtr.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeCode { get; set; }
        public int EmployeeSalary { get; set; }
        public int? SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public Employee Supervisor { get; set; }
    }
}