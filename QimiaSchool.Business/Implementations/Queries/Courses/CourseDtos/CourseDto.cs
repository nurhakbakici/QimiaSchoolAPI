using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;

public class CourseDto
{
    public int ID { get; set; }
    public string? Title { get; set; }
    public int Creadits { get; set; }
}