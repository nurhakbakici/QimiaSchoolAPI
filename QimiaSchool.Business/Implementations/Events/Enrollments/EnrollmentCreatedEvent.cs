using MassTransit.Futures.Contracts;
using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Enrollments;

public record EnrollmentCreatedEvent
{
    public int EnrollmentId { get; init; }
    public int StudentId { get; init; }
    public int CourseId { get; init; }
    public Grade Grade { get; init; }
}