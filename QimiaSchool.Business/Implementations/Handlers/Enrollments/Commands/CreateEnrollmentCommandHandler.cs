using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Events.Enrollments;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.MessageBroker.Asbtractions;
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
    private readonly IEventBus _eventBus;

    public CreateEnrollmentCommandHandler(
        IEnrollmentManager enrollmentManager,
        IStudentManager studentManager,
        ICourseManager courseManager,
        IEventBus eventBus)
    {
        _enrollmentManager = enrollmentManager;
        _studentManager = studentManager;
        _courseManager = courseManager;
        _eventBus = eventBus;
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

        await _eventBus.PublishAsync(new EnrollmentCreatedEvent
        {
            EnrollmentId = enrollment.ID,
            StudentId = enrollment.StudentID,
            CourseId = enrollment.CourseID,
            Grade = (Grade)enrollment.Grade,
        });

        return enrollment.ID;
    }
}