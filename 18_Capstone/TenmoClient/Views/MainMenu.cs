﻿using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    public class MainMenu : ConsoleMenu
    {
        private TransferService transferService = new TransferService();

        public MainMenu(TransferService transferService)
        { 
            AddOption("View your current balance", ViewBalance)
                .AddOption("View your past transfers", ViewTransfers)
                .AddOption("View your pending requests", ViewRequests)
                .AddOption("Send TE bucks", SendTEBucks)
                .AddOption("Request TE bucks", RequestTEBucks)
                .AddOption("Log in as different user", Logout)
                .AddOption("Exit", Exit);
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        { 
            int userId = UserService.GetUserId();
            
            Console.WriteLine($"Your balance is: {AccountService.AccountBalance(userId).Balance:c}");
            
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            int userId = UserService.GetUserId();
            foreach (ViewTransfers transfers in transferService.ViewTransfers(userId))
            {
                Console.WriteLine($"{transfers.transfer_id} \t {transfers.transfer_type_id}: {transfers.Username} \t {transfers.Amount:c}");
            }
            Console.WriteLine("Please enter transfer ID to view details  \"Enter\". ");
            int transferIDSelection = Convert.ToInt32(Console.ReadLine());
            Transfers trans = transferService.GetTransfer(transferIDSelection);
            Console.WriteLine($"Id:{trans.transfer_id}\nFrom: {trans.account_from}\nTo: {trans.account_to}\nType: {trans.transfer_type_id}\nStatus: {trans.transfer_status_id}\nAmount: {trans.Amount}");

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewRequests()
        {
            Console.WriteLine("Not yet implemented!");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            //List all users
            foreach (API_User user in TransferService.GetListUsers())
            {
                Console.WriteLine($"Username: {user.Username}, UserId: {user.UserId}");
            }
            Console.WriteLine("Please enter the UserId of the person you would like to send funds to and press \"Enter\". ");
            int userIDSelection = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter amount to transfer: ");
            decimal userAmountSelection = Convert.ToDecimal(Console.ReadLine());

            transferService.SendMoney(userIDSelection, UserService.GetUserId(), userAmountSelection);

            return MenuOptionResult.WaitAfterMenuSelection;

        }

        private MenuOptionResult RequestTEBucks()
        {
            foreach (API_User user in TransferService.GetListUsers())
            {
                Console.WriteLine($"Username: {user.Username}, UserId: {user.UserId}");
            }
            Console.WriteLine("Please enter the UserId of the person you would like to request fund from and press \"Enter\". ");
            int userIDSelection = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter amount to transfer: ");
            decimal userAmountSelection = Convert.ToDecimal(Console.ReadLine());

            transferService.RequestMoney(UserService.GetUserId(), userIDSelection, userAmountSelection);

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Logout()
        {
            UserService.SetLogin(new API_User()); //wipe out previous login info
            return MenuOptionResult.CloseMenuAfterSelection;
        }

    }
}
