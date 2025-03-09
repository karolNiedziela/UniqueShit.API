namespace UniqueShit.Infrastructure.Persistence.Options
{
    public sealed class ConnectionStringOptions
    {
        public const string SectionName = "UniqueShitDB";

        public string Value { get; }

        public ConnectionStringOptions(string value)
        {
            Value = value;
        }

        public static implicit operator string(ConnectionStringOptions connectionString) => connectionString.Value;
    }
}
