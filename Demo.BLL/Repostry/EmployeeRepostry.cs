using Demo.BLL.Interfaces;
using Demo.DAL;
using Demo.DAL.Models;
using Demo.DLL.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostry
{
    public class EmployeeRepostry : GenaricResponsry<Employees>, IemployeeRepostry
    {
        public EmployeeRepostry(AppDbContext context):base(context)
        {

        }

        public async Task< IEnumerable<Employees>> Getbyname(string name)
        {
           return await _context.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower())).Include(E=>E.Department).ToListAsync();
        }
    }
}
