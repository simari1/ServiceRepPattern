using ServiceRep.Models;

namespace ServiceRep.Repositories
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetBooks();
    }
}
