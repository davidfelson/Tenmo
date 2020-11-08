using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public class TransferService : ErrorHandling
    {
        
        
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private static readonly IRestClient client = new RestClient();

        public static List<API_User> GetListUsers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + $"transfer");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
            //add exception handling

            return response.Data;       
        }

        public static API_User GetUserById(int id)
        {
            RestRequest request = new RestRequest(API_BASE_URL + $"transfer/" + "user" + id);
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<API_User> response = client.Get<API_User>(request);
            //add exception handling

            return response.Data;
        }

        public bool SendMoney(int receiverId, int senderId, decimal sendAmount)
        {
            Transfers transfers = new Transfers()
            {
                account_from = senderId,
                account_to = receiverId,
                Amount = sendAmount
            };
            RestRequest request = new RestRequest(API_BASE_URL + "transfer" + "/send");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            request.AddJsonBody(transfers);
            IRestResponse response = client.Post(request);

            CheckResponse(response);

            return true;
        }

        public bool RequestMoney(int receiverId, int senderId, decimal sendAmount)
        {
            Transfers transfers = new Transfers()
            {
                account_from = senderId,
                account_to = receiverId,
                Amount = sendAmount
            };
            RestRequest request = new RestRequest(API_BASE_URL + "transfer" + "/request");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            request.AddJsonBody(transfers);
            IRestResponse response = client.Post(request);

            CheckResponse(response);

            return true;
        }

        public List<Transfers> ViewTransfers(int id)
        {
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer/{id}");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<List<Transfers>> response = client.Get<List<Transfers>>(request);

            CheckResponse(response);

            return response.Data;

        }


        public Transfers GetTransfer(int id)
        {
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer/transfer{id}");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<Transfers> response = client.Get<Transfers>(request);

            CheckResponse(response);

            return response.Data;
        }



    }
}
