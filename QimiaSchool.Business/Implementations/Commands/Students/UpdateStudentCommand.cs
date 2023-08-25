using MediatR;
using QimiaSchool.Business.Implementations.Commands.Students.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Students;

public class UpdateStudentCommand : IRequest<Unit>
{
    public int Id { get; }
    public UpdateStudentDto Student { get; set; }


    public UpdateStudentCommand(
        int id,
        UpdateStudentDto student)
    {
        Id = id;
        Student = student;
    }
}


// IRequest<Unit> is like a void, we can't use void since it is not a valid return type in C#. unit also gives something like a readonly attribute to data ve requesting.
// then we get ıd and Student by updateStudentDto
// after we get it we will update it using UpdateStudentCommand