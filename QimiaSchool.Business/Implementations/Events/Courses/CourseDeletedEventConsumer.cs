using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Courses;

public class CourseDeletedEventConsumer : IConsumer<CourseDeletedEvent>
{
    private readonly ILogger<CourseDeletedEventConsumer> _logger;

    public  CourseDeletedEventConsumer(ILogger<CourseDeletedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CourseDeletedEvent> context)
    {
        _logger.LogInformation("Course Deleted : {@Course}", context.Message);

        return Task.CompletedTask;
    }
}
