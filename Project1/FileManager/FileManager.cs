using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Accounts;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

namespace FileManager
{
    public class FileManager
    {
        public const string fileName = "Accounts.json";
        public static string filePath { get; private set; }
        public const string schemaName = "Schema.json";
        public static string schemaPath { get; private set; }



        public static FileManager fileManager;
        //private static JsonEngine engine = new JsonEngine();
        private FileManager()
        {
            //check if file exists
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if(!CheckFile())throw new FileNotFoundException(fileName);
            schemaPath = Path.Combine(Directory.GetCurrentDirectory(), schemaName);
            if (!CheckFile()) throw new FileNotFoundException(schemaName);
        }
        public static FileManager Instance()
        {
            if(fileManager == null)fileManager = new FileManager();
            return fileManager;
        }

        public bool CheckFile()
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            return false;
        }

        public void ParseJson()
        {
            JsonEngine.JsonToAccount();
        }

        public void WriteJson()
        {
            JsonEngine.WriteJson();
        }
    }

    internal static class JsonEngine
    {
        internal static void JsonToAccount()
        {
            string jsonText = ReadJsonFile(FileManager.filePath);//json to convert
            Account.allAccounts = JsonConvert.DeserializeObject<List<Account>>(jsonText);
        }

        internal static string AccountToJsonString()
        {
            return JsonConvert.SerializeObject(Account.allAccounts);
        }

        /// <summary>
        /// Read file from repo
        /// </summary>
        /// <param name="filePath">path with the file to find</param>
        /// <returns>a string containing the file text</returns>
        private static string ReadJsonFile(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string temp = sr.ReadToEnd();
            sr.Close();//house keeping
            return temp;
        }

        internal static void WriteJson()
        {
            JSchema schema = JSchema.Parse(ReadJsonFile(FileManager.schemaPath));
            string accountsText = AccountToJsonString();
            JArray accounts = JArray.Parse(accountsText);
            if (accounts.IsValid(schema))
            {
                using (StreamWriter sw = File.CreateText(FileManager.filePath))
                {
                    sw.Write(accountsText);
                }
            }
            Console.WriteLine($"{Account.allAccounts.Count} account(s) written to: {FileManager.filePath}");

        }
    }


}
