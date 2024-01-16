using System.Collections.Immutable;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication1.Model;

namespace WebApplication1.Services;

public class BooksServices
{
    private readonly IMongoCollection<Books> _bookCollection;

    public BooksServices (IOptions<UserDatabaseSetting> userDatabaseSetting){
        var mongoClient = new MongoClient(
            userDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userDatabaseSetting.Value.DatabaseName);

        _bookCollection = mongoDatabase.GetCollection<Books>(
            userDatabaseSetting.Value.BookCollection);
    }

    public async Task<object> AddBook(Books book)
    {
          await _bookCollection.InsertOneAsync(book);
          return new { status = "success", message = "book has been addedd" };
    }

    public async Task<Books> UpdateBoook(string id,Books book)
    {
        FilterDefinition<Books> filter = Builders<Books>.Filter.Eq("Id", id);
        UpdateDefinition<Books> update = Builders<Books>.Update.Set(u => u.BookName, book.BookName)
            .Set(u => u.Author, book.Author).Set(u => u.Published, book.Published);
        FindOneAndUpdateOptions<Books> option = new FindOneAndUpdateOptions<Books>
        {
            ReturnDocument = ReturnDocument.After // Return the updated document
        };
        var result = await _bookCollection.FindOneAndUpdateAsync(filter,update,option);
        return result;
    }

    public async Task<List<Books>> GetBooks()
    {
        var books = await _bookCollection.Find(book => true).ToListAsync();
        return books;
    }
}