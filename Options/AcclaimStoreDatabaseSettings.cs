namespace Acclaim.Api.Options
{
    public class AcclaimStoreDatabaseSettings : IAcclaimStoreDatabaseSettings
    {
        public string AcclaimsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAcclaimStoreDatabaseSettings
    {
        string AcclaimsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
