using Microsoft.EntityFrameworkCore;
using CourseManagement.Data.Entities;

namespace CourseManagement.Data
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new CourseDbContext(serviceProvider.GetRequiredService<DbContextOptions<CourseDbContext>>()))
            {
                // Look for any products.
                if (context.Courses.Any())
                {
                    return;   // DB has been seeded
                }
                context.Courses.AddRange(
                    new Course
                    {
                        Title = "ASP.NET Core MVC",
                        Topic = ".NET Programming",
                        ReleaseDate = DateTime.Today,
                        Author = "vnLab"
                    },
                    new Course
                    {
                        Title = "ASP.NET Core API",
                        Topic = ".NET Programming",
                        ReleaseDate = DateTime.Today,
                        Author = "vnLab"
                    },
                    new Course
                    {
                        Title = "Java Spring Boot",
                        Topic = "Java Programming",
                        ReleaseDate = DateTime.Today,
                        Author = "vnLab"
                    },
                    new Course
                    {
                        Title = "Laravel - The PHP Framework",
                        Topic = "PHP Programming",
                        ReleaseDate = DateTime.Today,
                        Author = "vnLab"
                    },
                    new Course
                    {
                        Title = "Angular Tutorial For Beginner",
                        Topic = "Angular Programming",
                        ReleaseDate = DateTime.Today,
                        Author = "vnLab"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}