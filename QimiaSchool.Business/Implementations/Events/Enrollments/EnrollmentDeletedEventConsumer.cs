using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Enrollments;

public class EnrollmentDeletedEventConsumer : IConsumer<EnrollmentDeletedEvent>
{
    private readonly ILogger<EnrollmentCreatedEventConsumer> _logger;

    public EnrollmentDeletedEventConsumer(ILogger<EnrollmentCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<EnrollmentDeletedEvent> context)
    {
        _logger.LogInformation("Enrollment deleted : {@Enrollment}", context.Message);

        return Task.CompletedTask;
    }
}
