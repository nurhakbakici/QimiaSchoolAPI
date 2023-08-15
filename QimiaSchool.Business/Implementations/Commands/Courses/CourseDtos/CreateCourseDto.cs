using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Courses.CourseDtos;

public class CreateCourseDto
{
    public string? Title { get; set; } // ? after string means that string value can be null or hold a string.
    public int Credits { get; set; }
}
