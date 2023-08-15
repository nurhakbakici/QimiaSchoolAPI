using MediatR;
using QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Courses
{
    public class GetCourseQuery : IRequest<CourseDto>
    {
        public int Id { get; }

        public GetCourseQuery(int id)
        {
            Id = id;
        }
    }
}

// we only used get property because in this code all we want is to get the item we will not set anything.