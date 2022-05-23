using AppWithDDD.SnsDomain.Models.Circles;
using System;

namespace AppWithDDD.SnsDomain.Models.Circles.Exceptioins
{
    public class CircleNotFoundException : Exception
    {
        public CircleNotFoundException(CircleId id)
        {
            Id = id.Value;
        }

        public CircleNotFoundException(CircleId id, string message) : base(message)
        {
            Id = id.Value;
        }

        public string Id { get; }
    }
}
