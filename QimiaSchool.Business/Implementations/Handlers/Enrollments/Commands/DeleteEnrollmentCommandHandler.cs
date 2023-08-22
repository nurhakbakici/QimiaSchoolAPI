using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.Business.Implementations.Events.Enrollments;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Asbtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;


public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, Unit>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IEventBus _eventBus;

    public DeleteEnrollmentCommandHandler(IEnrollmentManager enrollmentManager, IEventBus eventBus)
    {
        _enrollmentManager = enrollmentManager;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentManager.GetEnrollmentByIdAsync(request.Id, cancellationToken);

        await _enrollmentManager.DeleteEnrollmentByIdAsync(request.Id, cancellationToken);

        await _eventBus.PublishAsync(new EnrollmentDeletedEvent
        {
            Id = request.Id,
            CourseId = enrollment.CourseID,
            StudentId = enrollment.StudentID 
        });

        return Unit.Value;
    }
}

