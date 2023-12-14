using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels;

public class CourseRequest
{
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }
    [StringLength(60)]
    [Required]
    public string? Topic { get; set; }
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    [StringLength(60)]
    [Required]
    public string? Author { get; set; }
}

public class CourseViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Topic { get; set; }
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Author { get; set; }
}