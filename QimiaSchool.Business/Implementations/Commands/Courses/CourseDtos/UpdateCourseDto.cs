using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Courses.CourseDtos;

public class UpdateCourseDto : IRequest<Unit>
{
    public string? Title { get; set; }
    public int Credits { get; set; }
}