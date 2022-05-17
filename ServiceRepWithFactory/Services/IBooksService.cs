using ServiceRepWithFactoryWithFactory.Models;

namespace ServiceRepWithFactoryWithFactory.Services
{
    public interface IBooksService
    {
        IEnumerable<Book>? GetBooks(bool returnBooks = true);
    }
}
