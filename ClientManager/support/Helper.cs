﻿using ClientManager.domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ClientManager
{
    class Helper
    {
        public static SortedList<string,Provincia> province = new SortedList<string, Provincia>();
        private static List<String> stati = new List<String>();
        public static string pathBin = getBaseDirectory() + "\\bin\\";
        public static string pathData = getBaseDirectory() + "\\data\\";
        public static string pathAppData = getAppDataFolder() + "\\ClientManager\\";
        public static string pathUsers = pathData + "users\\";
        public static string formatDate = "dd/MM/yyyy";

        

        public static SortedList<string, Provincia> getProvince(bool forceRead = false)
        {
            if(province.Count == 0 || forceRead)
            {
                DBHelper.readProvince();
                //province.Clear();
                //try
                //{
                //    //Logger(pathBin);
                //    if (File.Exists(pathBin + "province.csv"))
                //    {
                //        string line;

                //        // Read the file and display it line by line.
                //        System.IO.StreamReader file = new System.IO.StreamReader(pathBin + "province.csv");
                //        while ((line = file.ReadLine()) != null)
                //        {
                //            string[] split = line.Split(',');
                //            if(split.Length > 2)
                //            {
                //                Provincia provincia = new Provincia(split[1], split[2], split[0]);
                //                province.Add(provincia.Abbr,provincia);
                //            }
                //        }

                //        file.Close();

                //    }
                //}
                //catch (Exception ex)
                //{
                //    Logger(ex.Message);
                //}
            }

            return province;
        }

        public static List<String> getStati(bool forceRead = false)
        {
            if (stati.Count == 0 || forceRead)
            {
                stati.Clear();
                try
                {
                    if (File.Exists(pathBin + "stati.csv"))
                    {
                        string line;

                        // Read the file and display it line by line.
                        System.IO.StreamReader file = new System.IO.StreamReader(pathBin + "stati.csv");
                        while ((line = file.ReadLine()) != null)
                        {
                           stati.Add(line);
                        }

                        file.Close();

                    }
                }
                catch (Exception ex)
                {
                    Logger(ex.Message);
                }
            }

            return stati;
        }

        private static string getAppDataFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) ;
        }


        public static void Logger(String lines)
        {
            string logPath = getBaseDirectory() + "//log//";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            // Write the string to a file.append mode is enabled so that the log
            // lines get appended to  test.txt than wiping content and writing the log

            System.IO.StreamWriter file = new System.IO.StreamWriter(logPath + "//log.txt", true);
            file.WriteLine(lines);

            file.Close();
            MessageBox.Show(lines);

        }

        public static String getBaseDirectory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Directory.GetParent(path).FullName;
