using ServiceRepWithFactoryWithFactory.Models;
using ServiceRepWithFactoryWithFactory.Repositories;
using System.Collections.Generic;

namespace Test.ServiceRepWithFactory.Test.Repositories
{
    internal class InMemoryBooksRepository : IBooksRepository
    {
        List<Book> _books;

        public InMemoryBooksRepository()
        {
            _books = new List<Book>();
            _books.Add(new Book { id = "4", title = "Hello 4", author = "john doe 4" });
            _books.Add(new Book { id = "5", title = "Hello 5", author = "john doe 5" });
            _books.Add(new Book { id = "6", title = "Hello 6", author = "john doe 6" });
            _books.Add(new Book { id = "7", title = "Hello 7", author = "john doe 7" });
        }

        public IEnumerable<Book> GetBooks()
        {
            return _books;
        }
    }
}
