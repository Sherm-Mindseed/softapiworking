
using softapiworking.mydb;

namespace softapiworking.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Hired { get; set; }
        public string? skills { get; set; } 
    }
}
