using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace MonitorMaquinasVirtuales
{
    public partial class Form1 : Form
    {
        DateTime inicio = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer2.Interval = 7200000; // Cada 2 Horas
            //timer2.Interval = 60000; //Cada Minuto
            timer2.Enabled = true;
            label1.Text = "Cargando...";
            button1.Text = "Reiniciar Ahora";
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Falta "+((Convert.ToDateTime("2:00:00")) - (DateTime.Now - inicio)).ToString(@"hh:mm:ss") +" para reiniciar checkpoint";
            //label1.Text = "Falta " + ((Convert.ToDateTime("0:01:00")) - (DateTime.Now - inicio)).ToString(@"hh:mm:ss") + " para reiniciar checkpoint";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            label1.Text = "Reiniciando Robots en Hyper-V...";
            timer2.Enabled = false;

            MessageBoxTemporal.Show(reiniciarRobots(), "Estado", 10, true);

            inicio = DateTime.Now;
            timer2.Enabled = true;
            timer2.Start();
            timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            label1.Text = "Reiniciando Robots en Hyper-V...";

            MessageBoxTemporal.Show(reiniciarRobots(), "Estado", 10, true);

            inicio = DateTime.Now;
            timer1.Start();
        }

        public string reiniciarRobots()
        {
            StringBuilder stringBuilder = new StringBuilder();
            PowerShell ps;
            ps = PowerShell.Create();
            ps.AddScript("Set-ExecutionPolicy Unrestricted");
            Collection<PSObject> results = ps.Invoke();
            ps.AddScript(".\\reiniciarRobots.ps1");
            ps.Commands.AddCommand("Out-String");
            results = ps.Invoke();
            foreach (PSObject obj in results)
            {
                stringBuilder.Append(obj.ToString());
            }
            foreach (ErrorRecord error in ps.Streams.Error)
            {
                stringBuilder.Append(error.ToString());
            }
            return (stringBuilder.ToString());
        }
    }


    public class MessageBoxTemporal
    {
        System.Threading.Timer IntervaloTiempo;
        string TituloMessageBox;
        string TextoMessageBox;
        int TiempoMaximo;
        IntPtr hndLabel = IntPtr.Zero;
        bool MostrarContador;

        MessageBoxTemporal(string texto, string titulo, int tiempo, bool contador)
        {
            TituloMessageBox = titulo;
            TiempoMaximo = tiempo;
            TextoMessageBox = texto;
            MostrarContador = contador;

            if (TiempoMaximo > 99) return; //Máximo 99 segundos
            IntervaloTiempo = new System.Threading.Timer(EjecutaCada1Segundo,
                null, 1000, 1000);
            if (contador)
            {
                DialogResult ResultadoMensaje = MessageBox.Show(texto + "\r\nEste mensaje se cerrará dentro de " +
                    TiempoMaximo.ToString("00") + " segundos ...", titulo);
                if (ResultadoMensaje == DialogResult.OK) IntervaloTiempo.Dispose();
            }
            else
            {
                DialogResult ResultadoMensaje = MessageBox.Show(texto + "...", titulo);
                if (ResultadoMensaje == DialogResult.OK) IntervaloTiempo.Dispose();
            }
        }
        public static void Show(string texto, string titulo, int tiempo, bool contador)
        {
            new MessageBoxTemporal(texto, titulo, tiempo, contador);
        }
        void EjecutaCada1Segundo(object state)
        {
            TiempoMaximo--;
            if (TiempoMaximo <= 0)
            {
                IntPtr hndMBox = FindWindow(null, TituloMessageBox);
                if (hndMBox != IntPtr.Zero)
                {
                    SendMessage(hndMBox, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    IntervaloTiempo.Dispose();
                }
            }
            else if (MostrarContador)
            {
                // Ha pasado un intervalo de 1 seg:
                if (hndLabel != IntPtr.Zero)
                {
                    SetWindowText(hndLabel, TextoMessageBox +
                        "\r\nEste mensaje se cerrará dentro de " +
                        TiempoMaximo.ToString("00") + " segundos");
                }
                else
                {
                    IntPtr hndMBox = FindWindow(null, TituloMessageBox);
                    if (hndMBox != IntPtr.Zero)
                    {
                        // Ha encontrado el MessageBox, busca ahora el texto
                        hndLabel = FindWindowEx(hndMBox, IntPtr.Zero, "Static", null);
                        if (hndLabel != IntPtr.Zero)
                        {
                            // Ha encontrado el texto porque el MessageBox
                            // solo tiene un control "Static".
                            SetWindowText(hndLabel, TextoMessageBox +
                                "\r\nEste mensaje se cerrará dentro de " +
                                TiempoMaximo.ToString("00") + " segundos");
                        }
                    }
                }
            }
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll",
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string lpszClass, string lpszWindow);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern bool SetWindowText(IntPtr hwnd, string lpString);
    }

}
