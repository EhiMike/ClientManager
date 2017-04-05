using ClientManager.domain;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.support
{
    class DBSqlLite
    {
        private static SQLiteConnection m_dbConnection = new  SQLiteConnection("Data Source=SQL\\ClientManager.sqlite;Version=3;");

        public static void aggiungiCliente(Utente utente)
        {
            try
            {
                m_dbConnection.Open();
                string sql = "INSERT INTO clienti (idcliente,nome,cognome,sesso,codiceFiscale,dataNascita,luogoNascita,email,telefono,indirizzo,provincia,stato,scadAbb,scadVisita,stato_cliente)" +
                    "VALUES('"+ utente.Identifier+"','" + utente.Nome + "','" + utente.Cognome + "','" + utente.Sesso + "','" + utente.CodiceFiscale + "','" +
                    utente.DataDiNascita.ToShortDateString() + "','" + utente.LuogoNascita + "','" + utente.Email + "','" + utente.Telefono + "','" +
                    utente.Indirizzo + "','" + utente.Provincia.Abbr + "','" + utente.Stato + "','" + utente.ScadenzaAbb.ToShortDateString() + "','" +
                     utente.ScadenzaVisitaMedica.ToShortDateString() + "','" + utente.Attivo + "')" ;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                
                    m_dbConnection.Close();
            }
        }

        public static SortedList<string, Utente> readClienti()
        {
            SortedList<string, Utente> listClienti = new SortedList<string, Utente>();
            try
            {
                m_dbConnection.Open();
                string sql = "SELECT idcliente,clienti.nome as nomeCl,cognome,sesso,codiceFiscale,"
                    +"dataNascita,luogoNascita,email,telefono,indirizzo,province.cod as provC,province.nome as provN,"
                    +"province.regione as provR,stato,scadAbb,scadVisita,stato_cliente"
                    +" FROM clienti inner join province on province.cod = provincia";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Utente user = new Utente();
                    user.Identifier = rdr["idcliente"].ToString();
                    user.Nome = rdr["nomeCl"].ToString();
                    user.Cognome = rdr["cognome"].ToString();
                    user.Sesso = rdr["sesso"].ToString();
                    user.CodiceFiscale = rdr["codiceFiscale"].ToString();
                    user.DataDiNascita = Convert.ToDateTime(rdr["dataNascita"].ToString());
                    user.LuogoNascita = rdr["luogoNascita"].ToString();
                    user.Email = rdr["email"].ToString();
                    user.Telefono = rdr["telefono"].ToString();
                    user.Indirizzo = rdr["indirizzo"].ToString();
                    user.Provincia = new Provincia(rdr["provC"].ToString(), rdr["provN"].ToString(), rdr["provR"].ToString());
                    user.Stato = rdr["stato"].ToString();
                    user.ScadenzaAbb = Convert.ToDateTime(rdr["scadAbb"].ToString());
                    user.ScadenzaVisitaMedica = Convert.ToDateTime(rdr["scadVisita"].ToString());
                    user.Attivo = Convert.ToBoolean(rdr["stato_cliente"].ToString());
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
                        sql = "SELECT presenze.dataingresso as dataIn,presenze.oraIn as oraIn,presenze.oraOut as oraOut,presenze.idpresenze as idPres" +
                                    " FROM presenze INNER JOIN clienti on clienti.idcliente = presenze.idutente" +
                                    " WHERE idcliente = " + id;
                        command = new SQLiteCommand(sql, m_dbConnection);
                        rdr = command.ExecuteReader();

                        while (rdr.Read())
                        {
                            Utente user = listClienti[id];
                            Presenza pres = new Presenza();
                            pres.Data = Convert.ToDateTime(rdr["dataIn"].ToString());
                            pres.OraIngresso = TimeSpan.Parse(rdr["oraIn"].ToString());
                            pres.OraUscita = TimeSpan.Parse(rdr["oraOut"].ToString());
                            pres.IdPresenza = Convert.ToInt32(rdr["idPres"].ToString());
                            user.ListPresenze.Add(pres.Data, pres);
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.Logger("class=DBSqlLite readClienti.readPresenze ->" + ex.Message);
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
            catch (Exception ex)
            {
                Helper.Logger("class=DBSqlLite readClienti ->" + ex.Message);
            }
            finally
            {
                m_dbConnection.Close();
            }
            return listClienti;
        }

        public static void modificaCliente(Utente utente)
        {
            try
            {
                m_dbConnection.Open();
                string sql = "UPDATE clienti SET nome = '" + utente.Nome + "', cognome ='" +utente.Cognome +
                    "',sesso ='" + utente.Sesso + "',codiceFiscale ='" + utente.CodiceFiscale + "' ,"
                   + "dataNascita ='" + utente.DataDiNascita.ToShortDateString() + "',luogoNascita ='" + utente.LuogoNascita +
                   "',email ='" + utente.Email +"',telefono ='" + utente.Telefono +
                   "',indirizzo ='" + utente.Indirizzo + "',provincia ='" + utente.Provincia.Abbr +"',stato ='" + utente.Stato +
                   "',scadAbb ='" + utente.ScadenzaAbb.ToShortDateString() +"',scadVisita ='" +  utente.ScadenzaVisitaMedica.ToShortDateString() +
                   "',stato_cliente ='" + utente.Attivo + "'"
                   + "WHERE idcliente =" + utente.Identifier;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader rdr = command.ExecuteReader();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                    m_dbConnection.Close();
            }
        }

        public static void aggiungiPresenza(Presenza pres, string idUtente)
        {
            try
            {
                m_dbConnection.Open();

                string sql = "INSERT INTO presenze (idutente,dataingresso,oraIn,oraOut)" +
                    "VALUES('" + idUtente + "','" + pres.Data + "','" + pres.OraIngresso + "','" + pres.OraUscita + "')";
                
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                sql = "SELECT MAX(idpresenze) as id FROM presenze";
                command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    pres.IdPresenza = Convert.ToInt32(rdr["id"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                m_dbConnection.Close();
            }
        }

        public static void modificaPresenza(Presenza pres)
        {
            try
            {
                m_dbConnection.Open();

                string sql = " UPDATE presenze SET dataingresso = '" + pres.Data.ToString("yyyy-MM-dd hh:mm:ss") +
                    "',oraIn = '" + pres.OraIngresso.ToString() + "',oraOut = '" + pres.OraUscita.ToString() + "'"
                    + " where idpresenze = " + pres.IdPresenza;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                    m_dbConnection.Close();
            }
        }



        public static void aggiungiVariazione(VariazioneEconomica var)
        {
            try
            {
                m_dbConnection.Open();

                string sql = "INSERT INTO variazioni (data,descr,importo,dare)" +
                    "VALUES('" + var.Data +"','"+ (var.isDare() ? var.DescrizioneDare : var.DescrizioneAvere) + "','" +
                   (var.isDare() ? var.ImportoDare : var.ImportoAvere) + "','"+ var.isDare().ToString() + "')";


                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                    m_dbConnection.Close();
            }
        }

        public static List<VariazioneEconomica> readVariazioni()
        {
            List<VariazioneEconomica> listVariazioni = new List<VariazioneEconomica>();
            try
            {

                m_dbConnection.Open();

                string sql = "SELECT data,descr,importo,dare FROM variazioni";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    VariazioneEconomica var = new VariazioneEconomica(Convert.ToDateTime(rdr["data"].ToString()), rdr["descr"].ToString(), Convert.ToDouble(rdr["importo"].ToString()), Convert.ToBoolean(rdr["dare"].ToString()));
                    listVariazioni.Add(var);
                }
            }
            catch (Exception ex)
            {
                MethodBase site = ex.TargetSite;
                string methodName = site == null ? null : site.Name;
                Helper.Logger("class=DBSqlLite readVariazioni -" + ex.Message);
            }
            finally
            {
                    m_dbConnection.Close();
            }
            return listVariazioni;
        }

        public static void readProvince()
        {
            try
            {
                Helper.province.Clear();

                m_dbConnection.Open();
                string sql = "SELECT cod,nome,regione FROM province";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    Provincia prov = new Provincia(rdr["cod"].ToString(), rdr["nome"].ToString(), rdr["regione"].ToString());
                    Helper.province.Add(prov.Abbr, prov);
                }
            }
            catch (Exception ex)
            {
                Helper.Logger("class=DBSqlLite readProvince - " + ex.Message);
            }
            finally
            {
                    m_dbConnection.Close();
            }
        }

        public static void init()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
             
            m_dbConnection.Open();
            // CREAZIONE TABELLE
            // creazione tabella clienti
            //string sql =

            //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            // sql = "insert into highscores (name, score) values ('Me', 9001)";

            // sql = "insert into highscores (name, score) values ('Me', 3000)";
            // command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            //sql = "insert into highscores (name, score) values ('Myself', 6000)";
            //command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            //sql = "insert into highscores (name, score) values ('And I', 9001)";
            //command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();
            //sql = "select * from highscores order by score desc";
            // command = new SQLiteCommand(sql, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            m_dbConnection.Close();
        }


    }

    
}
