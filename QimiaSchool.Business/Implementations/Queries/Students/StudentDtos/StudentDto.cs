using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;

public class StudentDto
{
    public int ID { get; set; }
    public string? LastName { get; set; }
    public string? FirstMidName { get; set;}
    public DateTime EnrollmentDate { get; set; }
    public IEnumerable<EnrollmentDto>? Enrollments { get; set; }
}
