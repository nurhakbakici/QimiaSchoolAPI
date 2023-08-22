using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Students;

public class StudentUpdatedEventConsumer : IConsumer<StudentUpdatedEvent>
{
    private readonly ILogger<StudentUpdatedEventConsumer> _logger;

    public StudentUpdatedEventConsumer(ILogger<StudentUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<StudentUpdatedEvent> context)
    {
        _logger.LogInformation("Student Updated{@Student}", context.Message);

        return Task.CompletedTask;
    }
}
