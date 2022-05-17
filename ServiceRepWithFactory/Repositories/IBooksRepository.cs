using ServiceRepWithFactoryWithFactory.Models;

namespace ServiceRepWithFactoryWithFactory.Repositories
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetBooks();
    }
}
