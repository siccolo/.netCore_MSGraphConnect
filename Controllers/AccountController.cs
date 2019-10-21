using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Extensions;

namespace WebAPI_MSGraphConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Microsoft.Graph.GraphServiceClient _GraphServiceClient;    //  in AddAzureADAuthenticationProvider()
        private readonly Microsoft.Extensions.Logging.ILogger<AccountController> _Logger;
        public AccountController(Microsoft.Graph.GraphServiceClient graphclient, Microsoft.Extensions.Logging.ILogger<AccountController> logger)
        {
            _GraphServiceClient = graphclient ?? throw new System.ArgumentNullException("GraphServiceClient is missing");
            _Logger = logger;
        }

        [HttpGet]
        [Route("getall")]
        //public async Task<IActionResult> GetAll()
        public async IAsyncEnumerable<Models.GraphUser> GetAll()
        {
            //https://docs.microsoft.com/en-us/graph/api/resources/user?view=graph-rest-1.0
             
            var graphResult = await _GraphServiceClient.Users.Request().Select("department,displayName,givenName,jobTitle,mail").GetAsync();   
            foreach ( var user in graphResult)
            {
                var graphUser = new Models.GraphUser(user.Department, user.DisplayName, user.GivenName, user.JobTitle, user.Mail);
                yield return graphUser;
            }
            while (graphResult.NextPageRequest != null)
            {
                graphResult = await graphResult.NextPageRequest.GetAsync();
                foreach (var user in graphResult)
                {
                    var graphUser = new Models.GraphUser(user.Department, user.DisplayName, user.GivenName, user.JobTitle, user.Mail);
                    yield return graphUser;
                }
            }


            /*
            var groups = await _GraphServiceClient.Groups.Request().Select(x => new { x.Id, x.DisplayName }).GetAsync();
            do
            {
                foreach (var group in groups)
                {
                    var users = await _GraphServiceClient.Groups[group.Id].Members.Request().Select("department,displayName,givenName,jobTitle,mail").GetAsync();

                    do
                    {
                        foreach (var user in users)
                        {
                            var graphUser = new Models.GraphUser(user.Department, user.DisplayName, user.GivenName, user.JobTitle, user.Mail);
                            yield return graphUser;
                        }
                    }
                    while (users.NextPageRequest != null && (users = await users.NextPageRequest.GetAsync()).Count > 0);
                }
            }
            while (groups.NextPageRequest != null && (groups = await groups.NextPageRequest.GetAsync()).Count > 0);
            */
        }

        [HttpGet]
        [Route("get")]
        public async IAsyncEnumerable<Models.GraphUser> Get(string userEmail)
        {

            if (String.IsNullOrWhiteSpace(userEmail))
            {
                throw new ArgumentNullException("userEmail is empty");
            }

            var graphResult = await _GraphServiceClient.Users[userEmail].Request().Select("department,displayName,givenName,jobTitle,mail,photos,photo").GetAsync();
            
            var photo  = await _GraphServiceClient.Users[userEmail].Photo.Content.Request().GetAsync();
            var photo_Base64 = Convert.ToBase64String(photo.ReadAsBytes());

            var graphUser = new Models.GraphUser(graphResult.Department, graphResult.DisplayName, graphResult.GivenName, graphResult.JobTitle, graphResult.Mail, photo_Base64);

            yield return graphUser;
        }
        
    }
}