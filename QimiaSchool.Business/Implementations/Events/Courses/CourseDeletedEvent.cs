using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Courses;

public record CourseDeletedEvent
{
    public int Id { get; init; }
}
