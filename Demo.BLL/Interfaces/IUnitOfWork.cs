using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IdepartmentResposty DepartmentReposrty { get; }
        public IemployeeRepostry EmoloyeeRepostry { get;  }
      Task<int> Complete();
    }
}
