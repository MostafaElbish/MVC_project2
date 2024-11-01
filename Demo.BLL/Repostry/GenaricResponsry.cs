using Demo.DAL.Models;
using Demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Repostry
{
    public class GenaricResponsry<T> :Igenaric<T> where T : Base
    {
        private protected AppDbContext _context;

        public GenaricResponsry(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {

           await _context.AddAsync(entity);
         

        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            


        }

        public async Task< T> Get(int id)
        {
            //var department = _context.Employees.FirstOrDefault(x => x.Id == id);
            var department =await _context.Set<T>().FindAsync(id);
            return department;
        }

        public async Task< IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employees))
            {
                return (IEnumerable<T>)await _context.Employees.Include(E=>E.Department).ToListAsync();
            }
            var department = await _context.Set<T>().ToListAsync();
            return department;

        }

        public void Update(T entity)

        {
            _context.Update(entity);

          

        }

        
    }
}