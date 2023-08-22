using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Enrollments;

public class EnrollmentCreatedEventConsumer : IConsumer<EnrollmentCreatedEvent>
{
    private readonly ILogger<EnrollmentCreatedEvent> _logger;

    public EnrollmentCreatedEventConsumer(ILogger<EnrollmentCreatedEvent> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<EnrollmentCreatedEvent> context)
    {
        _logger.LogInformation("Enrollment created{@Enrollment}", context.Message);

        return Task.CompletedTask;
    }
}