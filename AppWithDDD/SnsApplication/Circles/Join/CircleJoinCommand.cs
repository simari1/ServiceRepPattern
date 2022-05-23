namespace AppWithDDD.SnsApplication.Circles.Join
{
    public class CircleJoinCommand
    {
        public string UserId { get; }
        public string CircleId { get; }

        public CircleJoinCommand(string userId, string circleId)
        {
            UserId = userId;
            CircleId = circleId;
        }
    }
}
