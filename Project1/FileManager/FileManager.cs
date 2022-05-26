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



        private static FileManager fileManager;
        //private static JsonEngine engine = new JsonEngine();
        private FileManager(string filePath, string schemaPath)
        {
            //check if file exists
            FileManager.filePath = Path.Combine(filePath, fileName);
            if (!CheckFile())
            {
                fileManager = null;
                throw new FileNotFoundException(fileName);
            }

            //Uncomment for schema validation
            //FileManager.schemaPath = Path.Combine(schemaPath, schemaName);
            //if (!CheckFile()) throw new FileNotFoundException(schemaName);
        }

        private FileManager()
        {

        }

        public static FileManager Instance(string filePath, string schemaPath)
        {
            if (fileManager == null) fileManager = new FileManager(filePath, schemaPath);
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

        public static FileManager CreateFileAndInstance(string path)
        {
            filePath = Path.Combine(path, fileName);
            File.Create(filePath);
            if (!File.Exists(filePath)) throw new FileNotFoundException(fileName);
            fileManager = new FileManager();
            return (fileManager);
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
            if (jsonText == String.Empty) Account.allAccounts = new List<Account>();
            else Account.allAccounts = JsonConvert.DeserializeObject<List<Account>>(jsonText);
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
            //Comment for schema validation
            //JSchema schema = JSchema.Parse(ReadJsonFile(FileManager.schemaPath));
            string accountsText = AccountToJsonString();
            JArray accounts = JArray.Parse(accountsText);


            //Uncomment for schema validation
            //if (accounts.IsValid(schema))
            //{
            //    using (StreamWriter sw = File.CreateText(FileManager.filePath))
            //    {
            //        sw.Write(accountsText);
            //    }
            //}

            //Comment for schema validation
            using (StreamWriter sw = File.CreateText(FileManager.filePath))
            {
                sw.Write(accountsText);
            }


            Console.WriteLine($"{Account.allAccounts.Count} account(s) written to: {FileManager.filePath}");

        }
    }


}
