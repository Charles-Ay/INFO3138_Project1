using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class GUI
    {
        public void test(){
            int len = 0;
            string header = "+------------------------+\n";
            //header = 14 / 2 = 7
            //text = 10 /2 = 5 print text at consle pos 2
            Console.WriteLine("PASSWORD MANAGEMENT SYSTEM");
            Console.WriteLine("");
            Console.Write(header);
            Console.WriteLine("             Account entries");
            Console.Write(header);
            Console.WriteLine("option1");
            Console.WriteLine("option2");
            Console.WriteLine("option3");
            Console.Write(header);
            Console.WriteLine(" Press a # from the above list to select an entry.");
            Console.WriteLine(" Press A to add a new entry.");
            Console.WriteLine(" Press X to quit.");
            Console.Write("Enter a command: ");
            string inp = Console.ReadLine();
            if (inp == "A" || inp == "a")
            {

                len++;
            }
            else if(inp=="x"|| inp == "X")
            {
                System.Environment.Exit(0);
            }
            else if(System.Int32.Parse(inp) < len && System.Int32.Parse(inp)
            {
                List<Account> accounts = new List<Account>();
                accounts[System.Int32.Parse(inp)].PrintData();
              
            }
            else
            {
                Console.WriteLine("This is not a valid input");
            }




        }   



    }
}
