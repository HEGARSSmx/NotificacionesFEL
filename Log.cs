using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesFEL
{
    static class Log
    {
           public static void GrabaLog(String Mensaje){
               System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "LogServicio.log",true);
               String Linea = "Fecha: " + DateTime.Now + "; Mensaje: " + Mensaje;
               sw.WriteLine(Linea);
               sw.Flush();
               sw.Close();
               sw = null;
           }
    }
}
