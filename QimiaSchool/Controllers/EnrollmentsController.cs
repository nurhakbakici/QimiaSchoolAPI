using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QimiaSchool.Business.Implementations.Commands.Enrollments;
using QimiaSchool.Business.Implementations.Commands.Enrollments.EnrollmentDtos;
using QimiaSchool.Business.Implementations.Queries.Enrollments;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;

namespace QimiaSchool.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class EnrollmentsController : Controller
{
    private readonly IMediator _mediator;
    public EnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateEnrollment(
        [FromBody] CreateEnrollmentDto enrollment,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateEnrollmentCommand(enrollment), cancellationToken);

        return CreatedAtAction(
            nameof(GetEnrollment),
            new { Id = response },
            enrollment);

    }

    [HttpGet("{id}")]
    public Task<EnrollmentDto> GetEnrollment(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetEnrollmentQuery(id),
            cancellationToken);
    }

    [HttpGet]
    public Task<List<EnrollmentDto>> GetEnrollments(
        CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetEnrollmentsQuery(),
            cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEnrollment(
        [FromRoute] int id,
        [FromBody] UpdateEnrollmentDto enrollment,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateEnrollmentCommand(id, enrollment),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEnrollment(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteEnrollmentCommand(id),
            cancellationToken);

        return NoContent();
    }
}
