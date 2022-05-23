using AppWithDDD.SnsDomain.Models.Circles;

namespace AppWithDDD.Infrastructure.Circles.Repositories
{
    public interface ICircleRepository
    {
        public void Save(Circle circle);
        public Circle Find(CircleId id);
        public Circle Find(CircleName name);
        public IQueryable<Circle> FindAll();
    }
}
