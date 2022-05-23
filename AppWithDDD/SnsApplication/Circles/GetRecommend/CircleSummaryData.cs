
using AppWithDDD.SnsDomain.Models.Circles;

namespace AppWithDDD.SnsApplication.Circles.GetRecommend
{
    public class CircleSummaryData
    {
        public CircleSummaryData(Circle circle)
        {
            Id = circle.Id.ToString();
            Name = circle.Name.ToString();
        }

        public string Id { get; }
        public string Name { get; }
    }
}
