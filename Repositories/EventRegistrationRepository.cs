using asbEvent.Data;
using asbEvent.Models;
using asbEvent.DTOs;
using asbEvent.Interfaces;
using System.Linq.Expressions;

namespace asbEvent.Repositories
{
    public class EventRegistrationRepository(ApplicationDbContext context) : IRepository<EventRegistration>
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<EventRegistration> GetAll()
        {
            return _context.EventRegistrations;
        }

        public IEnumerable<EventRegistration> Find(Expression<Func<EventRegistration, bool>>? predicate)
        {
            if (predicate == null)
            {
                return _context.EventRegistrations;
            }
            return _context.EventRegistrations.Where(predicate);
        }

        public EventRegistration GetById(long id)
        {
            return _context.EventRegistrations.FirstOrDefault(c => c.EventId == id);
        }

        public EventRegistration Add(EventRegistration entity)
        {
            _context.EventRegistrations.Add(entity);
            _context.SaveChanges(); // Ensure changes are saved to generate the Id
            return entity; // Return the entity with the generated Id
        }

        public EventRegistration Update(EventRegistration entity)
        {
            _context.EventRegistrations.Update(entity);
            _context.SaveChanges();
            return entity;
        }

    }
}

