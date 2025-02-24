using System.ComponentModel.DataAnnotations;

namespace DemoProject.Entities;

public class Course
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Vul in sukkel")]
    [RegularExpression("^[a-zA-Z0-9: #.'-]*$", ErrorMessage = "Bladiebla letters graag")]
    public string Title { get; set; }

    [Required]
    [Range(0, 7)]
    public short NrOfDays { get; set; }

    public string FunPhoto { get; set; }
}
