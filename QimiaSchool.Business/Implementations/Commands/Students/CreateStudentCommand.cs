using MediatR;
using QimiaSchool.Business.Implementations.Commands.Students.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Students;

public class CreateStudentCommand : IRequest<int>
{
    public CreateStudentDto StudentDto { get; set; }

    public CreateStudentCommand(CreateStudentDto student)
    {
        StudentDto = student;
    }
}