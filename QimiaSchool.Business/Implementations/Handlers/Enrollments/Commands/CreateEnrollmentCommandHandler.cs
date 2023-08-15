using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Commands;

public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, int>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IStudentManager _studentManager;
    private readonly ICourseManager _courseManager;

    public CreateEnrollmentCommandHandler(
        IEnrollmentManager enrollmentManager,
        IStudentManager studentManager,
        ICourseManager courseManager)
    {
        _enrollmentManager = enrollmentManager;
        _studentManager = studentManager;
        _courseManager = courseManager;
    } 


    public async Task<int> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentManager.GetStudentByIdAsync(request.Enrollment.StudentId, cancellationToken);
        var course = await _courseManager.GetCourseByIdAsync(request.Enrollment.CourseId, cancellationToken);
        var enrollment = new Enrollment
        {
            Student = student,
            Course = course,
            Grade = request.Enrollment.Grade,
        };


        await _enrollmentManager.CreateEnrollmentAsync(enrollment, cancellationToken);

        return enrollment.ID;
    }
}