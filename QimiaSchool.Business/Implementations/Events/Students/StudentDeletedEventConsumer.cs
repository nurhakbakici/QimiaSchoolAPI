using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Students;

public class StudentDeletedEventConsumer : IConsumer<StudentDeletedEvent>
{
    private readonly ILogger<StudentDeletedEventConsumer> _logger;

    public  StudentDeletedEventConsumer(ILogger<StudentDeletedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<StudentDeletedEvent> context)
    {
        _logger.LogInformation("Student deleted: {@student}", context.Message);

        return Task.CompletedTask;
    }
}
