using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QimiaSchool.Business.Implementations.Commands.Students;
using QimiaSchool.Business.Implementations.Commands.Students.StudentDtos;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;
using QimiaSchool.Business.Implementations.Queries.Students;
using Serilog;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class StudentsController : Controller
{
    private readonly IMediator _mediator;
    private readonly Serilog.ILogger _studentLogger;
    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
        _studentLogger = Log.ForContext("SourceContext", typeof(StudentsController).FullName);
    }

    [HttpPost]
    public async Task<ActionResult> CreateStudent(
        [FromBody] CreateStudentDto student,
        CancellationToken cancellationToken)
    {
        _studentLogger.Information("Request for creating a student is accepted. Student:{@student}",student);


        var response = await _mediator.Send(new CreateStudentCommand(student), cancellationToken);

        return CreatedAtAction(
            nameof(GetStudent),
            new { Id = response },
            student);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent(
       [FromRoute] int id,
       [FromBody] UpdateStudentDto student,
       CancellationToken cancellationToken)
    {
        _studentLogger.Information("Request for updating a student is accepted. Student:{@student}", student);

        await _mediator.Send(
            new UpdateStudentCommand(id, student),
            cancellationToken);

        return NoContent();
    }


    [HttpGet("{id}")]
    public Task<StudentDto> GetStudent(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        _studentLogger.Information("Request for getting a student is accepted.");

        return _mediator.Send(
        new GetStudentQuery(id),
            cancellationToken);
    }

    [HttpGet]
    public Task<List<StudentDto>> GetStudents(CancellationToken cancellationToken)
    {
        _studentLogger.Information("Request for getting students is accepted.");

        return _mediator.Send(
            new GetStudentsQuery(),
            cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        _studentLogger.Information("Request for deleting a student is accepted.");

        await _mediator.Send(
            new DeleteStudentCommand(id),
            cancellationToken);

        return NoContent();
    }
}
