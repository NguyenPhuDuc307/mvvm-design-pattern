# Part 5: Use AutoMapper in MVVM Pattern ASP.NET Core

>ASP.Net Core has a wide array of libraries that provide great assistance in development strategy. Using MVVM pattern and AutoMapper you can reduce your code lines and produce more reusable and efficient code. This guide is compiled based on [Get started with ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-8.0&tabs=visual-studio-code) by `Microsoft`.

In this section:

- Implement MVVM design pattern (Model-View-ViewModel), combined use AutoMapper to automatically map models.

Before coming to this guide, please refer to [Part 4: Seed the database an ASP.NET Core MVC application](https://github.com/NguyenPhuDuc307/seed-the-database).

## What is MVVM pattern?

Design patterns are exceptionally useful, no matter which platform or language you develop for. MVVM means Model, View, and ViewModel. For a test-driven development process, you can use MVVM patterns so that you can achieve maximum code coverage.

- **Model**: Model holds the data and its related logic.
- **View**: It is used for the UI component that handles the user interaction.
- **View Model**: It is used to link Model and View. ViewModel is presenting the function, commands and methods to support the state of view.

## Add a Request and ViewModel

Add a file named `CourseViewModel.cs` to the `ViewModels` folder, create it in your project's source folder.

Update the `ViewModels/CourseViewModel.cs` file with the following code:

```c#
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
```

Update the `Data/Entities/Course.cs` file with the following code:

```c#
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Data.Entities;

public class Course
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Topic { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Author { get; set; }
}
```

## Install AutoMapper

For install AutoMapper run the following command:

```bash
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

After installing the required package, the next step is to configure the services. Let’s do it in the `Program.cs` class:

```c#
//automapper config
builder.Services.AddAutoMapper(typeof(Program));
```

Add a file named `AutoMapperProfile.cs` to the `ViewModels/AutoMapper` folder, create it in your project's source folder.

Update the `ViewModels/AutoMapper/AutoMapperProfile.cs` file with the following code:

```c#
using AutoMapper;
using CourseManagement.Data.Entities;

namespace CourseManagement.ViewModels.AutoMapper
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();
            CreateMap<CourseRequest, Course>();
        }
    }
}
```

Update methods `Index`, `Create`, `Edit`, `Details`, Delete in `Controllers/CoursesController.cs` file with the following code:

```c#
public class CoursesController : Controller
    {
        private readonly CourseDbContext _context;
        private readonly IMapper _mapper;

        public CoursesController(CourseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var products = await _context.Courses.ToListAsync();

            return View(_mapper.Map<IEnumerable<CourseViewModel>>(products));
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CourseViewModel>(course));
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRequest request)
        {
            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(request);
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<CourseViewModel>(course));
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<Course>(course));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CourseViewModel>(course));
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
```

>Here, change the input and output of the methods

Similar with views, change the ```@model``` accordingly.

**Final, run the application to test functions:**

Run the following command:

```bash
dotnet watch run
```

Next let's [Part 6: Use dependency injection in .NET](https://github.com/NguyenPhuDuc307/dependency-injection).
