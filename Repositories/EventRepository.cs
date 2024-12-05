using asbEvent.Data;
using asbEvent.Models;
using asbEvent.DTOs;
using asbEvent.Interfaces;
using System.Linq.Expressions;

namespace asbEvent.Repositories
{
    public class EventRepository : IRepository<EventRegistration>
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public void Add(EventRegistration entity)
        {
            _context.EventRegistrations.Add(entity);
        }


        // public ServiceResult RegisterEvent(EventRegistration eventReg)
        // {
        //     try
        //     {
        //         _context.EventRegistrations.Add(eventReg);
        //         _context.SaveChanges();
        //         return ServiceResult.SuccessResult("0", "Event registered successfully.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return ServiceResult.ErrorResult("1", $"Error registering event: {ex.Message}");
        //     }
        // }
        // public bool IsAlreadyRegistered(int eventId, string empId)
        // {
        //     return _context.EventRegistrations
        //                    .Any(reg => reg.EventId == eventId && reg.EmpID == empId);
        // }

        // public bool IsAlreadyAttended(int eventId, string empId)
        // {
        //     return _context.AttendeeDetails
        //                    .Any(attn => attn.EventId == eventId && attn.EmpID == empId);
        // }


    }
}

