using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Model;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Role { get; set; }
    public string FName { get; set; }
    public string LName { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; }

    public User()
    {
        this.Role = "admin";
    }
}
