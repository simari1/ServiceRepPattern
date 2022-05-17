using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceRep.Repositories;
using System.Linq;
using Test.ServiceRep.Test.Repositories;

namespace Test.ServiceRep
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
            global::ServiceRep.Services.BooksService booksService = new global::ServiceRep.Services.BooksService(_booksRepository);

            var books = booksService.GetBooks(true);

            Assert.IsNotNull(books);
            Assert.AreEqual(4, books.ToList().Count);
        }

        [TestMethod]
        public void When_returnBooks_is_false_then_Null()
        {
            global::ServiceRep.Services.BooksService booksService = new global::ServiceRep.Services.BooksService(_booksRepository);

            var books = booksService.GetBooks(false);

            Assert.IsNull(books);
        }
    }
}