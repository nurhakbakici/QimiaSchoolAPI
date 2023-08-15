using MediatR;
using QimiaSchool.Business.Implementations.Commands.Enrollments.EnrollmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Enrollments;

public class CreateEnrollmentCommand : IRequest<int>
{
    public CreateEnrollmentDto Enrollment { get; set; }

    public CreateEnrollmentCommand(
        CreateEnrollmentDto enrollment)
    {
        Enrollment = enrollment;
    }
}
