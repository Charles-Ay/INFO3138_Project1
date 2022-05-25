using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager;
using Accounts;

namespace Project1
{
    internal class GUI
    {
        private const string header = "+--------------------------------------------------------------+";
        private void endChar(int rightPos)
        {
            int curLeft = Console.CursorLeft;
            Console.SetCursorPosition(rightPos, Console.CursorTop -1);
            Console.Write("|");
            Console.SetCursorPosition(curLeft, Console.CursorTop + 1);
        }

        public void start()
        {
            Console.WriteLine("PASSWORD MANAGEMENT SYSTEM");
            Console.WriteLine();
            Console.WriteLine(header);
            main();
        }

        private void main() {
            int len = 0;
            int leftPos = Console.CursorLeft;
            int rightPos = 0;
            int val = 0;

        start:

            for (int i = 0; i < header.Length; i++)
            {
                Console.Write(" ");
                if (i == header.Length / 2 - "Account entries".Length / 2)
                {
                    int curLeft = Console.CursorLeft;
                    Console.SetCursorPosition(leftPos, Console.CursorTop);
                    Console.Write("|");

                    Console.SetCursorPosition(curLeft, Console.CursorTop);

                    Console.Write("Account entries");
                }
                if (i == header.Length - 1 - "Account entries".Length - 1)
                {
                    rightPos = Console.CursorLeft;
                    Console.Write("|");
                    Console.WriteLine();
                    break;
                }
            }

            Console.WriteLine(header);

            for (int i = 0; i < Account.allAccounts.Count; i++)
            {
                Console.WriteLine($"|   {i + 1}. {Account.allAccounts[i].Description}");
                endChar(rightPos);
            }

            Console.WriteLine(header);

            Console.WriteLine("|      Press # from the above list to select an entry.");
            endChar(rightPos);
            Console.WriteLine("|      Press A to add a new entry.");
            endChar(rightPos);
            Console.WriteLine("|      Press X to quit.");
            endChar(rightPos);
            Console.WriteLine(header);
            Console.WriteLine();

            Console.Write("Enter a command: ");
            string inp = Console.ReadLine();

            Console.WriteLine();

            do
            {
                if (inp == "x" || inp == "X") break;
                if (inp == "A" || inp == "a")
                {
                    Console.WriteLine(header);
                    Console.WriteLine("|   Add a new entry");
                    endChar(rightPos);
                    Console.WriteLine(header);
                    AddEntry(rightPos);
                    Console.WriteLine(header);
                    goto start;

                }
                else if (int.TryParse(inp, out val) && (val > 0 && val <= Account.allAccounts.Count))
                {
                    Console.WriteLine(header);
                    Account.allAccounts[val-1].PrintData(endChar, rightPos);
                    editEntry(Account.allAccounts[val-1], rightPos, val-1);
                    Console.WriteLine(header);
                    goto start;
                }
                else
                {
                    Console.WriteLine(header);
                    Console.WriteLine("|  This is not a valid input");
                    endChar(rightPos);
                    Console.WriteLine(header);
                    Console.WriteLine();


                    Console.Write("Enter a command: ");
                    inp = Console.ReadLine();

                    Console.WriteLine();
                }
            } while (inp != "x" || inp != "X");
            
        }



