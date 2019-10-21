using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;

using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Helpers
{
    public class MSALAuthenticationProvider : Microsoft.Graph.IAuthenticationProvider
    {
        private readonly IConfidentialClientApplication _clientApplication;
        private readonly Helpers.IConfidentialClientApplicationBuilderExtension _ConfidentialClientApplicationBuilderExtension;
        private string[] _scopes;
        /*
        public MSALAuthenticationProvider(IConfidentialClientApplication clientApplication)//, string[] scopes)
        {
            //  where IConfidentialClientApplication  = IConfidentialClientApplication.Build();
            _clientApplication = clientApplication;
            _scopes = new string[] { "https://graph.microsoft.com/.default" };// scopes;
        }
        */
        public MSALAuthenticationProvider(Helpers.IConfidentialClientApplicationBuilderExtension builder)//, string[] scopes)
        {
            //  where IConfidentialClientApplication  = IConfidentialClientApplication.Build();
            _ConfidentialClientApplicationBuilderExtension = builder;
            _clientApplication = builder.Build();
            _scopes = new string[] { "https://graph.microsoft.com/.default" };// scopes;
        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var token = await GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        public async Task<string> GetTokenAsync()
        {
            AuthenticationResult authResult = null;
            authResult = await _clientApplication.AcquireTokenForClient(_scopes).ExecuteAsync();
            return authResult.AccessToken;
        }
    }
}
