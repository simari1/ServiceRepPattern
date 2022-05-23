using AppWithDDD.Infrastructure.Circles.Repositories;

namespace AppWithDDD.SnsDomain.Models.Circles.Services
{
    /// <summary>
    /// 複数のドメインモデルにまたがる処理をするので、DomainService
    /// </summary>
    public class CircleService
    {
        private readonly ICircleRepository circleRepository;

        public CircleService(ICircleRepository circleRepository)
        {
            this.circleRepository = circleRepository;
        }

        public bool Exists(Circle circle)
        {
            var duplicated = circleRepository.Find(circle.Name);
            return duplicated != null;
        }
    }
}
