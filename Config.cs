using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
namespace NotificacionesFEL
{
    class Config
    {
        private String _strUsuarioFEL; // Usuario FEL para el acceso al WerService
        private String _strPassFEL; // Contraseña para el acceso al WebService
        private int _intIntervalo; // Intervalo de Ejecucion
        private int _intTimbresMinimos; // Cantidad minima de timbres para empezar a notificar
        private String _strCorreoNotificacion; // Correo al que se le notificara 
        private String _strCorreoEnvios; // Correo del que se enviaran las notificacion
        private String _strPassCorreoEnvios; // Contraseña del correo que notificara
        private String _strSmtp; // Servidor SMTP para el envio de correos
        private int _intSmtpPort; // Puerto del servidor de correos
        private bool _boolSSL; // Indica si el servidor de correo utiliza SSL

        public String UsuarioFEL { get { return _strUsuarioFEL; } }
        public String PassFEL { get { return _strPassFEL; } }
        public int Intervalo { get { return _intIntervalo; } }
        public int TimbresMinimos { get { return _intTimbresMinimos; } }
        public String CorreoNotificacion { get { return _strCorreoNotificacion; } }
        public String CorreoEnvios { get { return _strCorreoEnvios; } }
        public String PassCorreoEnvios { get { return _strPassCorreoEnvios; } }
        public String Smtp { get { return _strSmtp; } }
        public int SmtpPort { get { return _intSmtpPort; } }
        public bool UsaSSL { get { return _boolSSL; } }

        public Config()
        {
            CargaConfiguracion();
        }

        private void CargaConfiguracion()
        {
            /* Carga la configuracion del archivo XML que se encuentra a un lado del Servicio*/
            if (!File.Exists("ConfiguracionServicio.xml"))
            {
                return;
            }
            XmlDocument xml = new XmlDocument();
            xml.Load("ConfiguracionServicio.xml");

            /*Datos de FEL*/
            XmlNode FEL = xml.DocumentElement.SelectSingleNode("/Config/FEL");
            _strUsuarioFEL = FEL.Attributes["usuario"].Value;
            _strPassFEL = FEL.Attributes["pass"].Value;
            FEL = null;

            /*Configuracion del Servicio*/
            XmlNode servicio = xml.DocumentElement.SelectSingleNode("/Config/Servicio");
            _intIntervalo = int.Parse(servicio.Attributes["intervalo"].Value);
            _intTimbresMinimos = int.Parse(servicio.Attributes["timbresMinimos"].Value);
            servicio = null;

            /*SMTP*/
            XmlNode smtp = xml.DocumentElement.SelectSingleNode("/Config/SMTP");
            _strCorreoEnvios = smtp.Attributes["correo"].Value;
            XmlTextReader reader = new XmlTextReader("ConfiguracionServicio.xml");
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "UsuarioFEL": _strUsuarioFEL = reader.Value; break;
                    case "ContraseñaFel": _strPassFEL = reader.Value; break;
                    case "Intervalo": _intIntervalo = int.Parse(reader.Value); break;
                    case "TimbresMinimos": _intTimbresMinimos = int.Parse(reader.Value); break;
                    case "CorreoNotificacion": _strCorreoNotificacion = reader.Value; break;
                    case "CorreoEnvios": _strCorreoEnvios = reader.Value; break;
                    case "PassCorreoEnvios": _strPassCorreoEnvios = reader.Value; break;
                    case "Smtp": _strSmtp = reader.Value; break;
                    case "SmptPort": _intSmtpPort = int.Parse(reader.Value); break;
                    case "SSL": _boolSSL = bool.Parse(reader.Value); break;
                }
            }

        }
    }
}
