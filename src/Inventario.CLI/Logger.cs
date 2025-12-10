using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gestor_Inventario.src.Inventario.CLI
{
    static class Logger
    {
        public static async Task GrabarLogAsync(string message)
        {
            string logFilePath = "movimientos.log"; // lo grabo en carpeta Bin
            string logEntry = $"{DateTime.Now}: {message}";
            bool append = true;

            // Uso "StreamWriter" para escribir archivo, si quiero leer uso StreamReader
            using (StreamWriter sw = new StreamWriter(logFilePath, append, System.Text.Encoding.UTF8))
            {
               await sw.WriteLineAsync(logEntry);
            }
        }

    }
}
