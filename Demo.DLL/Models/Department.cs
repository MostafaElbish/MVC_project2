using Demo.DAL.Data.Migrations;
using Demo.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DLL.models
{
    public class Department :Base
    {
        [Required(ErrorMessage = "code is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Code is Required")]
        public string Name { get; set; }
       
        [DisplayName("Date Of Creation")]
        public DateTime Date { get; set; }
        public List<Employees> Employees { get; set; }
    }
}
