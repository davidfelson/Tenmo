using TenmoClient.Data;

namespace TenmoClient
{
    public static class UserService                                                 //Use RestSharp
    {
        private static API_User user = new API_User();

        public static void SetLogin(API_User u)
        {
            user = u;
        }

        public static string GetUserName()
        {
            return user.Username;
        }

        public static int GetUserId()
        {
            return user.UserId;
        }

        public static bool IsLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(user.Token);
        }

        public static string GetToken()
        {                                                   //Going to use JWT authentication
            return user?.Token ?? string.Empty;
        }
    }
}
