using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ComputerGraphics
{
    class LeerArchivo
    {
        string rutaArchivo = null;

        public LeerArchivo(string r)
        {
            rutaArchivo = r;
            Console.WriteLine(this.rutaArchivo);
        }

        public string getLine(int line)
        {
            Console.WriteLine(rutaArchivo);
            string[] lines = File.ReadAllLines(rutaArchivo);
            return lines[line - 1];
        }

        public string[] getLines()
        {
            return File.ReadAllLines(rutaArchivo);
        }

        public string getAllDocument()
        {
            return File.ReadAllText(rutaArchivo);
        }

    }
}