        private void AddEntry(int rightPos)
        {
            PasswordManager.PasswordManager passwordManager = new PasswordManager.PasswordManager();

            Console.Write("| User ID:           ");
            string userId = Console.ReadLine();
            endChar(rightPos);

            while (userId == String.Empty)
            {
                Console.WriteLine("|");
                endChar(rightPos);
                Console.Write("| User ID can not be empty, enter user ID:          ");
                userId = Console.ReadLine();
                endChar(rightPos);
            }

            Console.Write("| Password:          ");
            string passwordText = Console.ReadLine();
            endChar(rightPos);

            while (passwordText == String.Empty)
            {
                Console.WriteLine("|");
                endChar(rightPos);
                Console.Write("| Password can not be empty, enter password:          ");
                passwordText = Console.ReadLine();
                endChar(rightPos);
            }

            Accounts.Password password = passwordManager.CheckStrength(passwordText);

            while (password.StrengthNum < 50)
            {
                Console.Write("| Password is too weak, try again:          ");
                passwordText = Console.ReadLine();
                endChar(rightPos);

                password = passwordManager.CheckStrength(passwordText);
                Console.Write($"| Password Strength: {password.StrengthText} ({password.StrengthNum})% ");
                endChar(rightPos);
            }

            Console.WriteLine("|");

            Console.Write("| Login Url:         ");
            string LoginUrl = Console.ReadLine();
            endChar(rightPos);

            Console.Write("| Account #:         ");
            int accountNum = 0;
            try
            {
                string tmp = Console.ReadLine();
                if (tmp != string.Empty)
                    accountNum = System.Int32.Parse(tmp);
            }
            catch (Exception e)
            {
                Console.WriteLine("|");
                endChar(rightPos);
                Console.Write("| Account # must be a number > 0, enter account #:          ");
                passwordText = Console.ReadLine();
                endChar(rightPos);
            }


            endChar(rightPos);

            Console.Write("| Description:       ");
            string description = Console.ReadLine();
            endChar(rightPos);

            while (description == String.Empty)
            {
                Console.WriteLine("|");
                endChar(rightPos);
                Console.Write("| Description can not be empty, enter description:          ");
                description = Console.ReadLine();
                endChar(rightPos);
            }

            Account.allAccounts.Add(new Account(description, userId, LoginUrl, accountNum, password));
        }

        private void editEntry(Account account, int rightPos, int index)
        {
            Console.WriteLine(header);
            Console.WriteLine("| Press P to change this Password.");
            endChar(rightPos);
            Console.WriteLine("| Press D to delete this entry.");
            endChar(rightPos);
            Console.WriteLine("| Press M to return to the main menu.");
            endChar(rightPos);
            Console.WriteLine(header);

            Console.WriteLine();
            Console.Write("Enter selection: ");
            ConsoleKey k = Console.ReadKey().Key;
            Console.WriteLine();
            Console.WriteLine();

            bool removed = false;

            while (k != ConsoleKey.M)
            {
                if(k == ConsoleKey.P)
                {
                    Console.WriteLine(header);
                    
                    PasswordManager.PasswordManager passwordManager = new PasswordManager.PasswordManager();
                    Console.Write("| Password:          ");
                    string passwordText = Console.ReadLine();
                    endChar(rightPos);

                    while(passwordText == String.Empty)
                    {
                        Console.WriteLine("|");
                        endChar(rightPos);
                        Console.Write("| Password can not be empty, enter password:          ");
                        passwordText = Console.ReadLine();
                        endChar(rightPos);
                    }
                    Accounts.Password password = passwordManager.CheckStrength(passwordText);

                    if(password.StrengthNum < 50)
                    {
                        do
                        {
                            Console.Write("| Password is too weak, try again:          ");
                            passwordText = Console.ReadLine();

                            endChar(rightPos);

                            password = passwordManager.CheckStrength(passwordText);
                            Console.Write($"| Password Strength: {password.StrengthText} ({password.StrengthNum})% ");
                            endChar(rightPos);
                        } while (password.StrengthNum < 50);
                    }



                    account.Password = new Accounts.Password(password.Value, password.StrengthNum, password.StrengthText, DateTime.Now.ToString());
                    Console.WriteLine();
                    Console.WriteLine(header);
                    Console.WriteLine();
                    break;
                }
                else if (k == ConsoleKey.D)
                {
                    removed = true;
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Invalid input. Enter selection: ");
                    k = Console.ReadKey().Key;
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            if (removed) Account.allAccounts.RemoveAt(index);
            else Account.allAccounts[index] = account;

        }
    }
}
