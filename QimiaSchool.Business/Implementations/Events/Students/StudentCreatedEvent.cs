using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Events.Students;

public record StudentCreatedEvent
{
    public int Id { get; init; }
    public string FirstMidName { get; init; } = string.Empty;
    public string LastName { get; init;} = string.Empty;

    // init is used to make an object readible during initializaiton and once its created
    // it makes it readonly. So basically it means that you can only set value during the creation of the object.
    // init is only used with record types.
}
