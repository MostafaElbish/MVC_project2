using Demo.BLL.Interfaces;
using Demo.BLL.Repostry;
using Demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public
        class UnitOfWork : IUnitOfWork 
    {
        private readonly AppDbContext _context;
        private Lazy<IdepartmentResposty> departmentResposty;
        private Lazy<IemployeeRepostry> employeeRepostry;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            departmentResposty = new Lazy<IdepartmentResposty>(new Departmentrespostres(_context));
            employeeRepostry = new Lazy<IemployeeRepostry>(new EmployeeRepostry(_context));
        }
        public  IdepartmentResposty DepartmentReposrty => departmentResposty.Value;
        public IemployeeRepostry EmoloyeeRepostry  => employeeRepostry.Value;

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
