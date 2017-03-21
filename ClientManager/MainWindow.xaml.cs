using ClientManager.domain;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ClientManager
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Utente utenteCorrente = null;
        private SortedList<string,Utente> listaUtenti;

        public MainWindow()
        {
            InitializeComponent();
            DBHelper.initDBConnection();
            DataContext = new List<Presenza>
            {
                
            };

        }

        private void reloadUtenti()
        {
            cbClient.Items.Clear();
            foreach(Utente user in listaUtenti.Values)
            {
                cbClient.Items.Add(user.Cognome + " " + user.Nome);
            }
            if(cbClient.Items.Count > 0)
            {
                cbClient.SelectedIndex = 0;
            }
        }

        private void riempiDatiUtente()
        {
            try
            {
                if(utenteCorrente != null)
                {
                    lblNomeVal.Content = utenteCorrente.Nome;
                    lblCognomeVal.Content = utenteCorrente.Cognome;
                    lblCFVal.Content = utenteCorrente.CodiceFiscale;
                    lblDataNascitaVal.Content = utenteCorrente.DataDiNascita.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lblEmailVal.Content = utenteCorrente.Email;
                    lblIndirizzoVal.Content = utenteCorrente.Indirizzo;
                    lblLuogoNascitaVal.Content = utenteCorrente.LuogoNascita;
                    lblProvinciaVal.Content = utenteCorrente.Provincia.getTestoProvincia();
                    lblScadAbbVal.Content = utenteCorrente.ScadenzaAbb.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lblSessoVal.Content = utenteCorrente.Sesso;
                    lblStato.Content = utenteCorrente.Stato;
                    lblTelefonoVal.Content = utenteCorrente.Telefono;
                    lblVisitaVal.Content = utenteCorrente.ScadenzaVisitaMedica.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    btnCI.IsEnabled = File.Exists(utenteCorrente.getDocumentPath("CI"));
                    btnVisita.IsEnabled = File.Exists(utenteCorrente.getDocumentPath("VM"));
                    if(utenteCorrente.ListPresenze.Count > 0)
                    {
                        Presenza pres = utenteCorrente.ListPresenze[utenteCorrente.getLastKey()];
                        if(pres.OraUscita != null)
                        {
                            btnIngresso.IsEnabled = true;
                            btnUscita.IsEnabled = false;
                        }else
                        {
                            btnIngresso.IsEnabled = false;
                            btnUscita.IsEnabled = true;
                        }
                        loadPresenzeTabella();

                    }
                    else
                    {
                        btnIngresso.IsEnabled = true;
                        btnUscita.IsEnabled = false;
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Helper.Logger(ex.Message);
            }
        }

        private void btnNuovo_Click(object sender, RoutedEventArgs e)
        {
            WindowUtente winUtente = new WindowUtente();
            winUtente.setUsersList(listaUtenti);
            winUtente.ShowDialog();
            Helper.saveUsers(listaUtenti);
            reloadUtenti();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (utenteCorrente != null)
            {
                WindowUtente winUtente = new WindowUtente(utenteCorrente);
                winUtente.ShowDialog();
                Helper.saveUsers(listaUtenti);
                reloadUtenti();
            }
        }

        private void cbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbClient.SelectedItem != null)
            {
                trovaUtente(cbClient.SelectedItem.ToString().Replace(" ", "-"));
            }
        }

        private bool trovaUtente(String cognomeNome)
        {
            bool trovato = false;
            try
            {
                string[] splitNome = cognomeNome.Split('-');
                foreach(Utente user in listaUtenti.Values)
                {
                    if(splitNome.Length > 1 && user != null)
                    {
                        if(user.Cognome.ToLower().Trim().Equals(splitNome[0].ToLower().Trim()) && user.Nome.ToLower().Trim().Equals(splitNome[1].ToLower().Trim()))
                        {
                            trovato = true;
                            utenteCorrente = user;
                            riempiDatiUtente();
                            break;
                        }
                    }
                }
                if (!trovato && cognomeNome.Length > 3)
                {
                    foreach(string value in cbClient.Items)
                    {
                        if (value.Contains(cognomeNome))
                        {
                            cbClient.SelectedItem = value;
                            break;
                        }
                    }
                }
            }catch(Exception ex)
            {
                Helper.Logger(ex.Message);
            }
            return trovato;
        }

        private void tbClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            trovaUtente(tbClient.Text.Replace(" ", "-"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            DBHelper.readProvince();
            //listaUtenti = Helper.readUsers();
            listaUtenti = DBHelper.readClienti();

            reloadUtenti();
            if(cbClient.SelectedItem != null)
            {
                trovaUtente(cbClient.SelectedItem.ToString());
            }
        }

        private void btnVisita_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(utenteCorrente.getDocumentPath("VM"));
        }

        private void btnCI_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(utenteCorrente.getDocumentPath("CI"));
        }

        private void btnElimina_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Eliminando il cliente verranno perse tutte le sue informazioni e la cronologia.Eliminare?", "Attezione", MessageBoxButton.YesNo);
            if(MessageBoxResult.Yes == result)
            {
                utenteCorrente.Attivo = false;
                DBHelper.modificaCliente(utenteCorrente);
            }
        }

        private void btnIngresso_Click(object sender, RoutedEventArgs e)
        {
            if(utenteCorrente != null)
            {
                Presenza pres = new Presenza();
                pres.Data = DateTime.Now;
                pres.OraIngresso = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds);
                utenteCorrente.ListPresenze.Add(pres.Data,pres);
                btnIngresso.IsEnabled = false;
                btnUscita.IsEnabled = true;
                DBHelper.aggiungiPresenza(pres, utenteCorrente.Identifier);
                loadPresenzeTabella();
            }
        }

        private void btnUscita_Click(object sender, RoutedEventArgs e)
        {
            if (utenteCorrente != null)
            {
                Presenza pres = utenteCorrente.ListPresenze[utenteCorrente.getLastKey()];
                pres.OraUscita = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds);
                btnIngresso.IsEnabled = true;
                btnUscita.IsEnabled = false;
                DBHelper.modificaPresenza(pres);
                loadPresenzeTabella();
            }
        }

        private void loadPresenzeTabella()
        {
            try
            {
                dataGrid.Items.Clear();
              
                foreach (Presenza pres in utenteCorrente.ListPresenze.Values)
                {
                    dataGrid.Items.Add(pres);
                }
                
            }
            catch (Exception ex)
            {
            }
        }
    }
}
