using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Enrollments;

public record EnrollmentDeletedEvent
{
    public int Id { get; init; }
    public int CourseId { get; init; }
    public int StudentId { get; init; }
}
