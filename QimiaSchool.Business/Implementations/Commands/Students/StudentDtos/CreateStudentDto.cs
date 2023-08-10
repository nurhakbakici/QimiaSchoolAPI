using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Students.StudentDtos;

public class CreateStudentDto
{
    public string? FirstMidName { get; set; }
    public string? LastName { get; set; }


    // name and surname are the only two parameters we need to create a new student.

    // we will not have a DeleteStudentDto because we dont need data transfer for delete operation.
}
