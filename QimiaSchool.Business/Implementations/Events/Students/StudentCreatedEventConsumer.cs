using MassTransit;
using Microsoft.Extensions.Logging;
using QimiaSchool.Business.Implementations.Events.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Students;

public class StudentCreatedEventConsumer : IConsumer<StudentCreatedEvent>
{
    private readonly ILogger<StudentCreatedEventConsumer> _logger;

    public StudentCreatedEventConsumer(ILogger<StudentCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<StudentCreatedEvent> context)
    {
        _logger.LogInformation("Student created: {@Student}", context.Message);

        return Task.CompletedTask;
    }
}