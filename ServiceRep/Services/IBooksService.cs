using ServiceRep.Models;

namespace ServiceRep.Services
{
    public interface IBooksService
    {
        IEnumerable<Book>? GetBooks(bool returnBooks = true);
    }
}
