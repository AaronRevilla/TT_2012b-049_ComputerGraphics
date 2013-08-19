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
        }

        public string getLine(int line)
        {
            string[] lines = File.ReadAllLines(rutaArchivo);
            if (line <= lines.Count() && line > 0)
            {
                return lines[line - 1];
            }
            else
            {
                return null;
            }
        }

        public string[] getLines()
        {
            return File.ReadAllLines(rutaArchivo);
        }

        public string getAllDocument()
        {
            return File.ReadAllText(rutaArchivo);
        }

        public int numLines()
        {
            return File.ReadAllLines(rutaArchivo).Count();
        }

    }
}
