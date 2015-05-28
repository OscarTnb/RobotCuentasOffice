using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
namespace LogCuentasCSV
{
    public class Conexion 
    {
        private string connectionStringSQL = "Server=tcp:xhiuq62u2t.database.windows.net,1433;Database=CuentasOffice;User ID=CuentasOffice@xhiuq62u2t;Password=superWolke08;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
        private string connectionString = "Database=robottnb;Datasource=68.178.217.47;Uid=robottnb;pwd=superWolke08%";
        public string username, password;
        public string host = System.Environment.MachineName;
        public string table;
        public string mensajeError;
        public int cantRegistros, filaCSV;
        public bool[] faltantes = new bool[300000];

        public Conexion(string tabla)
        {
              this.table = tabla;
        }

        public void faltan()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("SELECT * FROM `veracruz200000` WHERE 1");
                    MySqlCommand cmdIns = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmdIns.ExecuteReader())
                    {
                        int filaCSVActual = 0;
                        while (reader.Read())
                        {
                            filaCSVActual++;
                            int filaCSV = int.Parse(reader[0].ToString());
                            while (filaCSV != filaCSVActual)
                            {
                                Console.WriteLine(filaCSVActual);
                                faltantes[filaCSVActual] = true;
                                filaCSVActual++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Conexion()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("select * from datos where Nombre = 'tablaActual'");
                    MySqlCommand cmdIns = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmdIns.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.table = reader[1].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void migrarBD()
        {
            string queryMySQL = "insert into veracruz values";
            try
            {
                string querySQL = string.Format("select * from veracruz order by filaCSV");
                using (SqlConnection connectionSQL = new SqlConnection(connectionStringSQL))
                {
                    connectionSQL.Open();
                    
                    SqlCommand cmdInsSQL = new SqlCommand(querySQL, connectionSQL);
                    using (SqlDataReader readerSQL = cmdInsSQL.ExecuteReader())
                    {
                        
                        int cont = 0;
                        while (readerSQL.Read())
                        {
                            cont++;
                            if (cont % 1000 == 0)
                            {
                                queryMySQL = queryMySQL.Substring(0, queryMySQL.Length - 2) + ";";
                                try
                                {
                                    using (MySqlConnection connectionMySQL = new MySqlConnection(connectionString))
                                    {
                                        connectionMySQL.Open();
                                        int retorno = new MySqlCommand(queryMySQL, connectionMySQL).ExecuteNonQuery();
                                        Console.WriteLine(retorno +": "+cont);
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    mensajeError = ex2.Message;
                                    Console.WriteLine(mensajeError);
                                    Console.WriteLine(queryMySQL.Substring(queryMySQL.Length - 10,10));
                                }
                                queryMySQL = "insert into veracruz values";
                            }
                            username = readerSQL[1].ToString().Replace("'", "''");
                            password = readerSQL[2].ToString().Replace("'", "''");
                            string time = DateTime.Parse(readerSQL[3].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            queryMySQL += string.Format("({0},'{1}','{2}','{3}','{4}','{5}'),\n"
                                    , readerSQL[0].ToString(), username, password
                                    , time, readerSQL[4].ToString(), readerSQL[5].ToString());
                        }
                        queryMySQL = queryMySQL.Substring(0, queryMySQL.Length - 2) + ";";
                        try
                        {
                            using (MySqlConnection connectionMySQL = new MySqlConnection(connectionString))
                            {
                                connectionMySQL.Open();
                                int retorno = new MySqlCommand(queryMySQL, connectionMySQL).ExecuteNonQuery();
                                Console.WriteLine(retorno + ": " + cont);
                            }
                        }
                        catch (Exception ex2)
                        {
                            mensajeError = ex2.Message;
                            Console.WriteLine(mensajeError);
                            Console.WriteLine(queryMySQL);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                Console.WriteLine(mensajeError);
                //Console.WriteLine(queryMySQL);
            }
        }

        public void tiempoDiferencia()
        {
            DateTime anterior = DateTime.Now;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("select * from veracruz where time is not null and status = 'Valida'  and host = 'LAB-A' order by time asc");
                    MySqlCommand cmdIns = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmdIns.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime actual = DateTime.Parse(reader[3].ToString());
                            TimeSpan ts = actual - anterior;
                            int MinDif = ts.Seconds + (ts.Minutes * 60);
                            Console.WriteLine(reader[0].ToString() + "\t" + reader[5].ToString() + "\t" + MinDif + "\t" + reader[3].ToString());
                            anterior = actual;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                Console.WriteLine(mensajeError);
            }
        }

        public int getCountRegistros()
        {
            int retorno = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("SELECT COUNT(*) FROM {0}", table);
                    MySqlCommand cmdIns = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmdIns.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cantRegistros = int.Parse(reader[0].ToString());
                            retorno = 0;
                        }
                        else
                        {
                            retorno = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                Console.WriteLine(mensajeError);
                retorno = -1;
            }
            return retorno;
        }

        public int nuevaCuenta(int filaCSV, string username, string password)
        {
            username = username.Replace("'", "''");
            password = password.Replace("'", "''");
            int retorno = -1;
            try{
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("INSERT into {0}(filaCSV, username, password, status) values('{1}','{2}','{3}','{4}')",
                        table, filaCSV, username, password, "Libre");
                    retorno = new MySqlCommand(query, connection).ExecuteNonQuery();
                }
            }catch(Exception ex){
                mensajeError = ex.Message;
                Console.WriteLine(mensajeError);
                Console.WriteLine(filaCSV+" "+username+" "+password);
                retorno = -1;
            }
            return retorno;
        }

        public int actualizarCuenta(string username, string password, string time, string status, string host)
        {
            //convert(datetime,getdate(),103)
            username = username.Replace("'", "''");
            password = password.Replace("'", "''");
            int retorno = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("UPDATE {0} set password='{1}', time='{2}', status='{3}', host='{4}' where username='{5}'",
                        table, password, time, status, host, username);
                    retorno = new MySqlCommand(query, connection).ExecuteNonQuery();
                }
            }catch(Exception ex){
                mensajeError = ex.Message;
                Console.WriteLine(mensajeError);
                retorno = -1;
            }
            return -1;
        }

        public int siguienteCuenta()
        {
            int retorno = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("UPDATE {0} as u, (SELECT filaCSV from {0} where (status='{2}' and host = '{3}') or status='{4}'  order by filaCSV  LIMIT 1) as t set u.time = {1}, u.status = '{2}', u.host = '{3}' where u.filaCSV = t.filaCSV",
                        table, "SYSDATE()", "Procesando", host, "Libre");
                    Console.WriteLine(query);
                    retorno = new MySqlCommand(query, connection).ExecuteNonQuery();
                    if (retorno == 1)
                    {
                        string query2 = string.Format("SELECT filaCSV, username, password from {0} where status ='{1}' and host = '{2}' order by filaCSV",
                        table, "Procesando", host);
                        MySqlCommand cmdIns = new MySqlCommand(query2, connection);
                        using (MySqlDataReader reader = cmdIns.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                filaCSV = int.Parse(reader[0].ToString());
                                username = reader[1].ToString();
                                password = reader[2].ToString();
                            }
                            else
                            {
                                retorno = -1;
                            }
                        }
                    }
                }
            }catch(Exception ex){
                mensajeError = ex.Message;
                Console.WriteLine(mensajeError);
                retorno = -1;
            }
            return retorno;
        }

        public int procesarCuenta(string status)
        {
            int retorno = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = string.Format("UPDATE {0} set time = {1}, status = '{2}' where filaCSV = {3}",
                        table, "SYSDATE()", status, filaCSV);
                    Console.WriteLine(query);
                    retorno = new MySqlCommand(query, connection).ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                Console.WriteLine(mensajeError);
                retorno = -1;
                esperar(2000);
            }
            return retorno;
        }

        public void esperar(int tiempo)
        {
            DateTime comienzo = DateTime.Now;
            DateTime final = DateTime.Now.AddMilliseconds(tiempo);
            while (comienzo.CompareTo(final) < 0)
            {
                comienzo = DateTime.Now;
            }
        }
    }
}
