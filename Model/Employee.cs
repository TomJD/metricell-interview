using System.ComponentModel.DataAnnotations;

namespace InterviewTest.Model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
