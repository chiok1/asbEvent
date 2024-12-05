using asbEvent.Data;
using asbEvent.Models;
using asbEvent.Interfaces;
using asbEvent.Repositories;
using asbEvent.DTOs;

namespace asbEvent.Services
{
    public class EventService(EventRepository eventRepository) : IEventService {

        private readonly EventRepository _eventRepository = eventRepository;
        private readonly List<string> _excludedDomains =
        [
            "gmail.com",
            "yahoo.com",
            "hotmail.com",
            "outlook.com",
            "icloud.com",
            "live.com",
        ];
        
        public ServiceResult RegisterEvent(EventRegistration eventReg)
        {
            try {

                // Check if the email is a corporate email
                if (_excludedDomains.Contains(eventReg.EmployeeEmail.Split("@")[1].ToLower()))
                {
                    return ServiceResult.SuccessResult("0", "Please enter your corporate email.");
                }

                // Check if the user is already registered for the event
                EventRegistration? eventRegistration = _eventRepository
                    .Find(er => er.EventId == eventReg.EventId && er.EmpID == eventReg.EmpID)
                    .FirstOrDefault();
                
                if (eventRegistration != null) {
                    return ServiceResult.SuccessResult("0", "You are already registered for this event.");
                }

                // Register the event
                _eventRepository.Add(eventReg);

                return ServiceResult.SuccessResult("0", "Event registered successfully.");

            } catch (Exception e) {
                return ServiceResult.ErrorResult("103", $"Error registering event: {e.Message}");
            }
        }

        public ServiceResult Mark(MarkAttendanceDTO markAttendanceDTO) {
            try {
                // Check if event registration exists for this user and event
                EventRegistration? eventRegistration = _eventRepository
                    .Find(er => er.EventId == markAttendanceDTO.EventId && er.EmpID == markAttendanceDTO.EmpID)
                    .FirstOrDefault();

                if (eventRegistration == null) {
                    return ServiceResult.SuccessResult("0", "You are not registered for this event.");
                }

                // Check if the user has already marked attendance for this event
                if (eventRegistration.AttendanceDate != null) {
                    return ServiceResult.SuccessResult("0", "You have already marked attendance for this event.");
                }

                // Mark attendance
                eventRegistration.AttendanceDate = DateTime.Now;

                return ServiceResult.SuccessResult("0", "Attendance marked successfully.");

            } catch (Exception e) {
                return ServiceResult.ErrorResult("103", $"Error marking attendance: {e.Message}");
            }
        }   
    }
}
