namespace Models.DatabaseSettings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionsCollectionName { get; set; }
        public string MessagesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    } 
}