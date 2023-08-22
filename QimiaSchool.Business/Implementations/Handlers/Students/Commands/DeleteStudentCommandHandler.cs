using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.Business.Implementations.Events.Students;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Asbtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
{
    private readonly IStudentManager _studentManager;
    private readonly IEventBus _eventBus;

    public DeleteStudentCommandHandler(IStudentManager studentManager, IEventBus eventBus)
    {
        _studentManager = studentManager;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        await _studentManager.DeleteStudentByIdAsync(request.Id, cancellationToken);

        await _eventBus.PublishAsync(new StudentDeletedEvent{
            Id = request.Id
        });

        return Unit.Value;
    }
}
