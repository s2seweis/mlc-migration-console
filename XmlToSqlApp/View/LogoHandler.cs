using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlToSqlApp.View
{
    internal class LogoHandler
    {
        public LogoHandler()
        {
            DisplayLogo();
        }

        public static void DisplayLogo()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   Welcome to the MLC Migration Tool");
            Console.WriteLine("===========================================");
            Console.WriteLine("       _   _  __     _     _                   ");
            Console.WriteLine("  __ _| | | |/ /___ | |__ | | ___ _ __  ____ ");
            Console.WriteLine(" / _` | | | ' // _ \\| '_ \\| |/ _ \\ '_ \\|_  / ");
            Console.WriteLine("| (_| | | | . \\ (_) | |_) | |  __/ | | |/ /  ");
            Console.WriteLine(" \\__,_|_| |_|\\_\\___/|_.__/|_|\\___|_| |_/___| ");
            Console.WriteLine("===========================================\n");
        }
    }
}
