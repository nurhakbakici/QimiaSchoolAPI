using MediatR;
using QimiaSchool.Business.Implementations.Commands.Enrollments.EnrollmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Enrollments;

public class UpdateEnrollmentCommand : IRequest<Unit>
{
    public int Id { get; }
    public UpdateEnrollmentDto Enrollment { get; set; }

    public UpdateEnrollmentCommand (
        int id, UpdateEnrollmentDto enrollment)
    {
        Id = id;
        Enrollment = enrollment;
    }
}



