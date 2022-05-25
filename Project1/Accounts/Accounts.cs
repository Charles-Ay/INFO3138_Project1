using PasswordManager;
using System;
using System.Collections.Generic;

namespace Accounts
{
    public class Account
    {
        public static List<Account> allAccounts = new List<Account>();//list of all accounts
        public string Description { get; set; }
        public string UserID { get; set; }
        public string LoginUrl { get; set; }
        public int AccountNum { get; set; }
        public Password Password { get; set; }
        public Account(string description, string userID, string loginUrl, int accountNum, Password password)
        {
            Description = description;
            UserID = userID;
            LoginUrl = loginUrl;
            AccountNum = accountNum;
            Password = new Password(password);
        }

        public void PrintData (Action<int> endChar, int rightPos)
        {

            Console.WriteLine($"| User ID:             {UserID}");
            endChar(rightPos);
            Console.WriteLine($"| Password:            {Password.Value}");
            endChar(rightPos);
            Console.WriteLine($"| Password Strength:   {Password.StrengthNum}");
            endChar(rightPos);
            Console.WriteLine($"| Password Last Reset: {Password.LastReset}");
            endChar(rightPos);
            Console.WriteLine($"| Login URL:           {LoginUrl}");
            endChar(rightPos);
            Console.WriteLine($"| Account Number:      {AccountNum}");
            endChar(rightPos);
        }

    }

    public class Password
    {
        public string Value { get; set; }
        public int StrengthNum { get; set; }
        public string StrengthText { get; set; }
        public string LastReset { get; set; }

        public Password()
        {

        }

        public Password(string value, int strengthNum, string strengthText, string lastReset = "")
        {
            Value = value;
            StrengthNum = strengthNum;
            StrengthText = strengthText;
            LastReset = lastReset;
        }

        public Password(Password password)
        {
            this.Value = password.Value;
            if(password.StrengthNum == 0)
            {
                PasswordManager.PasswordManager manager = new PasswordManager.PasswordManager();
                var res = manager.CheckStrength(password.Value);
                this.StrengthNum = res.StrengthNum;
                this.StrengthText = res.StrengthText;
            }
            else
            {
                this.StrengthNum = password.StrengthNum;
                this.StrengthText = password.StrengthText;
            }
            if(password.LastReset != null)this.LastReset = password.LastReset;
        }

        public Password(PasswordManager.PasswordTester tester)
        {
            this.Value = tester.Value;
            this.StrengthNum = tester.StrengthPercent;
            this.StrengthText = tester.StrengthLabel;
        }
    }
}
