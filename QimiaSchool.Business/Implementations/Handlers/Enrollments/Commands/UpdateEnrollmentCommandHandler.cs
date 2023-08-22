using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Events.Enrollments;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;
using QimiaSchool.DataAccess.MessageBroker.Asbtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;

public class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, Unit>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IEventBus _eventBus;

    public UpdateEnrollmentCommandHandler(IEnrollmentManager enrollmentManager, IEventBus eventBus)
    {
        _enrollmentManager = enrollmentManager;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentManager.GetEnrollmentByIdAsync(request.Id, cancellationToken);
        enrollment.Grade = request.Enrollment.Grade;

        await _enrollmentManager.UpdateEnrollmentAsync(enrollment, cancellationToken);

        await _eventBus.PublishAsync(new EnrollmentUpdatedEvent
        {
            Id = request.Id,
            CourseId = enrollment.CourseID,
            StudentId = enrollment.StudentID,
            Grade = request.Enrollment.Grade,
        });

        return Unit.Value;
    }
}