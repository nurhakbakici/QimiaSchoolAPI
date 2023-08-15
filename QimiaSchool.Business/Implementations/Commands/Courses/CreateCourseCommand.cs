using MediatR;
using QimiaSchool.Business.Implementations.Commands.Courses.CourseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Courses;

public class CreateCourseCommand : IRequest<int>
{ 
    public CreateCourseDto Course { get; set; }

    public CreateCourseCommand(
        CreateCourseDto course)
    {
        Course = course;
    }
}


