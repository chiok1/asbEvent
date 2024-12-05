namespace asbEvent.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public required string EmployeeName { get; set; }
        public required string EmployeeDept { get; set; }
        public required string EmployeeToken { get; set; }
    }
}