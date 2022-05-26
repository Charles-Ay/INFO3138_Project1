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

        private FileManager.FileManager GetFiles()
        {
            //Comment for schema validation
            //Console.Write("Enter Schema Path: ");
            //string schemaPath = Console.ReadLine();
            Console.Write("Enter Accounts File Path: ");
            string filePath = Console.ReadLine();

            try
            {

                FileManager.FileManager manager = FileManager.FileManager.Instance(filePath, "");
                return manager;
            }
            catch(Exception ex)
            {
                try
                {
                    Console.WriteLine($"Path Is Missing File: {ex.Message}. File will be created");

                    FileManager.FileManager manager = FileManager.FileManager.CreateFileAndInstance(filePath);
                    manager.ParseJson();
                    return manager;
                }
                catch(Exception ex2)
                {
                    Console.WriteLine($"Error Creating File: {ex2.Message}. Program will terminate");
                    System.Environment.Exit(1);
                }

            }
            return null;

        }

        public void start()
        {
            FileManager.FileManager manager = GetFiles();
            manager.ParseJson();

            Console.WriteLine();
            Console.WriteLine("PASSWORD MANAGEMENT SYSTEM");
            Console.WriteLine();
            Console.WriteLine(header);
            main();
        }

        private void main() {
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

            while(Account.allAccounts.Count == 0)
            {
                Console.WriteLine(header);
                Console.WriteLine("|      There are currently 0 account entries");
                endChar(rightPos);
                Console.WriteLine("|      Enter A to add a new entry.");
                endChar(rightPos);
                Console.WriteLine(header);
                Console.WriteLine();


                Console.Write("Enter a command: ");
                string inp2 = Console.ReadLine();

                Console.WriteLine();

                if (inp2 == "A" || inp2 == "a")
                {
                    Console.WriteLine(header);
                    Console.WriteLine("|   Add a new entry");
                    endChar(rightPos);
                    Console.WriteLine(header);
                    AddEntry(rightPos);
                    Console.WriteLine(header);
                    goto start;

                }
            }

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

            while (inp != "x" || inp != "X")
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
            } 
            
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
            }
            Console.Write($"| Password Strength: {password.StrengthText} ({password.StrengthNum})% ");


            Console.WriteLine();

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
            catch (Exception)
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
            Console.WriteLine(header);
            Console.WriteLine();
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
            string input = Console.ReadLine();
            //Console.WriteLine();
            //Console.WriteLine();

            bool removed = false;
            //Console.WriteLine($"Input: {input}");
            while (true)
            {
                if (input.Equals("M") || input.Equals("m")) break;
                if(input == "P" || input == "p")
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
                else if (input == "D" || input == "d")
                {
                    Console.Write("| Are You sure? (Y/N):");
                    input = Console.ReadLine();
                    endChar(rightPos);
                    if (input == "Y"|| input == "y")
                    {
                        removed = true;
                    }
                    else
                    {
                        Console.Write("| Account not removed");
                        endChar(rightPos);
                    }
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Invalid input. Enter selection: ");
                    input = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            if (removed)
            {
                Account.allAccounts.RemoveAt(index);
                Console.WriteLine("| Account removed");
                endChar(rightPos);
            }
            else Account.allAccounts[index] = account;


        }
    }
}
