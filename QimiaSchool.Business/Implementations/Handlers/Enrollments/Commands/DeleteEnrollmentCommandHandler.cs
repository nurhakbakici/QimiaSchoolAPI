using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Commands.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;


public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, Unit>
{
    private readonly IEnrollmentManager _enrollmentManager;

    public DeleteEnrollmentCommandHandler(IEnrollmentManager enrollmentManager)
    {
        _enrollmentManager = enrollmentManager;
    }

    public async Task<Unit> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {

        await _enrollmentManager.DeleteEnrollmentByIdAsync(request.Id, cancellationToken);

        return Unit.Value;
    }
}

