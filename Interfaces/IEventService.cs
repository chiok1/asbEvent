using asbEvent.Models;
using asbEvent.DTOs;

namespace asbEvent.Interfaces
{
    public interface IEventService
    {
        ServiceResult RegisterEvent(RegisterEventDTO registerEventDTO);
        ServiceResult Mark(byte[] qrCodeImage);
        // string SendReminder(string eventId);
        // string GetEvents();
        // string GetDate();
        // ServiceResult Hello();
    }
}