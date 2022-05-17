using ServiceRepWithFactory.Models;

namespace ServiceRepWithFactory.Repositories
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetBooks();
    }
}
