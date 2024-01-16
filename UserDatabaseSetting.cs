namespace WebApplication1;

public class UserDatabaseSetting
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UserCollectionName { get; set; } = null!;
    public string BookCollection { get; set; } = null!;
    public string BorrowCollection { get; set; } = null!;
    public string ReviewCollection { get; set; } = null!;

}