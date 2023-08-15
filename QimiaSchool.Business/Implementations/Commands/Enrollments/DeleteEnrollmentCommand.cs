using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Enrollments;

public class DeleteEnrollmentCommand : IRequest<Unit>
{
    public int Id { get;  }

    public DeleteEnrollmentCommand(
        int id)
    {
        Id = id;
    }
}