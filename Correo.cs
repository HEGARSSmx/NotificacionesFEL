using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesFEL
{
    class Correo
    {
        private String _strEmisor; // Correo electronico que envia
        public String Emisor { get { return _strEmisor; } set { _strEmisor = value; } }

        private String _strPass; // Constrseña del Correo electronico
        public String Password { get { return _strPass; } set { _strPass = value; } }

        private int _intPort; // Puerto del Servidor SMTP
        public int Puerto { get { return _intPort; } set { _intPort = value; } }

        private String _strHost; // Servidor SMTP
        public String SmtpHost { get { return _strHost; } set { _strHost = value; } }

        private bool _boolSsl;
        public bool UsaSSL { get { return _boolSsl; } set { _boolSsl = value; } }

        public Correo()
        {
            _strEmisor = "";
            _strPass = "";
            _strHost = "";
            _boolSsl = false;
            _intPort = 25;
        }

        public Correo(String email, String pass, String smtp)
        {
            _strEmisor = email;
            _strPass = pass;
            _strHost = smtp;
            _boolSsl = false;
            _intPort = 25;
        }

        public Correo(String email, String pass, String smtp, int port)
        {
            _strHost = smtp;
            _strEmisor = email;
            _strPass = pass;
            _intPort = port;
            _boolSsl = false;
        }

        public Correo(String email, String pass, String smtp, int port, bool ssl)
        {
            _strHost = smtp;
            _strEmisor = email;
            _strPass = pass;
            _intPort = port;
            _boolSsl = ssl;
        }

        public void EnviaCorreo()
        {

        }
    }
}
