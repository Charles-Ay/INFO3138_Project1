﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var GUI = new GUI();
            //GUI.test();

            FileManager manager = FileManager.Instance();
            manager.ParseJson();
        }
    }
}
