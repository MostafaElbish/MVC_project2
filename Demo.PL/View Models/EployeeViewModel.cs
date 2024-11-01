using Demo.DLL.models;
using System.ComponentModel.DataAnnotations;
using System;
using Demo.DAL.Models;

namespace Demo.PL.View_Models
{
    public class EployeeViewModel:Base
    {
        [Required]
        public string Name { get; set; }
        [Range(22, 45)]
        public int? Age { get; set; }
        public decimal Salary { get; set; }

        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HirangDate { get; set; }
        public DateTime DateofCreation { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
