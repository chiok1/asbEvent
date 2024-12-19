namespace asbEvent.DTOs;

public class SendEmailDTO
{
    public required string Email { get; set; }
    public required string EventName { get; set; }
    public required string EventDesc { get; set; }
    public required DateTime StartDateTime { get; set; }
    public required byte[] QrCode { get; set; }
}