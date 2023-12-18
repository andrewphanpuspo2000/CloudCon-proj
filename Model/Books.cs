using MongoDB.Bson;
 using MongoDB.Bson.Serialization.Attributes;
 
 namespace WebApplication1.Model;
 
 public class Books
 {
     [BsonId]
     [BsonRepresentation(BsonType.ObjectId)]
     public string? Id { get; set; }
 
     public string? Status { get; set; }
     public string BookName { get; set; }
     public string Author { get; set; } 
     public string Published { get; set; }
     public string Thumbnail { get; set; }

     public Books()
     {
         this.Status = "available";
         this.Author = "";
         this.BookName = "";
         this.Published = "";
         this.Thumbnail = "";
     }

 }