/*
 * Program:         PasswordManager.exe
 * Module:          PasswordManager.cs
 * Date:            <enter a date>
 * Author:          <enter your name>
 * Description:     Some free starting code for INFO-3138 project 1, the Password Manager
 *                  application. All it does so far is demonstrate how to obtain the system date 
 *                  and how to use the PasswordTester class provided.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Accounts;

namespace PasswordManager
{
    /// <summary>
    /// Class used to manage password
    /// </summary>
    public class PasswordManager
    {
        /// <summary>
        /// Check the strenght of a password
        /// </summary>
        /// <param name="password">password to check</param>
        /// <returns></returns>
        public Accounts.Password CheckStrength(string password)
        {
            try
            {
                PasswordTester pw = new PasswordTester(password);
                return new Accounts.Password(pw);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ERROR: Invalid password format");
            }
            return null;
        }
        
    } // end class
}
