using MediatR;
using QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Courses;

public class GetCoursesQuery : IRequest<List<CourseDto>>
{

}