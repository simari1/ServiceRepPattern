using AppWithDDD.SnsDomain.Models.Circles;

namespace AppWithDDD.SnsDomain.Models.Circles.Exceptioins
{
    class CanNotRegisterCircleException : Exception
    {
        public CanNotRegisterCircleException(Circle circle, string message) : base(message)
        {
            Id = circle?.Id.Value;
        }

        public string Id;
    }
}
