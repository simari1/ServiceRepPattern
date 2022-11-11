using AppWithDDD.SnsDomain.Models.Circles;

namespace AppWithDDD.SnsApplication.Circles.GetRecommend
{
    public class CircleGetRecommendResult
    {
        public CircleGetRecommendResult(List<Circle> recommendCircles)
        {
            Summaries.Concat(recommendCircles.Select(x => new CircleSummaryData(x)));
        }

        public List<CircleSummaryData> Summaries { get; }
    }
}
