using MediatR;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Students
{
    public class GetStudentQuery:IRequest<StudentDto>
    {
        public int Id { get; }

        public GetStudentQuery(int id)
        {
            Id = id;
        }
    }
}
