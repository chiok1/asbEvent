//    using Microsoft.AspNetCore.Mvc;
//    using asbEvent.Data;

//    namespace asbEvent.Controllers
//    {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class DatabaseTestController(ApplicationDbContext context) : ControllerBase
//       {
//          private readonly ApplicationDbContext _context = context;

//         [HttpGet("test-database")]
//          public IActionResult DatabaseTest()
//          {
//             try {
//                 var events = _context.EventRegistrations.ToList();
//                 return Ok(events);
//             } catch (Exception e) {
//                 return BadRequest(e.Message);
//             }
            
//          }
//       }
//    }