namespace asbEvent.Models
{
    public partial class EventRegistration
    {
        public int EventId { get; set; }
        public required string EmployeeName { get; set; }
        public required string EmpID { get; set; }
        public required string Company { get; set; }
        public required string Location { get; set; }
        public required string EmployeeEmail { get; set; }
        public required string Country { get; set; }
        public required DateTime RegistrationDate { get; set; }
        public DateTime? AttendanceDate { get; set; }
    }
}