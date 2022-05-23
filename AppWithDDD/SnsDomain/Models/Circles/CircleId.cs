using System;

namespace AppWithDDD.SnsDomain.Models.Circles
{
    public class CircleId
    {
        public string Value { get; }

        public CircleId(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            Value = value;
        }
    }
}
