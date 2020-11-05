using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public static class AccountService
    {

        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private static readonly IRestClient client = new RestClient();

        
        public static Accounts AccountBalance(int id)
        {
            RestRequest request = new RestRequest(API_BASE_URL + $"account/{id}");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<Accounts> response = client.Get<Accounts>(request);
            //ADD ERROR EXCEPTION HANDLING


            return response.Data;
        }
    }
}
