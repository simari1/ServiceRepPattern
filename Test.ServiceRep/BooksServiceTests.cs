using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceRepWithFactoryWithFactory.Repositories;
using ServiceRepWithFactoryWithFactory.Services;
using System.Linq;
using Test.ServiceRepWithFactory.Test.Repositories;

namespace Test.ServiceRepWithFactory
{
    /// <summary>
    /// BooksServiceクラスをテストするテストクラス
    /// </summary>
    [TestClass]
    public class BooksService
    {
        IBooksRepository _booksRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Testの際はIn Memoryのテストメソッドを使う
            _booksRepository = new InMemoryBooksRepository();
        }

        [TestMethod]
        public void When_returnBooks_is_true_then_NotNull()
        {
            BooksJsonService booksJsonService = new BooksJsonService(_booksRepository);
            var books = booksJsonService.GetBooks(true);

            Assert.IsNotNull(books);
            Assert.AreEqual(4, books.ToList().Count);
        }

        [TestMethod]
        public void When_returnBooks_is_false_then_Null()
        {
            BooksJsonService booksJsonService = new BooksJsonService(_booksRepository);
            var books = booksJsonService.GetBooks(false);

            Assert.IsNull(books);
        }
    }
}