using System;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManager.domain;

namespace ClientManager
{
    class DBHelper
    {
        private static MySqlConnection conn;
        private static MySqlCommand cmd;
        private static MySqlDataReader rdr;


        public static void initDBConnection()
        {

            string connStr ="server = localhost; user = root; database = clientmanager; port = 3306; password = root;";
            conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL…");
                conn.Open();
                // Perform database operations 
                //readClienti();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void closeConnection()
        {
            if (conn != null)
            {
                conn.Close();
                Console.WriteLine("Done.");
            }
        }

        public static SortedList<string, Utente> readClienti()
        {
            SortedList<string, Utente> listClienti = new SortedList<string, Utente>();
            try
            {
                

               if(conn == null)
                {
                    initDBConnection();
                }
                string queryString = "SELECT clienti.idcliente,clienti.nome,clienti.cognome,clienti.sesso,"
                    + "clienti.codiceFiscale,clienti.dataNascita,clienti.luogoNascita,clienti.email,"
                    + "clienti.telefono,clienti.indirizzo,province.abbr,province.nome,province.regione,"
                    + "clienti.stato,clienti.scadAbb,clienti.scadVisita,clienti.stato_cliente"
                    + " FROM clienti inner join province on province.abbr = clienti.provincia";
                 cmd = new MySqlCommand(queryString, conn);
                 rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Utente user = new Utente();
                    user.Identifier = rdr.GetString(0);
                    user.Nome = rdr.GetString(1);
                    user.Cognome = rdr.GetString(2);
                    user.Sesso = rdr.GetString(3);
                    user.CodiceFiscale = rdr.GetString(4);
                    user.DataDiNascita = rdr.GetDateTime(5);
                    user.LuogoNascita = rdr.GetString(6);
                    user.Email = rdr.GetString(7);
                    user.Telefono = rdr.GetString(8);
                    user.Indirizzo = rdr.GetString(9);
                    user.Provincia = new Provincia(rdr.GetString(10), rdr.GetString(11), rdr.GetString(12));
                    user.Stato = rdr.GetString(13);
                    user.ScadenzaAbb = rdr.GetDateTime(14);
                    user.ScadenzaVisitaMedica = rdr.GetDateTime(15);
                    user.Attivo = rdr.GetString(16).Equals("1");
                    listClienti.Add(user.Identifier, user);
                }

                if (rdr != null)
                {
                    rdr.Close();
                }

                foreach (string id in listClienti.Keys)
                {
                    try
                    {
                        queryString = "SELECT presenze.dataingresso,presenze.oraIn,presenze.oraOut,presenze.idpresenze" +
                                    " FROM presenze INNER JOIN clienti on clienti.idcliente = presenze.idutente" +
                                    " WHERE idcliente = " + id;
                        cmd = new MySqlCommand(queryString, conn);
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            Utente user = listClienti[id];
                            Presenza pres = new Presenza();
                            pres.Data = rdr.GetDateTime(0);
                            pres.OraIngresso = rdr.GetTimeSpan(1);
                            pres.OraUscita = rdr.GetTimeSpan(2);
                            pres.IdPresenza = rdr.GetInt32(3);
                            user.ListPresenze.Add(pres.Data, pres);
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.Logger("readClienti.readPresenze ->" + ex.Message);
                    }
                    finally
                    {
                        if (rdr != null)
                        {
                            rdr.Close();
                        }
                    }
                    
                }
            }
            catch(Exception ex)
            {
                Helper.Logger("readClienti ->"+ ex.Message);
            }
            finally
            {
                if(rdr != null)
                {
                    rdr.Close();
                }
            }
            return listClienti;
        }

