using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit> // it will handle commands from UpdateStudentCommand type and returna a unit type result.
{
    private readonly IStudentManager _studentManager;

    public UpdateStudentCommandHandler(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }


    public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentManager.GetStudentByIdAsync(request.Id, cancellationToken); // to update we must first get an item and we will get it by getStudentById method.

        student.FirstMidName  = request.Student.FirstMidName ?? student.FirstMidName;
        student.LastName = request.Student.LastName ?? student.LastName;
        student.EnrollmentDate = request.Student.EnrollmentDate ?? student.EnrollmentDate;

        await _studentManager.UpdateStudentAsync(student, cancellationToken);

        return Unit.Value;
    }
}