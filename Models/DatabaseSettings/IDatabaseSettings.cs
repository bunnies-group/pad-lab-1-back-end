namespace Models.DatabaseSettings
{
    public interface IDatabaseSettings
    {
        string ConnectionsCollectionName { get; set; }
        string MessagesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}