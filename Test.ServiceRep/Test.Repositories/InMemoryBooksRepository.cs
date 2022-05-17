using ServiceRep.Models;
using ServiceRep.Repositories;
using System.Collections.Generic;

namespace Test.ServiceRep.Test.Repositories
{
    internal class InMemoryBooksRepository : IBooksRepository
    {
        List<Book> _books;

        public InMemoryBooksRepository()
        {
            _books = new List<Book>();
            _books.Add(new Book { Id = "4", Title = "Hello 4", Author = "john doe 4" });
            _books.Add(new Book { Id = "5", Title = "Hello 5", Author = "john doe 5" });
            _books.Add(new Book { Id = "6", Title = "Hello 6", Author = "john doe 6" });
            _books.Add(new Book { Id = "7", Title = "Hello 7", Author = "john doe 7" });
        }

        public IEnumerable<Book> GetBooks()
        {
            return _books;
        }
    }
}
