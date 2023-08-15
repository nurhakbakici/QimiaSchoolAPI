using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
{
    private readonly IStudentManager _studentManager;

    public DeleteStudentCommandHandler(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        await _studentManager.DeleteStudentByIdAsync(request.Id, cancellationToken);

        return Unit.Value;
    }
}
