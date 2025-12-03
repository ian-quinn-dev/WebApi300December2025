namespace AppHost;

public static class AppHostExtensions
{
    extension<T>(IResourceBuilder<T> builder) where T : IResourceWithEnvironment
    {
        internal IResourceBuilder<T> WithSharedLoggingLevels()
        {
            var dict = new Dictionary<string, string>
            {
                { "Default", "Information" },
                { "System", "Warning" },
                { "Microsoft", "Warning" },
                { "Microsoft.Hosting", "Information" },
                { "NpgSql", "Warning" },
                { "Wolverine", "Warning" }
            };

            foreach (var item in dict.Keys) builder = builder.WithEnvironment($"Logging__LogLevel__{item}", dict[item]);
            return builder;
        }
    }
}