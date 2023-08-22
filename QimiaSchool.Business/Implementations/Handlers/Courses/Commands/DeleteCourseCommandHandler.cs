using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.Business.Implementations.Events.Courses;
using QimiaSchool.DataAccess.MessageBroker.Asbtractions;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly ICourseManager _courseManager;
    private readonly IEventBus _eventBus;

    public DeleteCourseCommandHandler(ICourseManager courseManager, IEventBus eventBus)
    {
        _courseManager = courseManager;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        await _courseManager.DeleteCourseById(request.Id, cancellationToken);

        await _eventBus.PublishAsync(new CourseDeletedEvent
        {
            Id = request.Id,
        });

        return Unit.Value;
    }
}
