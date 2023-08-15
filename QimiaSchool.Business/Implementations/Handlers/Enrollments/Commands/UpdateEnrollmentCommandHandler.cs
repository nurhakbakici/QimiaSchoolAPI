using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;

public class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, Unit>
{
    private readonly IEnrollmentManager _enrollmentManager;

    public UpdateEnrollmentCommandHandler(IEnrollmentManager enrollmentManager)
    {
        _enrollmentManager = enrollmentManager;
    }

    public async Task<Unit> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentManager.GetEnrollmentByIdAsync(request.Id, cancellationToken);
        enrollment.Grade = request.Enrollment.Grade;

        await _enrollmentManager.UpdateEnrollmentAsync(enrollment, cancellationToken);

        return Unit.Value;
    }
}