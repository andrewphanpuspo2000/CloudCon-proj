using System.Collections.Immutable;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication1.Model;

namespace WebApplication1.Services;

public class ReviewServices
{
    private readonly IMongoCollection<Review> _reviewCollection;

    public ReviewServices(IOptions<UserDatabaseSetting> userDatabaseSetting)
    {
        var mongoClient = new MongoClient(
            userDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userDatabaseSetting.Value.DatabaseName);

        _reviewCollection = mongoDatabase.GetCollection<Review>(
            userDatabaseSetting.Value.ReviewCollection);
    }

    public async Task<object> AddCommentService(Review data)
    { 
          await _reviewCollection.InsertOneAsync(data);
          return new { status="success", messasge="comment has been inputted" };
    }

    public async Task<object> DeleteCommentServices(string id)
    {
        FilterDefinition<Review> filter = Builders<Review>.Filter.Eq("Id", id);
        await _reviewCollection.DeleteOneAsync(filter);
        return new { status="success",message="comment is deleted"};
    }
}