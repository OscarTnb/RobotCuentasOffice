using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ScriptQuitarCuentas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> licenciasActuales = new List<string>();
        private void Form2_Load(object sender, EventArgs e)
        {
            while(ExistenLicencias())
            {
                var p = new Process();
                p = Process.Start("CerrarWord.exe");
                Console.WriteLine("Cerrando Word");
                p.WaitForExit();
                QuitaLicencias(licenciasActuales);
                p = Process.Start("AbrirWord.exe");
                Console.WriteLine("Abriendo Word");
                p.WaitForExit();
            }
            Thread.Sleep(1000);
            while (ExistenLicencias())
            {
                var p = new Process();
                p = Process.Start("CerrarWord.exe");
                Console.WriteLine("Cerrando Word");
                p.WaitForExit();
                QuitaLicencias(licenciasActuales);
                p = Process.Start("AbrirWord.exe");
                Console.WriteLine("Abriendo Word");
                p.WaitForExit();
            }
            this.Text = "No existen Licencias";
            Thread.Sleep(1000);
        }


        public bool ExistenLicencias()
        {
            Process p = new Process();
            p.StartInfo.RedirectStandardOutput = true;
            if (File.Exists("C:\\Program Files (x86)\\Microsoft Office\\Office15\\ospp.vbs"))
                p.StartInfo.Arguments = " \"C:\\Program Files (x86)\\Microsoft Office\\Office15\\ospp.vbs\" /dstatus";
            else
                p.StartInfo.Arguments = " \"C:\\Program Files\\Microsoft Office\\Office15\\ospp.vbs\" /dstatus";
            p.StartInfo.FileName = @"cscript.exe";
            p.StartInfo.UseShellExecute = false;
            try
            {
                p.Start();

                List<string> resultados = new List<string>();

                string busquedaLicencia = "Last 5 characters of installed product key:";
                System.IO.StreamReader resultadoScript = p.StandardOutput;

                StringBuilder Sb = new StringBuilder();

                string line;
                bool cerrar = false;
                System.IO.StreamReader file = resultadoScript;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("LICENSE STATUS:") == true && line.Contains("---NOTIFICATIONS---") == false)
                    {
                        cerrar = true;
                    }
                    if (line.Contains(busquedaLicencia) == true)
                    {
                        string R = line.Substring(44, 5);
                        resultados.Add(R);
                        Sb.AppendLine(string.Format("Licencia: {0} Encontrada.", R));
                    }
                }

                Licencias.Text = Sb.ToString();
                //MessageBox.Show(Sb.ToString());
                licenciasActuales = resultados;
                p.WaitForExit();
                p.Close();
                return cerrar;
                /*
                if (resultados.Count > 0)
                {
                   // QuitaLicencias(resultados);
                    licenciasActuales = resultados;
                    return true;
                }
                else
                {
                    //this.Text = "No existen Licencias";
                    return false;
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                Console.ReadLine();
            }
            return false;
        }

        public void QuitaLicencias(List<string> ArrayLicencias)
        {
            StringBuilder Sb = new StringBuilder();

            foreach (var N in ArrayLicencias)
            {
                    Process p = new Process();
                    p.StartInfo.RedirectStandardOutput = true;
                    if (File.Exists("C:\\Program Files (x86)\\Microsoft Office\\Office15\\ospp.vbs"))
                        p.StartInfo.Arguments = string.Format(" \"C:\\Program Files (x86)\\Microsoft Office\\Office15\\ospp.vbs\" /unpkey:{0}", N);
                    else
                        p.StartInfo.Arguments = string.Format(" \"C:\\Program Files\\Microsoft Office\\Office15\\ospp.vbs\" /unpkey:{0}", N);
                    p.StartInfo.FileName = @"C:\Windows\System32\cscript.exe";
                    p.StartInfo.UseShellExecute = false;
                    try
                    {
                        p.Start();

                        System.IO.StreamReader resultadoScript = p.StandardOutput;

                        Sb.AppendLine(string.Format("Licencia: {0} Eliminada.", N));

                        p.WaitForExit();
                        p.Close();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0}", ex.Message);
                        Console.ReadLine();
                    }
            }
            Licencias.Text = Sb.ToString();
            //MessageBox.Show(Sb.ToString());
            this.Text = "No existen Licencias";
            //this.Close();
        }
    }
}