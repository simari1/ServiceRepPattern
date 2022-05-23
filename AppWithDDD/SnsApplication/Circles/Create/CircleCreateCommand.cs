namespace AppWithDDD.SnsApplication.Circles.Create
{
    public class CircleCreateCommand
    {
        public string UserId { get; }
        public string Name { get; }

        public CircleCreateCommand(string userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}
