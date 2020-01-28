using Domain;
using Microsoft.AspNetCore.Components.Builder;
using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SharedComponents.Weather;

namespace TimeManagementClient
{
    public class Startup
    {
        private void RemoveDefaultHttpClient(IServiceCollection services)
        {
            var client = services.Single(s => s.ServiceType == typeof(HttpClient));
            services.Remove(client);
        }

        private void AddApiHttpClient(IServiceCollection services)
        {
            Type monoWasmHttpMessageHandlerType = Assembly
                                            .Load("WebAssembly.Net.Http")
                                            .GetType("WebAssembly.Net.Http.HttpClient.WasmHttpMessageHandler");

            services.AddScoped(monoWasmHttpMessageHandlerType);

            services.AddHttpClient("api", c =>
            {
                c.BaseAddress = new Uri("https://localhost:6001/api/");
            })
            .ConfigurePrimaryHttpMessageHandler(sp =>
                  (HttpMessageHandler)sp.GetService(monoWasmHttpMessageHandlerType));

            services.AddTransient<HttpClient>(s =>
            {
                var factory = s.GetService<IHttpClientFactory>();

                return factory.CreateClient("api");
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            RemoveDefaultHttpClient(services);
            AddApiHttpClient(services);

            services.AddScoped<ITaskService, TasksService>();
            services.AddScoped<IWeatherApi, WeatherApi>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
