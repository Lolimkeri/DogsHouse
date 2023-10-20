using AspNetCoreRateLimit;

namespace DogsHouse.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void SetupRateLimiter(this IServiceCollection serviceCollection, IConfigurationSection configurationSection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            serviceCollection.AddInMemoryRateLimiting();
            serviceCollection.Configure<IpRateLimitOptions>(configurationSection);
            serviceCollection.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
