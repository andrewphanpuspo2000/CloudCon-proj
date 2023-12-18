using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Model;

namespace WebApplication1.Services;

public class UserServices
{
    private readonly IMongoCollection<User> _userCollection;

    public UserServices (IOptions<UserDatabaseSetting> userDatabaseSetting){
        var mongoClient = new MongoClient(
            userDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userDatabaseSetting.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(
            userDatabaseSetting.Value.UserCollectionName);
    }

    public async Task CreateUserAsync(User newUser)
         { 
             await _userCollection.InsertOneAsync(newUser);
             
         }


    public async Task<List<User>> GetUserAsync()
    {
        return await _userCollection.Find(_=>true).ToListAsync();
    }
    
    public async Task<User> updateUserAsync(string id, User user)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        UpdateDefinition<User> update = Builders<User>.Update.
            Set(u => u.Name, user.Name).Set(u=>u.Age,user.Age).Set(u=>u.email,user.email).Set(u=>u.phone,user.phone).Set(u=>u.password,user.password);
        FindOneAndUpdateOptions<User> option = new FindOneAndUpdateOptions<User>
        {
            ReturnDocument = ReturnDocument.After // Return the updated document
        };
        var result=await _userCollection.FindOneAndUpdateAsync(filter, update,option);
        return result;
    }
}