using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project1
{
    internal class FileManager
    {
        public const string fileName = "Accounts.json";
        public static string filePath { get; private set; }

        FileManager()
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        }
        public bool CheckFile()
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            return false;
        }
    }

    private class JsonEngine
    {
        private Account account;
        private Password password;
        internal static void JsonToAccount()
        {
            string JsonText = ReadJsonFile(FileManager.filePath);
            string json = FileEngine.ReadFileFromGIT("Database", "Weapon.json");//json to convert
            weapon = JsonConvert.DeserializeObject<MakeWeapon>(json);//convert json to lists

            json = FileEngine.ReadFileFromGIT("Database", "Armour.json");
            armour = JsonConvert.DeserializeObject<MakeArmour>(json);

            json = FileEngine.ReadFileFromGIT("Database", "Enemy.json");
            enemy = JsonConvert.DeserializeObject<MakeEnemy>(json);

            json = FileEngine.ReadFileFromGIT("Database", "OtherItem.json");
            item = JsonConvert.DeserializeObject<MakeItem>(json);
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
