using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Students;

public record StudentUpdatedEvent
{
    public int Id { get; init; }
    public string FirstMidName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}