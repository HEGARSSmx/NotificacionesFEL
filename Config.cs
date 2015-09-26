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
        private List<String[]> _lstDestinatarios = new List<string[]>(); // Correos al que se le notificara 
        private String _strCorreoEnvios; // Correo del que se enviaran las notificacion
        private String _strPassCorreoEnvios; // Contraseña del correo que notificara
        private String _strSmtp; // Servidor SMTP para el envio de correos
        private int _intSmtpPort; // Puerto del servidor de correos
        private bool _boolSSL; // Indica si el servidor de correo utiliza SSL

        public String UsuarioFEL { get { return _strUsuarioFEL; } }
        public String PassFEL { get { return _strPassFEL; } }
        public int Intervalo { get { return _intIntervalo; } }
        public int TimbresMinimos { get { return _intTimbresMinimos; } }
        public List<String[]> CorreosNotificacion { get { return _lstDestinatarios; } }
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
            String ArchivoConfiguracion = AppDomain.CurrentDomain.BaseDirectory + "ConfiguracionServicio.xml";
            if (!File.Exists(ArchivoConfiguracion))
            {
                Log.GrabaLog("No se encontro el Archivo de Configuracion: " + ArchivoConfiguracion);
                throw new Exception("No se encontro el archivo de configuracion: " + ArchivoConfiguracion );
            }
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(ArchivoConfiguracion);

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
                _strPassCorreoEnvios = smtp.Attributes["pass"].Value;
                _strSmtp = smtp.Attributes["host"].Value;
                _intSmtpPort = int.Parse(smtp.Attributes["port"].Value);
                _boolSSL = bool.Parse(smtp.Attributes["ssl"].Value);

                /*Destinatarios*/
                XmlNode destinos = xml.DocumentElement.SelectSingleNode("/Config/Destinatarios");
                foreach (XmlNode destino in destinos.ChildNodes)
                {
                    String[] strDestino = new String[2];
                    strDestino[0] = destino.Attributes["nombre"].Value;
                    strDestino[1] = destino.Attributes["email"].Value;
                    _lstDestinatarios.Add(strDestino);
                }
                xml = null;
            }
            catch (Exception ex)
            {
                
                Log.GrabaLog(ex.Message);
                throw new Exception("Error al cargar la configuracion: " + ex.Message);
            }
        }
    }
}