        public static void aggiungiCliente(Utente utente)
        {
            try
            {
                if (conn == null)
                {
                    initDBConnection();
                }
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO clienti (nome,cognome,sesso,codiceFiscale,dataNascita,luogoNascita,email,telefono,indirizzo,provincia,stato,scadAbb,scadVisita,stato_cliente)" +
                    "VALUES(@Name,@surname,@sesso,@cf,@dn,@ln,@mail,@tel,@indirizzo,@prov,@stato,@abb,@visita,@attivo)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Name", utente.Nome);
                cmd.Parameters.AddWithValue("@surname", utente.Cognome);
                cmd.Parameters.AddWithValue("@sesso", utente.Sesso);
                cmd.Parameters.AddWithValue("@cf", utente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@dn", utente.DataDiNascita);
                cmd.Parameters.AddWithValue("@ln", utente.LuogoNascita);
                cmd.Parameters.AddWithValue("@mail", utente.Email);
                cmd.Parameters.AddWithValue("@tel", utente.Telefono);
                cmd.Parameters.AddWithValue("@indirizzo", utente.Indirizzo);
                cmd.Parameters.AddWithValue("@prov", utente.Provincia.Abbr);
                cmd.Parameters.AddWithValue("@stato", utente.Stato);
                cmd.Parameters.AddWithValue("@abb", utente.ScadenzaAbb);
                cmd.Parameters.AddWithValue("@visita", utente.ScadenzaVisitaMedica);
                cmd.Parameters.AddWithValue("@attivo", utente.Attivo ? "1" : "-1");

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }

        public static void modificaCliente(Utente utente)
        {
            try
            {
                if (conn == null)
                {
                    initDBConnection();
                }
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = " UPDATE clienti "
                    + "SET nome = @Name ,cognome = @surname,sesso= @sesso,codiceFiscale= @cf,dataNascita= @dn,"
                    + "luogoNascita = @ln,email = @mail,telefono = @tel,indirizzo = @indirizzo,provincia = @prov,"
                    + "stato = @stato,scadAbb = @abb,scadVisita = @visita,stato_cliente = @attivo "
                    + "where id =" + utente.Identifier;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Name", utente.Nome);
                cmd.Parameters.AddWithValue("@surname", utente.Cognome);
                cmd.Parameters.AddWithValue("@sesso", utente.Sesso);
                cmd.Parameters.AddWithValue("@cf", utente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@dn", utente.DataDiNascita);
                cmd.Parameters.AddWithValue("@ln", utente.LuogoNascita);
                cmd.Parameters.AddWithValue("@mail", utente.Email);
                cmd.Parameters.AddWithValue("@tel", utente.Telefono);
                cmd.Parameters.AddWithValue("@indirizzo", utente.Indirizzo);
                cmd.Parameters.AddWithValue("@prov", utente.Provincia.Abbr);
                cmd.Parameters.AddWithValue("@stato", utente.Stato);
                cmd.Parameters.AddWithValue("@abb", utente.ScadenzaAbb);
                cmd.Parameters.AddWithValue("@visita", utente.ScadenzaVisitaMedica);
                cmd.Parameters.AddWithValue("@attivo", utente.Attivo ? "1":"-1");

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }

        public static void aggiungiPresenza(Presenza pres,string idUtente)
        {
            try
            {
                if (conn == null)
                {
                    initDBConnection();
                }
                cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO presenze (idutente,dataingresso,oraIn,oraOut)" +
                    "VALUES(@idutente,@dataingresso,@oraIn,@oraOut); SELECT MAX(idpresenze) FROM presenze";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@idutente", idUtente);
                cmd.Parameters.AddWithValue("@dataingresso", pres.Data);
                cmd.Parameters.AddWithValue("@oraIn", pres.OraIngresso);
                cmd.Parameters.AddWithValue("@oraOut", pres.OraUscita);

                int value = (int)cmd.ExecuteScalar();
                pres.IdPresenza = value;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }

        public static void modificaPresenza(Presenza pres)
        {
            try
            {
                if (conn == null)
                {
                    initDBConnection();
                }
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = " UPDATE presenze "
                //    + "SET idutente = @idutente,dataingresso = '@dataingresso',"
                //    + "oraIn = '@oraIn',oraOut = '@oraOut' "
                //    + "where idpresenze = " + pres.IdPresenza;
                cmd.CommandText = "UPDATE presenze SET dataingresso = '" + pres.Data.ToString("yyyy-MM-dd hh:mm:ss") +
                    "',oraIn = '" + pres.OraIngresso.ToString() + "',oraOut = '"+ pres.OraUscita.ToString() + "'"
                    + " where idpresenze = " + pres.IdPresenza;
                //cmd.CommandText = "UPDATE presenze SET dataingresso = '2017-03-20 19:59:27',oraIn = '19:59:27',oraOut = '19:59:30' where idpresenze = 9";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@dataingresso", pres.Data.ToString("yyyy-MM-dd hh:mm:ss"));
                cmd.Parameters.AddWithValue("@oraIn", pres.OraIngresso.ToString());
                cmd.Parameters.AddWithValue("@oraOut", pres.OraUscita.ToString());

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }

        public static void readProvince()
        {
            try
            {
                Helper.province.Clear();

                if (conn == null)
                {
                    initDBConnection();
                }
                string queryString = "SELECT abbr,nome,regione FROM province";
                cmd = new MySqlCommand(queryString, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Provincia prov = new Provincia(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2));
                    Helper.province.Add(prov.Abbr,prov);
                }
            }
            catch (Exception ex)
            {
                Helper.Logger(ex.Message);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }



    }
}
