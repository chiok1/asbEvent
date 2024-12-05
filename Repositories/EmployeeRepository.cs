using asbEvent.Data;
using asbEvent.Models;
using asbEvent.Interfaces;
using System.Linq.Expressions;

namespace asbEvent.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }

        public IEnumerable<Employee> Find(Expression<Func<Employee, bool>>? predicate)
        {
            if (predicate == null)
            {
                return _context.Employees;
            }
            return _context.Employees.Where(predicate);
        }

        public Employee GetById(long id)
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeID == id);
        }

        public void Add(Employee entity)
        {
            _context.Employees.Add(entity);
        }
    }
}
