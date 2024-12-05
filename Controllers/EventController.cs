using asbEvent.Services;
using asbEvent.Interfaces;
using asbEvent.DTOs;
using asbEvent.Models;

using Microsoft.AspNetCore.Mvc;

namespace asbEvent.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpPost("register-event")]
        public IActionResult RegisterEvent(EventRegistration eventReg)
        {
            ServiceResult result = _eventService.RegisterEvent(eventReg);
            return Ok(result);
        }

        [HttpPost("mark")]
        public IActionResult Mark(MarkAttendanceDTO markAttendanceDTO)
        {
            ServiceResult result = _eventService.Mark(markAttendanceDTO);
            return Ok(result);
        }

        // [HttpPost("send-reminder")]
        // public IActionResult SendReminder(string eventId)
        // {
        //     string result = _eventService.SendReminder(eventId);
        //     return Ok(result);
        // }

        // [HttpGet("get-events")]
        // public IActionResult GetEvents()
        // {
        //     string result = _eventService.GetEvents();
        //     return Ok(result);
        // }

        // [HttpGet("get-date")]
        // public IActionResult GetDate()
        // {
        //     string result = _eventService.GetDate();
        //     return Ok(result);
        // }

        // [HttpGet("hello")]
        // public IActionResult Hello()
        // {
        //     ServiceResult result = _eventService.Hello();
        //     return Ok(result);
        // }
    }

}