using System;
using System.Runtime.Serialization;

namespace QimiaSchool.DataAccess.Exceptions;
[Serializable]
public class EntityNotFoundException<T> : Exception
{
    private readonly int id;

    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(int id)
    {
        this.id = id;
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public int Id => id;
}