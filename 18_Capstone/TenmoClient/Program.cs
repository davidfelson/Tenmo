using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoClient.Views;

namespace TenmoClient
{
    class Program
    {

        static void Main(string[] args)
        {
            TransferService transferService = new TransferService();
            AuthService authService = new AuthService();
            new LoginRegisterMenu(authService, transferService).Show();

            Console.WriteLine("\r\nThank you for using TEnmo!!!\r\n");
        }
    }
}
