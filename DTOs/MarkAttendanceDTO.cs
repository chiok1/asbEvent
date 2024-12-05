namespace asbEvent.DTOs
{
    public class MarkAttendanceDTO
    {
        public required string EmpID { get; set; }
        public required int EventId { get; set; }
        public required string Location { get; set; }
        public required string Country { get; set; }
        public required string EmployeeEmail { get; set; }
    }
}