using asbEvent.Models;
using asbEvent.Interfaces;
using asbEvent.Repositories;
using asbEvent.DTOs;
using asbEvent.Helpers;

using System.Transactions;

namespace asbEvent.Services
{
    public class EventService(EventRepository eventRepository, AttendeeRepository attendeeRepository, EventRegistrationRepository eventRegistrationRepository, ILogger<EventService> logger, Emailer emailer) : IEventService {
        private readonly EventRepository _eventRepository = eventRepository;
        private readonly EventRegistrationRepository _eventRegistrationRepository = eventRegistrationRepository;
        private readonly AttendeeRepository _attendeeRepository = attendeeRepository;
        private readonly ILogger<EventService> _logger = logger;
        private readonly Emailer _emailer = emailer;
        
        public ServiceResult RegisterEvent(RegisterEventDTO registerEventDTO)
        {
            try {
                using var transaction = new TransactionScope();
                // _logger.LogInformation("Registering event for email: {Email}", registerEventDTO.Email);
                // _logger.LogInformation("Checking if Attendee already exists with email: {Email}", registerEventDTO.Email);

                Attendee? existingAttendee = _attendeeRepository
                    .Find(a => a.Email == registerEventDTO.Email)
                    .FirstOrDefault();

                long attendeeId;

                if (existingAttendee == null) {
                    // Create a new Attendee
                    Attendee attendee = new()
                    {
                        Email = registerEventDTO.Email,
                        Lname = registerEventDTO.Lname,
                        Company = registerEventDTO.Company ?? null,
                        Fname = registerEventDTO.Fname ?? null,
                    };
                    // Save the attendee to the database
                    // _logger.LogInformation("Adding new attendee to the database: {Email}", registerEventDTO.Email);
                    _attendeeRepository.Add(attendee);
                    _logger.LogInformation("Attendee added successfully: {Email}", registerEventDTO.Email);
                    attendeeId = attendee.Id;
                } else {
                    attendeeId = existingAttendee.Id;
                }

                // Find Event
                // _logger.LogInformation("Checking for Event with ID: {EventId}", registerEventDTO.EventId);
                Event? eventToRegister = _eventRepository.Find(e => e.Id == registerEventDTO.EventId).FirstOrDefault();

                if (eventToRegister == null)
                {
                    return ServiceResult.ErrorResult("0", "Event not found.");
                }

                // Check if user has already registered for the event
                EventRegistration? existingRegistration = _eventRegistrationRepository
                    .Find(er => er.AttendeeId == attendeeId && er.EventId == registerEventDTO.EventId)
                    .FirstOrDefault();

                if (existingRegistration != null)
                {
                    _logger.LogInformation("User has already registered for this Event ID: {EventId}", existingRegistration.Id);
                    return ServiceResult.ErrorResult("0", "User has already registered for this event.");
                }

                // _logger.LogInformation("Generating QR code for Event ID: {EventId}", registerEventDTO.EventId);

                // Generate QR code
                string qrCodeString = $"{registerEventDTO.Email}-{registerEventDTO.EventId}";
                byte[] qrCodeImage = Helpers.QRCoder.GenerateQRCode(qrCodeString);

                _logger.LogInformation("QR code generated successfully for Event ID: {EventId}", registerEventDTO.EventId);

                // _logger.LogInformation("Registering Event ID: {EventId} for Attendee ID: {AttendeeId}", registerEventDTO.EventId, attendeeId);

                // Register Event
                EventRegistration eventRegistration = new()
                {
                    AttendeeId = attendeeId,
                    EventId = registerEventDTO.EventId,
                    Qrcode = "NIL",
                    EventDate = eventToRegister.StartDateTime,
                    RegistrationTime = DateTime.Now,
                };
                // _logger.LogInformation("Adding new Event Registration to the database for Event ID: {EventId}", registerEventDTO.EventId);

                _eventRegistrationRepository.Add(eventRegistration);

                _logger.LogInformation("Event Registration added successfully for Event ID: {EventId}", eventRegistration.Id);

                SendEmailDTO sendEmailDTO = new()
                {
                    Email = registerEventDTO.Email,
                    EventName = eventToRegister.EventName,
                    EventDesc = $"You have successfully registered for the event: {eventToRegister.EventDesc}.",
                    StartDateTime = eventToRegister.StartDateTime,
                    QrCode = qrCodeImage,
                };

                emailer.SendEmail(sendEmailDTO);

                _logger.LogInformation("Email sent successfully for Event ID: {EventId}", registerEventDTO.EventId);

                transaction.Complete();

                return ServiceResult.SuccessResult("0", "Event registered successfully.");

            } catch (Exception e) {
                _logger.LogError(e, "Error registering event.");
                return ServiceResult.ErrorResult("103", $"Error registering event: {e.Message}");
            }
        }

        public ServiceResult Mark(byte[] qrCodeImage) {
            try {
                 // Check if the QR code image is valid
                if (qrCodeImage == null || qrCodeImage.Length == 0) {
                    _logger.LogError("Invalid QR code image.");
                    return ServiceResult.ErrorResult("0", "Invalid QR code image.");
                }

                // Decode the QR code to get email and event ID
                string decodedString = Helpers.QRCoder.DecodeQRCode(qrCodeImage);
                Console.WriteLine("Decoded String: " + decodedString);
                string[] parts = decodedString.Split('-');
                if (parts.Length != 2) {
                    _logger.LogError("Invalid QR code format.");
                    return ServiceResult.ErrorResult("0", "Invalid QR code format.");
                }

                string email = parts[0];
                long eventId = long.Parse(parts[1]);

                // Find the event registration record
                EventRegistration? eventRegistration = _eventRegistrationRepository
                    .Find(er => er.EventId == eventId && er.Attendee.Email == email)
                    .FirstOrDefault();

                if (eventRegistration == null) {
                    _logger.LogError("Event registration not found for email: {Email} and event ID: {EventId}", email, eventId);
                    return ServiceResult.ErrorResult("0", "You are not registered for this event.");
                }

                // Check if the user has already marked attendance for this event
                if (eventRegistration.AttendedTime != null) {
                    _logger.LogError("User has already marked attendance for this event.");
                    return ServiceResult.SuccessResult("0", "You have already marked attendance for this event.");
                }

                // Mark attendance
                eventRegistration.AttendedTime = DateTime.Now;
                _eventRegistrationRepository.Update(eventRegistration);

                _logger.LogInformation("Attendance marked successfully for Event ID: {EventId}", eventRegistration.EventId);
                return ServiceResult.SuccessResult("0", "Attendance marked successfully.");

            } catch (Exception e) {
                _logger.LogError("Error marking attendance: {Error}", e.Message);
                return ServiceResult.ErrorResult("103", $"Error marking attendance: {e.Message}");
            }
        }   
    }
}
