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
    /// <summary>
    /// Using singletone design pattern.
    /// Class used to manage file IO
    /// </summary>
    public class FileManager
    {
        public const string fileName = "Accounts.json";
        public static string filePath { get; private set; }
        public const string schemaName = "Schema.json";
        public static string schemaPath { get; private set; }

        //file manger that will be used in application
        private static FileManager fileManager;

        /// <summary>
        /// Create the file manager
        /// </summary>
        /// <param name="filePath">Path the Json file</param>
        /// <param name="schemaPath">Used if path is needed</param>
        /// <exception cref="FileNotFoundException">if the file does not exist</exception>
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

        /// <summary>
        /// Create a instance of file manager
        /// </summary>
        /// <param name="filePath">Path the Json file</param>
        /// <param name="schemaPath">Used if path is needed</param>
        /// <returns></returns>
        public static FileManager Instance(string filePath, string schemaPath)
        {
            if (fileManager == null) fileManager = new FileManager(filePath, schemaPath);
            return fileManager;
        }

        /// <summary>
        /// Check filePath exist
        /// </summary>
        /// <returns></returns>
        private static bool CheckFile()
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Create a Json file and create a instance of file manager
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static FileManager CreateFileAndInstance(string path)
        {
            filePath = Path.Combine(path, fileName);
            File.Create(filePath);
            if (!CheckFile()) throw new FileNotFoundException(fileName);
            fileManager = new FileManager();
            return (fileManager);
        }

        /// <summary>
        /// Parse the JsonFile
        /// </summary>
        public void ParseJson()
        {
            JsonEngine.JsonToAccount();
        }

        /// <summary>
        /// Write to the JsonFile
        /// </summary>
        public void WriteJson()
        {
            JsonEngine.WriteJson();
        }
    }

    internal static class JsonEngine
    {
        /// <summary>
        /// Create Account Objects from Json
        /// </summary>
        internal static void JsonToAccount()
        {
            string jsonText = ReadJsonFile(FileManager.filePath);//json to convert
            if (jsonText == String.Empty) Account.allAccounts = new List<Account>();
            else Account.allAccounts = JsonConvert.DeserializeObject<List<Account>>(jsonText);
        }

        /// <summary>
        /// Turn accounts to a string
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Write to the JsonFile
        /// </summary>
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

            //Comment this out for schema validation
            using (StreamWriter sw = File.CreateText(FileManager.filePath))
            {
                sw.Write(accountsText);
            }

            Console.WriteLine($"{Account.allAccounts.Count} account(s) written to: {FileManager.filePath}");
        }
    }


}
