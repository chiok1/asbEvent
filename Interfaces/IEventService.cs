using asbEvent.Models;
using asbEvent.DTOs;

namespace asbEvent.Interfaces
{
    public interface IEventService
    {
        ServiceResult RegisterEvent(EventRegistration eventReg);
        ServiceResult Mark(MarkAttendanceDTO markAttendanceDTO);
        // string SendReminder(string eventId);
        // string GetEvents();
        // string GetDate();
        // ServiceResult Hello();
    }
}