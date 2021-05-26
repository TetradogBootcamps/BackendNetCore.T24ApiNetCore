using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore
{
    public static class Log
    {
        public static string FileName = "log.txt";
        public static string FilePath => System.IO.Path.Combine(Environment.CurrentDirectory, FileName);

        public static void WriteLines(params string[] lines)
        {
            System.IO.File.AppendAllLines(FilePath, lines.Select(l=>$"{DateTime.Now} ~ {l}"));

        }
        public static void Clear()
        {
            if (System.IO.File.Exists(FilePath))
                System.IO.File.Delete(FilePath);
        }
    }
}
