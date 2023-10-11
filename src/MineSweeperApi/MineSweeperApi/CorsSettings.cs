namespace MineSweeperApi;

public static class CorsSettings
{
    public static void RunCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowMineSweeper", builder =>
            {
                builder.WithOrigins(
                        "https://minesweeper-test.studiotg.ru",
                        "minesweeper-test.studiotg.ru/:1")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
            });
        });
    }
}