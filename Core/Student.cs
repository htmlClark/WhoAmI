using System.ComponentModel.DataAnnotations;

namespace Core;

public class Student
{
    [Required]
    public string Name { get; set; }
    public int Id { get; set; }
}
