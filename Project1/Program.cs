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
            var GUI = new GUI();
            GUI.start();

            FileManager.FileManager manager = FileManager.FileManager.Instance("", "");
            manager.WriteJson();

        }
    }
}
