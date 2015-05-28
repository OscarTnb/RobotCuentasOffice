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
using ElencySolutions.CsvHelper;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LogCuentasCSV
{
    public partial class Form1 : Form
    {
        List<List<string>> listaCuentas = new List<List<string>>();
        Conexion conn = new Conexion();

        public Form1()
        {
            
            InitializeComponent();
            /*
            //conn.tiempoDiferencia();
            //conn.migrarBD();
            //actualizarCuetas();
            int cantCuentas = conn.getCountRegistros();
            //conn.faltan();
            //CargaCuentas();
            if (cantCuentas != -1)
            {
                if (conn.cantRegistros > 0)
                {
                    ActivaControles();
                    lblNotificaciones.Text = string.Format("Se encontraron {0} registros", conn.cantRegistros);
                    siguienteCuenta();
                }
                else
                {
                    lblNotificaciones.Text = "No se encontraron cuentas";
                    this.Text = "No se encontraron cuentas";
                }
            }
            else
            {
                lblNotificaciones.Text = "No se pudo conectar con la BD";
                this.Text = "No se pudo conectar con la BD";
                DesctivaControles();
            }*/
            generarCuentas();
        }

        bool generarCuentas()
        {
            string rutaArchivo = "C:\\CuentasO365\\CuentasJunio.csv";
            var archivoExiste = System.IO.File.Exists(rutaArchivo);
            if (!archivoExiste)
            {
                return false;
            }
            else
            {
                try
                {
                    using (CsvReader reader = new CsvReader(rutaArchivo, Encoding.Default))
                    {
                        while (reader.ReadNextRecord())
                            listaCuentas.Add(reader.Fields);
                    }
                    var file = "C:\\CuentasO365\\LogBD.csv";
                    if (!File.Exists(file))
                    {
                        var stream = File.CreateText(file);
                        stream.Close();
                    }

                    int cant = listaCuentas.Count;
                    Console.WriteLine(cant);
                    bool[] logRandom = new bool[cant];
                    Random rand = new Random();
                    int posDom = 0;
                    string[] dominios = { "df.conalep.edu.mx", "qroo.conalep.edu.mx", "qro.conalep.edu.mx", "pue.conalep.edu.mx", "hgo.conalep.edu.mx", "ver.conalep.edu.mx", "nl.conalep.edu.mx", "jal.conalep.edu.mx", "ags.conalep.edu.mx", "bachilleres.edu.mx" ,"Error"};
                    int[] totales = {10053, 11330, 10310, 9063, 8118, 7023, 5983, 8946, 6602, 7572, 100000};
                    for (int i = 0; i < 85000; i++)
                    {
                        //Asignar Dominio
                        if (totales[posDom] <= 0) posDom++;
                        totales[posDom]--;

                        //Asignar Cuenta
                        int pos = rand.Next(cant);
                        while (logRandom[pos]) pos = rand.Next(cant);
                        logRandom[pos] = true;
                        
                        //Escribir cuenta
                        var cuenta = listaCuentas[pos];
                        string csvRow;
                        using (var stream = File.AppendText(file))
                        {
                            Console.Write(i + ": " + pos+": ");
                            Console.WriteLine(cuenta[3].IndexOf("@"));
                            string correo = cuenta[3].Substring(0,cuenta[3].LastIndexOf("@")).Replace(" ","")+"@"+dominios[posDom];
                            csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", pos, cuenta[0], cuenta[1], cuenta[2], correo, cuenta[4], cuenta[5], dominios[posDom]);
                            stream.WriteLine(csvRow);
                            stream.Close();
                        }
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(i+": "+totales[i]);
                    }
                        return true;
                }
                catch (Exception ex)
                {
                    lblNotificaciones.Text = string.Format("Hubo un error: {0}", ex.Message);
                    System.Windows.Forms.MessageBox.Show(string.Format("Hubo un error: {0}", ex.Message));
                    return false;
                }
            }
        }

        bool CargaCuentas()
        {
            string rutaArchivo = "C:\\CuentasO365\\nuevas.csv";
            var archivoExiste = System.IO.File.Exists(rutaArchivo);
            if (!archivoExiste)
            {
                return false;
            }
            else
            {
                try
                {
                    using (CsvReader reader = new CsvReader(rutaArchivo, Encoding.Default))
                    {
                        while (reader.ReadNextRecord())
                            listaCuentas.Add(reader.Fields);
                    }
                    var file = "C:\\CuentasO365\\LogBD.csv";
                    int filaCSV = 0;
                    int cantActual = conn.cantRegistros;
                    Console.WriteLine(cantActual);
                    foreach (var cuenta in listaCuentas)
                    {
                        filaCSV++;
                        if (conn.faltantes[filaCSV])
                        {
                            int status = conn.nuevaCuenta(filaCSV, cuenta[0], cuenta[1]);
                            if (status != 1)
                            {
                                if (!File.Exists(file))
                                {
                                    var stream = File.CreateText(file);
                                    stream.Close();
                                }
                                using (var stream = File.AppendText(file))
                                {
                                    string csvRow = string.Format("{0},{1},{2},{3}", filaCSV, cuenta[0], cuenta[1], status);
                                    stream.WriteLine(csvRow);
                                    stream.Close();
                                }
                            }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    lblNotificaciones.Text = string.Format("Hubo un error: {0}", ex.Message);
                    System.Windows.Forms.MessageBox.Show(string.Format("Hubo un error: {0}", ex.Message));
                    return false;
                }
            }
        }

        void actualizarCuetas()
        {
            string rutaArchivo = "C:\\CuentasO365\\validas.csv";
            try
            {
                using (CsvReader reader = new CsvReader(rutaArchivo, Encoding.Default))
                {
                    while (reader.ReadNextRecord())
                        listaCuentas.Add(reader.Fields);
                }
                var file = "C:\\CuentasO365\\LogBD.csv";
                int filaCSV = 0;
                foreach (var cuenta in listaCuentas)
                {
                    try { 
                        filaCSV++;
                        if(filaCSV%100 == 0)Console.WriteLine(filaCSV);
                        string hora = cuenta[0].ToString();
                        hora = hora.Replace("p. m", "PM");
                        hora = hora.Replace("a. m", "AM");
                        DateTime time = DateTime.Parse(hora);
                        string temp = time.ToString("yyyy-MM-dd HH:mm:ss");
                        int status = conn.actualizarCuenta(cuenta[1].ToString(), cuenta[2].ToString(), temp, "Valida", "LAB-C");
                        if (status != -1)
                        {
                            if (!File.Exists(file))
                            {
                                var stream = File.CreateText(file);
                                stream.Close();
                            }
                            using (var stream = File.AppendText(file))
                            {
                                string csvRow = string.Format("{0},{1},{2},{3}", filaCSV, cuenta[0], cuenta[1], status);
                                stream.WriteLine(csvRow);
                                stream.Close();
                            }
                        }
                    }
                    catch (Exception ex2)
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("Hubo un error: {0}", ex2.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                lblNotificaciones.Text = string.Format("Hubo un error: {0}", ex.Message);
                System.Windows.Forms.MessageBox.Show(string.Format("Hubo un error: {0}", ex.Message));
            }
        }

        private void siguienteCuenta()
        {
            this.Text = "Obteniendo otra cuenta";
            int intentos = 1;
            int resultado = -1;
            while ((resultado = conn.siguienteCuenta()) < 0)
            {
                this.Text = "Intentando nuevamente... " + intentos++;
                log(conn.mensajeError);
                Thread.Sleep(3000);
            }
            if (resultado > 0)
            {
                txtCuenta.Text = conn.username;
                txtPwd.Text = conn.password;
                lblRegistros.Text = string.Format("Registro {0} de {1}", conn.filaCSV, conn.cantRegistros);
                this.Text = "Log Cuentas CSV";
            }
            else
            {
                txtCuenta.Text = "";
                txtPwd.Text = "";
                lblRegistros.Text = "Terminamos...";
                lblNotificaciones.Text = string.Format("Se terminaron de procesar los {0} registros", conn.cantRegistros);
                this.Text = "Ya no hay mas registros";
                DesctivaControles();
            }
        }

        public void procesarCuenta(string status)
        {
            this.Text = "Procesando Cuenta";
            int intentos = 1;
            int resultado = -1;
            while ((resultado = conn.procesarCuenta(status)) < 0)
            {
                this.Text = "Intentando nuevamente... " + intentos++;
                log(conn.mensajeError);
                Thread.Sleep(3000);
            }
            this.Text = "Log Cuentas CSV";
        }

        /*private void GuardaRegistro(string user, string psw, int contadorInterno, int contadorCuentas, string estado)
        {   
            var file = "";
            if (estado.CompareTo("Valida") == 0)
            {
                file = "C:\\CuentasO365\\validas.csv";
            }else if (estado.CompareTo("Incorrecta") == 0){
                file = "C:\\CuentasO365\\incorrectas.csv";
            }else{
                file = "C:\\CuentasO365\\error.csv";
            }
            if (!File.Exists(file))
            {
                var stream = File.CreateText(file);
                stream.Close();
            }
            using (var stream = File.AppendText(file))
            {
                string csvRow = string.Format("{0},{1},{2},{3},{4}", DateTime.Now, user, psw, contadorInterno, contadorCuentas);
                stream.WriteLine(csvRow);
                stream.Close();
            }
            var fileNumCuenta = "C:\\CuentasO365\\ultimaCuenta.txt";
            using (var stream = File.CreateText(fileNumCuenta))
            {
                stream.WriteLine(contadorInterno);
                stream.Close();
            }
        }*/
        
        void ActivaControles()
        {
            btnCuentaIncorrecta.Enabled = true;
            btnCuentaValida.Enabled = true;
            btnError.Enabled = true;
            txtCuenta.Enabled = true;
            txtPwd.Enabled = true;
        }

        void DesctivaControles()
        {
            btnCuentaIncorrecta.Enabled = false;
            btnCuentaValida.Enabled = false;
            btnError.Enabled = false;
            txtCuenta.Text = string.Empty;
            txtPwd.Text = string.Empty;
            txtCuenta.Enabled = false;
            txtPwd.Enabled = false;
        }

        private void btnCopiarPwd_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(txtPwd.Text);
        }

        private void btnCopiarUser_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(txtCuenta.Text);
        }


        private void btnCuentaValida_Click(object sender, EventArgs e)
        {
            procesarCuenta("Valida");
            siguienteCuenta();
        }

        private void btnCuentaIncorrecta_Click(object sender, EventArgs e)
        {
            procesarCuenta("Incorrecta");
            siguienteCuenta();
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            procesarCuenta("Error");
            siguienteCuenta();
        }

        public void log(string cadena)
        {
            string file = "log.txt";
            if (!File.Exists(file))
            {
                var stream = File.CreateText(file);
                stream.Close();
            }
            using (var stream = File.AppendText(file))
            {
                stream.WriteLine(DateTime.Now+" -> "+cadena);
                stream.Close();
            }
        }

        private void lblNotificaciones_Click(object sender, EventArgs e)
        {

        }

        private void lblRegistros_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }
    }
}
