﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Courses;

public record CourseUpdatedEvent
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int Credits { get; init; }
}
