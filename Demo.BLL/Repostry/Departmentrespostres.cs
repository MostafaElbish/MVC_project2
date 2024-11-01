using Demo.BLL.Interfaces;
using Demo.DLL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Demo.DAL;

namespace Demo.BLL.Repostry
{
    public class Departmentrespostres : GenaricResponsry<Department>,IdepartmentResposty
    {
        public Departmentrespostres(AppDbContext context):base(context) 
        {

        }

    }
}
