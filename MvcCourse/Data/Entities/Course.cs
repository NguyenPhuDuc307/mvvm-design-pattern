using System.ComponentModel.DataAnnotations;

namespace MvcCourse.Data.Entities;

public class Course
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Topic { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Author { get; set; }
}