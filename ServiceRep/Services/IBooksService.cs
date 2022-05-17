using ServiceRepWithFactory.Models;

namespace ServiceRepWithFactory.Services
{
    public interface IBooksService
    {
        IEnumerable<Book>? GetBooks(bool returnBooks = true);
    }
}
