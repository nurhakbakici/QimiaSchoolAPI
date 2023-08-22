using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Enrollments;

public class EnrollmentUpdatedEventConsumer : IConsumer<EnrollmentUpdatedEvent>
{
    private readonly ILogger<EnrollmentUpdatedEventConsumer> _logger;

    public EnrollmentUpdatedEventConsumer(ILogger<EnrollmentUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<EnrollmentUpdatedEvent> context)
    {
        _logger.LogInformation("Enrollment Updated{@Enrollment}", context.Message);

        return Task.CompletedTask;
    }
}
