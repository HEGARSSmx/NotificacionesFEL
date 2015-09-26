using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesFEL
{
    static class Log
    {
        static private String ArchivoLog = AppDomain.CurrentDomain.BaseDirectory + "LogServicio.log";
        public static void GrabaLog(String Mensaje)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ArchivoLog, true);
            String Linea = "Fecha: " + DateTime.Now + "; Mensaje: " + Mensaje;
            sw.WriteLine(Linea);
            sw.Flush();
            sw.Close();
            sw = null;
        }

        public static void BorraLog()
        {
            System.IO.File.Delete(ArchivoLog);
        }
    }
}