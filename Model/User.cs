using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Model;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Role { get; set; }
    public string Name { get; set; }
    public int Age { get; set; } 
    public string email { get; set; }
    public string phone { get; set; }
    
    public string password { get; set; }

    public User()
    {
        this.Role = "admin";
    }
}
