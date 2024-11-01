using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string firstname { get; set; }
        public string Lastname { get; set; }
        public bool Isagree { get; set; }
    }
}
