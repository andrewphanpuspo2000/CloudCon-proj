using System.Collections.Immutable;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication1.Model;

namespace WebApplication1.Services;

public class BorrowBookService
{
    
    private readonly IMongoCollection<BorrowBook> _borrowCollection;
    private readonly IMongoCollection<Books> _bookCollection;

    public BorrowBookService (IOptions<UserDatabaseSetting> userDatabaseSetting){
        var mongoClient = new MongoClient(
            userDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userDatabaseSetting.Value.DatabaseName);

        _borrowCollection = mongoDatabase.GetCollection<BorrowBook>(
            userDatabaseSetting.Value.BorrowCollection);
        _bookCollection = mongoDatabase.GetCollection<Books>(userDatabaseSetting.Value.BookCollection);
    }

    public async Task<object> AddBorrowBook(BorrowBook borrow)
    {
        try
        {
            await _borrowCollection.InsertOneAsync(borrow);
            FilterDefinition<Books> filter = Builders<Books>.Filter.Eq("Id",borrow.BookId);
            UpdateDefinition<Books> update = Builders<Books>.Update.Set(u => u.Status, "borrowed");

            await _bookCollection.UpdateOneAsync(filter, update);
            return new
            {
                status = "success",
                message = "Book has been borrowed",
            };
        }
        catch (Exception err)
        {
            return new
            {
                status="error",
                message=err.Message,
                
            };
        }


    }

    public async Task<object> ReturnBookServices(string id , string bookId)
    {
        FilterDefinition<BorrowBook> filter = Builders<BorrowBook>.Filter.And(Builders<BorrowBook>.Filter.Eq("Id",id),Builders<BorrowBook>.Filter.Eq("BookId",bookId));
        await _borrowCollection.DeleteOneAsync(filter);
        FilterDefinition<Books> filterBook = Builders<Books>.Filter.Eq("Id",bookId);
        UpdateDefinition<Books> update = Builders<Books>.Update.Set(u=>u.Status,"available");

        await _bookCollection.UpdateOneAsync(filterBook, update);
        return new { status="success",message="Book has been returned"};
    }

}