using MediatR;
using QimiaSchool.Business.Implementations.Commands.Courses.CourseDtos;
using QimiaSchool.Business.Implementations.Commands.Students.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Courses;

public class UpdateCourseCommand : IRequest<Unit>
{
    public int Id { get; }
    public UpdateCourseDto Course { get; set; }

    public UpdateCourseCommand(
        int id, UpdateCourseDto course)
    {
        Id = id;
        Course = course;
    }
}