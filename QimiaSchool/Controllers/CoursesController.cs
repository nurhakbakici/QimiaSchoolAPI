using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.Business.Implementations.Commands.Courses.CourseDtos;
using QimiaSchool.Business.Implementations.Queries.Courses;
using QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;
using QimiaSchool.DataAccess.Entities;
using Serilog;

namespace QimiaSchool.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CoursesController : Controller
{
    private readonly IMediator _mediator;
    private readonly Serilog.ILogger _courseLogger;
    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
        _courseLogger = Log.ForContext("SourceContext", typeof(CoursesController).FullName);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCourse(
        [FromBody] CreateCourseDto course,
        CancellationToken cancellationToken)
    {

        _courseLogger.Information("Request for creating a course is accepted. Course:{@course}", course);

        var response = await _mediator.Send(new CreateCourseCommand(course), cancellationToken);

        return CreatedAtAction(
            nameof(GetCourse),
            new { Id = response },
            course);

    }

    [HttpGet("{id}")]
    public Task<CourseDto> GetCourse(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        _courseLogger.Information("Request for getting a course with ID is accepted. ID : {CourseId}", id);

        return _mediator.Send(
            new GetCourseQuery(id),
            cancellationToken);
    }

    [HttpGet]
    public Task<List<CourseDto>> GetCourses(CancellationToken cancellationToken)
    {
        _courseLogger.Information("Request for getting courses is accepted.");

        return _mediator.Send(
            new GetCoursesQuery(),
            cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(
        [FromRoute] int id,
        [FromBody] UpdateCourseDto course,
        CancellationToken cancellationToken)
    {
        _courseLogger.Information("Request for updating a course is accepted. Course:{@course}", course);

        await _mediator.Send(
            new UpdateCourseCommand(id, course),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        _courseLogger.Information("Request for deleting a course with ID is accepted. ID : {CourseId}", id);

        await _mediator.Send(
            new DeleteCourseCommand(id),
            cancellationToken);

        return NoContent();
    }
}
