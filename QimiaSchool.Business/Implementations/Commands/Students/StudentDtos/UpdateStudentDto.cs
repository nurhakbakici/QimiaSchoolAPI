using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Students.StudentDtos;

public class UpdateStudentDto
{
    public string? FirstMidName { get; set; }
    public string? LastName { get; set; }
    public DateTime? EnrollmentDate { get; set; }
}