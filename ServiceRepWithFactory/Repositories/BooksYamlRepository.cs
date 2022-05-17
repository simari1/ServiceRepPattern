using ServiceRepWithFactoryWithFactory.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ServiceRepWithFactoryWithFactory.Repositories
{
    public class BooksYamlRepository : IBooksRepository
{
    string _filePath;

    public BooksYamlRepository()
    {
        _filePath = @"./Resources/books.yaml";
    }

    public IEnumerable<Book> GetBooks()
    {
        var books = new List<Book>();

        using (StreamReader r = new StreamReader(_filePath))
        {
            string json = r.ReadToEnd();

            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)  // see height_in_inches in sample yml 
            .Build();

            books = deserializer.Deserialize<List<Book>>(json);
        }

        return books;
    }
}
}
