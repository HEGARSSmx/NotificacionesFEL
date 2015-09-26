using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesFEL
{
    public partial class Service1 : ServiceBase
    {
        Config config;
        System.Timers.Timer timer = null;

        public Service1()
        {
            InitializeComponent();
        }

        public void OnDebug() // para debuggear el servicio
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args) // Inicia el Servicio
        {
            try
            {
                Log.BorraLog(); // Borramos el archivo log para que este limpio para el nuevo inicio del Servicio
                config = new Config();
                timer = new System.Timers.Timer(config.Intervalo);
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = true;
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.GrabaLog(ex.Message);
                this.ExitCode = 1906;
                this.Stop();
                throw;
            }
        }

        protected override void OnStop()
        {
            Log.GrabaLog("Fin del Servivcio");
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            /*Proceso que se estara ejecutando cada intervalo del timer
             * Recupera el usuario y contraseña que se desa consultar
             * si la cantidad de timbres es menor a la especificada, enviara un correo de notificacion
             * Autor: Guadalupe Garza Moreno
             * Fecha: 23 Septimbre del 2015
             */
            int cantidadTimbres;
            cantidadTimbres = getTimbresRestantes(config.UsuarioFEL, config.PassFEL);
            if (cantidadTimbres <= config.TimbresMinimos)
            {
                SendNotification();
            }
        }

        private void SendNotification()
        {
            /* */

        }

        public int getTimbresRestantes(String UserName, String Password)
        {
            /*Obtenemos los timbres restantes dependiendo del usuario y contraseña que se pasen como paramentros
             * puede devolver un Error en caso de no poder conectarse al WebService De FEL
             * Autor: Guadalupe Garza Moreno
             * Fecha: 23 Septiembre del 2015
             */
            int cantidadTimbres = 0;
            try
            {
                ServiceReferenceFEL.WSTFDClient cliente = new ServiceReferenceFEL.WSTFDClient();
                ServiceReferenceFEL.RespuestaCreditos creditos = cliente.ConsultarCreditos(UserName, Password);
                if (creditos.OperacionExitosa) // recorremos la lista de paquetes y sumamos solo los paquetes vigentes
                {
                    foreach (ServiceReferenceFEL.DetallesPaqueteCreditos paquete in creditos.Paquetes)
                    {
                        if (paquete.Vigente)
                        {
                            cantidadTimbres += paquete.TimbresRestantes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.GrabaLog("Error en la conexion:" + ex.Message);
                throw ex;
            }
            return cantidadTimbres;
        }
    }
}