using ServiceRepWithFactory.Models;
using ServiceRepWithFactory.Repositories;

namespace ServiceRepWithFactory.Services
{
    /// <summary>
    /// Serviceクラス
    /// 本来ならここにメインの業務ロジックが含まれる
    /// </summary>
    public class BooksService
    {
        private IBooksRepository _rep;

        /// <summary>
        /// DIでRepositoryがコンストラクタに注入される
        /// 場合によっては2つ以上のRepositoryが入る場合も
        /// </summary>
        /// <param name="rep"></param>
        public BooksService(IBooksRepository rep)
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
