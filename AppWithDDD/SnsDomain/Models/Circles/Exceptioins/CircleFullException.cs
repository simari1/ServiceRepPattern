using AppWithDDD.SnsDomain.Models.Circles;
using System;

namespace AppWithDDD.SnsDomain.Models.Circles.Exceptioins
{

    public class CircleFullException : Exception
    {
        public CircleFullException(CircleId id, string message = null) : base(message)
        {
            Id = id;
        }

        public CircleId Id { get; }
    }
}
