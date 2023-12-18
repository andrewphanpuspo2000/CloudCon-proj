using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Model;

public class BorrowBook
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string UserId { get; set; }
    public string userName { get; set; }
    public string BookId { get; set; }
    public string BookName { get; set; }
    public string Thumbnail { get; set; }
}