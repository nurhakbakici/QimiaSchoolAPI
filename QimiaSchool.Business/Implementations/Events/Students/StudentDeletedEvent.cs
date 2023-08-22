using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Students;

public record StudentDeletedEvent
{
    public int Id { get; init; }
}
