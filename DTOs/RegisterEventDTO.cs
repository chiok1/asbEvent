namespace asbEvent.DTOs;

public class RegisterEventDTO {
    public required string Email { get; set; }
    public string? Fname { get; set; }
    public required string Lname { get; set; }
    public required long EventId { get; set; }
    public string? Company { get; set; }
}