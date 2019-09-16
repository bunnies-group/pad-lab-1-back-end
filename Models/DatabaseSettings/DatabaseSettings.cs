namespace Models.DatabaseSettings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string SubscriptionsCollectionName { get; set; }
        public string MessagesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    } 
}