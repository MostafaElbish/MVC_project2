using Demo.DLL.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employees :Base
    {
        [Required(ErrorMessage = "Name is Required")]
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
        [DisplayName("Hiring Date")]
        public DateTime HirangDate { get; set; }
        [DisplayName("Date Of Creation")]
        public DateTime DateofCreation { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
