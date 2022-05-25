using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;

namespace Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManager.FileManager manager = FileManager.FileManager.Instance();
            manager.ParseJson();

            var GUI = new GUI();
            GUI.start();

            manager.WriteJson();

        }
    }
}
