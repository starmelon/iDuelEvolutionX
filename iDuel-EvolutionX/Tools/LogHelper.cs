using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iDuel_EvolutionX.Tools
{
    class LogHelper
    {

        public static void Wrtie(string pro,string dec)
        {
            string LogPath = System.IO.Directory.GetCurrentDirectory() + "\\Data\\";
            StreamWriter log = new StreamWriter(LogPath + "log.txt", true);
            log.WriteLine("time：" + System.DateTime.Now.ToLongTimeString());
            log.WriteLine(pro + "：" + dec);
            log.Close(); 
        }
    }
}
