using System.Text;
using System;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading;
using Stardust.Flux.PublishApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Auth.OAuth2.Responses;

namespace Stardust.Flux.PublishApi.Youtube
{
    public class AuthenticateService
    {

        private readonly YoutubeApiOptions apiOptions;
        private readonly PublishContext publishContext;

        public AuthenticateService(IOptions<YoutubeApiOptions> options, PublishContext publishContext)
        {
            apiOptions = options.Value;
            this.publishContext = publishContext;
        }

        // This is a method we'll use to obtain the authorization code flow
        private AuthorizationCodeFlow GetGoogleAuthorizationCodeFlow(string description, params string[] scopes)
        {

            var clientSecrets = new ClientSecrets { ClientId = apiOptions.ClientId, ClientSecret = apiOptions.ClientSecrets };
            var initializer = new GoogleAuthorizationCodeFlow.Initializer { ClientSecrets = clientSecrets, Scopes = scopes, DataStore = new EFDataStore(publishContext, description) };
            var googleAuthorizationCodeFlow = new GoogleAuthorizationCodeFlow(initializer);

            return googleAuthorizationCodeFlow;
        }

        // HttpContext context, string userId, 
        public async Task<Uri> GetAuthorizationUrl(HttpContext context, string id, string description, params string[] scopes)
        {
            // Now, let's grab the AuthorizationCodeFlow that will generate a unique authorization URL to redirect our user to
            var googleAuthorizationCodeFlow = this.GetGoogleAuthorizationCodeFlow(description, scopes);

            var codeRequestUrl = googleAuthorizationCodeFlow.CreateAuthorizationCodeRequest(apiOptions.RedirectUrl);
            codeRequestUrl.ResponseType = "code";

            // Build the url
            var authorizationUrl = codeRequestUrl.Build();
            context.Session.Set("user_id", Encoding.UTF8.GetBytes(id));
            context.Session.Set("scopes", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(scopes)));

            // Give it back to our caller for the redirect
            return authorizationUrl;
        }

        public async Task RevokeToken(string id, params string[] scopes)
        {
            // Now, let's grab the AuthorizationCodeFlow that will generate a unique authorization URL to redirect our user to
            var googleAuthorizationCodeFlow = this.GetGoogleAuthorizationCodeFlow(null, scopes);
            var account = await publishContext.YoutubeAccounts.FirstOrDefaultAsync(x => x.Key == id);
            if (account == null)
                throw new InvalidOperationException("Account not found for this id");
            TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(account.Value);
            try
            {
                await googleAuthorizationCodeFlow.RevokeTokenAsync(id, tokenResponse.AccessToken, CancellationToken.None);
            }
            finally
            {
                account.Value = null;
                publishContext.SaveChanges();
            }


        }


        public async Task<string> GetYoutubeAuthenticationToken(ControllerBase controller, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                /* 
                    This means the user canceled and did not grant us access. In this case, there will be a query parameter
                    on the request URL called 'error' that will have the error message. You can handle this case however.
                    Here, we'll just not do anything, but you should write code to handle this case however your application
                    needs to.
                */
            }

            // The userId is the ID of the user as it relates to YOUR application (NOT their Youtube Id).
            // This is the User ID that you assigned them whenever they signed up or however you uniquely identify people using your application     
            var userId = Encoding.UTF8.GetString(controller.HttpContext.Session.Get("user_id"));
            var scopes = JsonConvert.DeserializeObject<string[]>(Encoding.UTF8.GetString(controller.HttpContext.Session.Get("scopes")));

            // We need to build the same redirect url again. Google uses this for validaiton I think...? Not sure what it's used for
            // at this stage, I just know we need it :)


            // Now, let's ask Youtube for our OAuth token that will let us do awesome things for the user

            var googleAuthorizationCodeFlow = this.GetGoogleAuthorizationCodeFlow(null, scopes);
            var token = await googleAuthorizationCodeFlow.ExchangeCodeForTokenAsync(userId, code, apiOptions.RedirectUrl, CancellationToken.None);

            // Now, you need to store this token in rlation to your user. So, however you save your user data, just make sure you
            // save the token for your user. This is the token you'll use to build up the UserCredentials needed to act on behalf
            // of the user.
            var tokenJson = JsonConvert.SerializeObject(token);
            //var dataStore = new EFDataStore(publishContext);

            // Now that we've got access to the user's YouTube account, let's get back
            // to our application :)
            return userId;
        }

    }
}