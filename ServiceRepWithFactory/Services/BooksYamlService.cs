using ServiceRepWithFactoryWithFactory.Models;
using ServiceRepWithFactoryWithFactory.Repositories;

namespace ServiceRepWithFactoryWithFactory.Services
{
    /// <summary>
    /// Serviceクラス
    /// 本来ならここにメインの業務ロジックが含まれる
    /// </summary>
    public class BooksYamlService : IBooksService
    {
        private IBooksRepository _rep;

        /// <summary>
        /// DIでRepositoryがコンストラクタに注入される
        /// </summary>
        /// <param name="rep"></param>
        public BooksYamlService(IBooksRepository rep)
        {
            this._rep = rep;
        }

        /// <summary>
        /// サンプル業務ロジック
        /// クエリストリングにreturnBooks = trueで入ってきた場合のみデータを返す
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book>? GetBooks(bool returnBooks = true)
        {
            if (returnBooks)
            {
                return _rep.GetBooks();
            }
            else
            {
                return null;
            }
            ;
        }
    }
}
