using Microsoft.AspNetCore.Builder;

public static class StartupExtensions
{
    public static void UseMvcControllersConfiguration(this IApplicationBuilder app)
    {
        app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

        // app.UseMvc();
    }
}