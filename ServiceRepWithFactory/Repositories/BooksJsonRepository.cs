using Newtonsoft.Json;
using ServiceRepWithFactoryWithFactory.Models;

namespace ServiceRepWithFactoryWithFactory.Repositories
{
    public class BooksJsonRepository : IBooksRepository
    {
        string _filePath;

        public BooksJsonRepository()
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
