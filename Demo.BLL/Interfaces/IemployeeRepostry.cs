using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models;
using Demo.DLL.models;

namespace Demo.BLL.Interfaces
{
    public interface IemployeeRepostry : Igenaric<Employees>
    {
       public Task< IEnumerable<Employees>> Getbyname(string name);
    }
}
