using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
//
namespace Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureADAuthenticationProvider(this IServiceCollection services, IConfiguration configuration)
        {
            //  1) Helpers.IConfidentialClientApplicationBuilderExtension _ConfidentialClientApplicationBuilder:
            //      ConfidentialClientApplicationBuilderExtension(IServiceCollection services, Action<Models.AzureADOptions> configureOptions)
            //  2) Microsoft.Graph.IAuthenticationProvider _MSALAuthenticationProvider:
            //      MSALAuthenticationProvider(IConfidentialClientApplication clientApplication, string[] scopes)

            //services.AddTransient<Helpers.IConfidentialClientApplicationBuilderExtension, Helpers.ConfidentialClientApplicationBuilderExtension>();
            //services.AddTransient<Helpers.ConfidentialClientApplicationBuilderExtension>();
            //var sp = services.BuildServiceProvider();
            //var service = (Helpers.ConfidentialClientApplicationBuilderExtension)sp.GetService(typeof(Helpers.ConfidentialClientApplicationBuilderExtension));

            var s = services.AddTransient<Helpers.IConfidentialClientApplicationBuilderExtension, Helpers.ConfidentialClientApplicationBuilderExtension>()
                            .AddTransient<Microsoft.Graph.IAuthenticationProvider, Helpers.MSALAuthenticationProvider>()
                            .AddTransient<Microsoft.Graph.GraphServiceClient>()
                            ;
            return s;

        }
    }
}