#if DEBUG
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(path).FullName;
#endif

            return path;
        }

        public static void saveUsers(SortedList<string,Utente> users)
        {
            try
            {
                if (!Directory.Exists(pathData))
                {
                    Directory.CreateDirectory(pathData);
                }
                string fileUsers = pathData + "users.xml";

                XDocument doc = new XDocument();
                XElement rootNode = new XElement("USERS");
                rootNode.Add(new XAttribute("lastUpdate", DateTime.Now.ToString()));

                foreach(Utente user in users.Values)
                {
                    if(user != null)
                    {
                        XElement userNode = new XElement("USER");
                        userNode.Add(new XAttribute("identifier", user.Identifier));
                        userNode.Add(new XAttribute("nome",user.Nome != null? user.Nome:""));
                        userNode.Add(new XAttribute("cognome", user.Cognome != null ? user.Cognome : ""));
                        userNode.Add(new XAttribute("sesso", user.Sesso != null ? user.Sesso : ""));
                        userNode.Add(new XAttribute("codicefiscale", user.CodiceFiscale != null ? user.CodiceFiscale : ""));
                        userNode.Add(new XAttribute("datanascita", user.DataDiNascita != null ? getStringFromDateTime(user.DataDiNascita) : ""));
                        userNode.Add(new XAttribute("luogonascita", user.LuogoNascita != null ? user.LuogoNascita : ""));
                        userNode.Add(new XAttribute("email", user.Email != null ? user.Email : ""));
                        userNode.Add(new XAttribute("telefono", user.Telefono != null ? user.Telefono : ""));
                        userNode.Add(new XAttribute("indirizzo", user.Indirizzo != null ? user.Indirizzo : ""));
                        userNode.Add(new XAttribute("provincia", user.Provincia != null ? user.Provincia.Abbr : null));
                        userNode.Add(new XAttribute("stato", user.Stato != null ? user.Stato : ""));
                        userNode.Add(new XAttribute("scadenzaabb", user.ScadenzaAbb != null ? getStringFromDateTime(user.ScadenzaAbb) : ""));
                        userNode.Add(new XAttribute("scadenzamed", user.ScadenzaVisitaMedica != null ? getStringFromDateTime(user.ScadenzaVisitaMedica) : ""));
                        rootNode.Add(userNode);
                    }
                }
                doc.Add(rootNode);

                doc.Save(fileUsers);

            }
            catch(Exception ex)
            {
                Logger(ex.Message);
            }
        }

        public static SortedList<string, Utente> readUsers()
        {
            SortedList<string, Utente> listUsers = new SortedList<string, Utente>();
            try
            {
                string fileUsers = pathData + "users.xml";
                if (File.Exists(fileUsers))
                {
                    XDocument doc = XDocument.Load(fileUsers);
                    XElement rootNode = doc.Element("USERS");
                    foreach(XElement node in rootNode.Elements("USER"))
                    {
                        try
                        {


                            object value = node.Attribute("identifier").Value;
                            if (value != null)
                            {
                                Utente user = new Utente(value.ToString());
                                value = node.Attribute("nome").Value;
                                if (value != null)
                                {
                                    user.Nome = value.ToString();
                                }
                                value = node.Attribute("cognome").Value;
                                if (value != null)
                                {
                                    user.Cognome = value.ToString();
                                }
                                value = node.Attribute("sesso").Value;
                                if (value != null)
                                {
                                    user.Sesso = value.ToString();
                                }
                                value = node.Attribute("codicefiscale").Value;
                                if (value != null)
                                {
                                    user.CodiceFiscale = value.ToString();
                                }
                                value = node.Attribute("datanascita").Value;
                                if (value != null)
                                {
                                    user.DataDiNascita = getDateTimeFromString(value.ToString());
                                }
                                value = node.Attribute("luogonascita").Value;
                                if (value != null)
                                {
                                    user.LuogoNascita = value.ToString();
                                }
                                value = node.Attribute("email").Value;
                                if (value != null)
                                {
                                    user.Email = value.ToString();
                                }
                                value = node.Attribute("telefono").Value;
                                if (value != null)
                                {
                                    user.Telefono = value.ToString();
                                }
                                value = node.Attribute("indirizzo").Value;
                                if (value != null)
                                {
                                    user.Indirizzo = value.ToString();
                                }
                                value = node.Attribute("provincia").Value;
                                if (value != null && !String.IsNullOrEmpty(value.ToString())) 
                                {
                                    user.Provincia = province[value.ToString()];
                                }
                                value = node.Attribute("stato").Value;
                                if (value != null)
                                {
                                    user.Stato = value.ToString();
                                }
                                value = node.Attribute("scadenzaabb").Value;
                                if (value != null)
                                {
                                    user.ScadenzaAbb = getDateTimeFromString(value.ToString());
                                }
                                value = node.Attribute("scadenzamed").Value;
                                if (value != null)
                                {
                                    user.ScadenzaVisitaMedica = getDateTimeFromString(value.ToString());
                                }
                                listUsers.Add(user.Identifier, user);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger("Errore lettura Utente");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger(ex.Message);
            }
            return listUsers;
        }

        public static string getStringFromDateTime(DateTime dt)
        {
            return (dt.Day > 9 ?"":"0") + dt.Day + "/" + (dt.Month > 9 ? "" : "0")+  dt.Month + "/" + dt.Year;
        }

        public static DateTime getDateTimeFromString(string dateString)
        {
            return DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

    }
}