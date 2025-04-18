namespace UniqueShit.Infrastructure.Authentication.Options
{
    public sealed class CreateAppUserEndpointFilterOptions
    {
        public const string SectionName = "CreateAppUserEndpointFilter";

        public string Key { get; set; } = default!;
    }
}
