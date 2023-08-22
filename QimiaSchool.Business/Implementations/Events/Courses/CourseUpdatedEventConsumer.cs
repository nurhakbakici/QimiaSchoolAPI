using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Courses;

public class CourseUpdatedEventConsumer : IConsumer<CourseUpdatedEvent>
{
    private readonly ILogger<CourseUpdatedEventConsumer> _logger;

    public CourseUpdatedEventConsumer(ILogger<CourseUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CourseUpdatedEvent> context)
    {
        _logger.LogInformation("Course Updated {@Course}", context);
       
        return Task.CompletedTask;
    }
}
