using asbEvent.Data;
using asbEvent.Models;
using asbEvent.DTOs;
using asbEvent.Interfaces;
using System.Linq.Expressions;

namespace asbEvent.Repositories;

public class AttendeeRepository : IRepository<Attendee> {
    private readonly ApplicationDbContext _context;

    public AttendeeRepository(ApplicationDbContext context) {
        _context = context;
    }

    public IEnumerable<Attendee> GetAll() {
        return _context.Attendees;
    }

    public Attendee GetById(long id) {
        return _context.Attendees.FirstOrDefault(e => e.Id == id);
    }

    public Attendee Add(Attendee entity) {
        _context.Attendees.Add(entity);
        _context.SaveChanges(); // Ensure changes are saved to generate the Id
        return entity; // Return the entity with the generated Id
    }

    public void AddRange(IEnumerable<Attendee> entities) {
        _context.Attendees.AddRange(entities);
    }

    public void Remove(Attendee entity) {
        _context.Attendees.Remove(entity);
    }

    public void RemoveRange(IEnumerable<Attendee> entities) {
        _context.Attendees.RemoveRange(entities);
    }

    public IEnumerable<Attendee> Find(Expression<Func<Attendee, bool>>? predicate) {
        if (predicate == null) {
            return _context.Attendees;
        }
        return _context.Attendees.Where(predicate);
    }
}