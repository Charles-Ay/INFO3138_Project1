using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;

namespace Project1
{
    internal class FileManager
    {
        public const string fileName = "Accounts.json";
        public static string filePath { get; private set; }
        public static FileManager fileManager;
        //private static JsonEngine engine = new JsonEngine();
        private FileManager()
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if(!CheckFile())throw new FileNotFoundException(fileName);
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
    }

    internal class JsonEngine
    {
        private static Account account;
        private static Password password;
        private static Root root;
        public static void JsonToAccount()
        {
            string jsonText = ReadJsonFile(FileManager.filePath);//json to convert
            _ = JsonConvert.DeserializeObject<Root>(jsonText);
        }

        /// <summary>
        /// Read file from repo
        /// </summary>
        /// <param name="filePath">path with the file to find</param>
        /// <returns>a string containing the file text</returns>
        internal static string ReadJsonFile(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string temp = sr.ReadToEnd();
            sr.Close();//house keeping
            return temp;
        }
    }


}
