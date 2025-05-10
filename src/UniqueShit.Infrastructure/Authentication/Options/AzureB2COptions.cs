namespace UniqueShit.Infrastructure.Authentication.Options
{
    public sealed class AzureB2COptions
    {
        public const string SectionName = "AzureAdB2C";

        public string Instance { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string Domain { get; set; } = default!;
        public string SignUpSignInPolicyId { get; set; } = default!;

        public string Authority =>
            $"{Instance}/{Domain}/{SignUpSignInPolicyId}/v2.0/";
    }
}
