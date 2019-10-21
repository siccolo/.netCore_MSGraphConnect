using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Helpers
{

    public interface IConfidentialClientApplicationBuilderExtension
    {
        IConfidentialClientApplication Build();
    }

    public class ConfidentialClientApplicationBuilderExtension: IConfidentialClientApplicationBuilderExtension
    {
        private readonly Models.AzureADOptions _AzureADOptions;
        public ConfidentialClientApplicationBuilderExtension(IOptions<Models.AzureADOptions> azureOptions)
        {
            _AzureADOptions = azureOptions.Value;
        }
        public IConfidentialClientApplication Build()
        {
            //Models.AzureADOptions options = new Models.AzureADOptions();
            //_Configuration.Bind("AzureAD", options);
            /*
                var clientId = "<AzureADAppClientId>";
                var clientSecret = "<AzureADAppClientSecret>";
                var redirectUri = "<AzureADAppRedirectUri>";
                var authority = "https://login.microsoftonline.com/<AzureADAppTenantId>/v2.0";
            */
            var cca = ConfidentialClientApplicationBuilder.Create(_AzureADOptions.ClientId)
                                              .WithAuthority(_AzureADOptions.Authority)
                                              .WithRedirectUri(_AzureADOptions.RedirectUri)
                                              .WithClientSecret(_AzureADOptions.ClientSecret)
                                              .Build();

            return cca;
        }
    }
}
