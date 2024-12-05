using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace asbEvent.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseTestController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public DatabaseTestController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("test-connection")]
    public IActionResult TestConnection()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                return Ok("Connection successful!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Connection failed: {ex.Message}");
            }
        }
    }
}
