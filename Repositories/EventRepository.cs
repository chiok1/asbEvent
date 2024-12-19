using asbEvent.Data;
using asbEvent.Models;
using asbEvent.DTOs;
using asbEvent.Interfaces;
using System.Linq.Expressions;

namespace asbEvent.Repositories;

public class EventRepository(ApplicationDbContext context) : IRepository<Event> {
    private readonly ApplicationDbContext _context = context;

    public IEnumerable<Event> GetAll() {
        return _context.Events;
    }
    public Event GetById(long id) {
        return _context.Events.FirstOrDefault(e => e.Id == id);
    }
    public Event Add(Event entity) {
        _context.Events.Add(entity);
        _context.SaveChanges(); // Ensure changes are saved to generate the Id
        return entity; // Return the entity with the generated Id
    }
    public void AddRange(IEnumerable<Event> entities) {
        _context.Events.AddRange(entities);
    }
    public void Remove(Event entity) {
        _context.Events.Remove(entity);
    }
    public void RemoveRange(IEnumerable<Event> entities) {
        _context.Events.RemoveRange(entities);
        ;
    }
    public IEnumerable<Event> Find(Expression<Func<Event, bool>>? predicate) {
        if (predicate == null) {
            return _context.Events;
        } else {
            return _context.Events.Where(predicate);
        }
    }
}