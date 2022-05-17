using Newtonsoft.Json;
using ServiceRep.Models;

namespace ServiceRep.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        string _filePath;

        public BooksRepository()
        {
            _filePath = @"./Resources/books.json";
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>();

            using (StreamReader r = new StreamReader(_filePath))
            {
                string json = r.ReadToEnd();
                books = JsonConvert.DeserializeObject<IEnumerable<Book>>(json).ToList();
            }

            return books;
        }
    }
}
