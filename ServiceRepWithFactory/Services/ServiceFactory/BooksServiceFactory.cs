using ServiceRepWithFactoryWithFactory.Repositories;

namespace ServiceRepWithFactoryWithFactory.Services.ServiceFactory
{
    public class BooksServiceFactory
    {
        static Dictionary<string, IBooksService> booksServices = new Dictionary<string, IBooksService>();

        private IBooksRepository _booksJsonRep;
        private IBooksRepository _booksYamlRep;

        public BooksServiceFactory()
        {
            if (booksServices != null)
            {
                this._booksJsonRep = new BooksJsonRepository();
                this._booksYamlRep = new BooksYamlRepository();
                booksServices.Add("json", new BooksJsonService(_booksJsonRep));
                booksServices.Add("yaml", new BooksYamlService(_booksYamlRep));
            }
        }

        public IBooksService CreateBooksService(string mode)
        {
            return booksServices[mode];
        }
    }
}
