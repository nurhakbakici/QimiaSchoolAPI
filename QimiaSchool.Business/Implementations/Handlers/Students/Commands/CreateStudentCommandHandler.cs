using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Commands;
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IStudentManager _studentManager;

    public CreateStudentCommandHandler(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstMidName = request.Student.FirstMidName,
            LastName = request.Student.LastName,
            EnrollmentDate = DateTime.Now,
        };

        await _studentManager.CreateStudentAsync(student, cancellationToken);

        return student.ID;
    }
}
