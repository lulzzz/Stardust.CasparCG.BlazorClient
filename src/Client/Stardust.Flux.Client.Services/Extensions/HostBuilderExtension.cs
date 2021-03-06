using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using Refit;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Stardust.Flux.Client.Services.Extensions
{
    public static class HostBuilderExtension
    {
        public static IServiceCollection AddFluxClientServices(this IServiceCollection services, IConfiguration configuration)
        {

            ManageRefitClient(services, configuration);
            AddScheduleServices(services, configuration);
            services.AddScoped<IEncodingOptionProvider, EncodingOptionProvider>();
            return services;

        }



        public static WebAssemblyHostBuilder AddFluxClientServices(this WebAssemblyHostBuilder builder)
        {

            ManageRefitClient(builder.Services, builder.Configuration) ;
            AddScheduleServices(builder.Services, builder.Configuration);

            return builder;

        }


     

        private static void ManageRefitClient(IServiceCollection services, IConfiguration configuration)
        {
           services
                .AddRefitClient<ILicenceClientApi>()
                .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(configuration["CoreApiUrl"]);
                    });

            services
                 .AddRefitClient<IYoutubeApi>()
                 .ConfigureHttpClient(c =>
                 {
                     c.BaseAddress = new Uri(configuration["YoutubeApiUrl"]);
                 })
                 //Retry policy using Polly
                 //You could also add a fallback policy, a circuit-breaker or any combination of these
                 .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                 .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[] {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                 }));
            services.AddHttpClient<IYoutubeAccountManager, YoutubeAccountManager>(client =>
                 {
                     client.BaseAddress = new Uri(configuration["YoutubeApiUrl"]);
                 }).ConfigurePrimaryHttpMessageHandler(() =>
                 {
                     return new HttpClientHandler
                     {
                         AllowAutoRedirect = false
                     };
                 }); ;
        }





        private static void AddScheduleServices(IServiceCollection services, IConfiguration configuration)
        {
         
                services
                       .AddRefitClient<IRecordClientApi>()
                       .ConfigureHttpClient(c =>
                       {
                           c.BaseAddress = new Uri(configuration["ScheduleApiUrl"]);
                       })
                       //Retry policy using Polly
                       //You could also add a fallback policy, a circuit-breaker or any combination of these
                       .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                       .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[] {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                       }));
                    

         


            services.AddScoped<IRecordModelService, RecordModelService>();
        }
    }
}
