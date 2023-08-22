using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.Business.Implementations.Events.Courses;
using QimiaSchool.Business.Implementations.Events.Students;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Exceptions;
using QimiaSchool.DataAccess.MessageBroker.Asbtractions;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
{
    private readonly ICourseManager _courseManager;
    private readonly IEventBus _eventBus;

    public UpdateCourseCommandHandler(ICourseManager courseManager, IEventBus eventBus)
    {
        _courseManager = courseManager;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {

        var course = await _courseManager.GetCourseByIdAsync(request.Id, cancellationToken);

        course.Title = request.Course.Title ?? course.Title;
        course.Credits = request.Course?.Credits ?? course.Credits;

        await _courseManager.UpdateCourseById(course, cancellationToken);

        await _eventBus.PublishAsync(new CourseUpdatedEvent
        {
            Id = course.ID,
            Title = course.Title,
            Credits = course.Credits,
        });

        return Unit.Value;
    }
}
