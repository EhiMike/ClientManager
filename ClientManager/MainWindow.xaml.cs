using ClientManager.domain;
using ClientManager.support;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;

namespace ClientManager
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Utente utenteCorrente = null;
        private SortedList<string,Utente> listaUtenti;
        private List<VariazioneEconomica> listVariazioni;

        private int showClienti = 3; // 3 -> tutti,1 -> attivi,2 -> non attivi,

        public MainWindow()
        {
            bool start =  false;

            //string testAbi = Helper.readRegistryKey("SN");
            //string testDsk = Helper.readRegistryKey("DSK");

            //if (!testAbi.Equals("null") && Helper.diskSerial().Equals(testDsk)){
            //    start = true;
            //}else
            //{
            //    WindowAbi dialog = new WindowAbi();
            //    dialog.ShowDialog();

            //    if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            //    {
            //        start = true;
            //    }
            //}

            if(DateTime.Now < Helper.scadenza)
            {
                start = true;
                if((Helper.scadenza - DateTime.Now).TotalDays < 7)
                {
                    System.Windows.MessageBox.Show("Il programma scadrà tra meno di una settimana");
                }else if ((Helper.scadenza - DateTime.Now).TotalDays < 30)
                {
                    System.Windows.MessageBox.Show("Il programma scadrà tra meno di un mese");
                }
            }else
            {
                System.Windows.MessageBox.Show("Programma scaduto");
            }
            
            
            if (start)
            {
                InitializeComponent();

                //DBHelper.initDBConnection();
                listVariazioni = DBSqlLite.readVariazioni();
                datePickerVariazione.SelectedDate = DateTime.Now;
                loadVariazioni();

               
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void reloadUtenti()
        {
            cbClient.Items.Clear();
            foreach(Utente user in listaUtenti.Values)
            {
                if(showClienti == 3 || (showClienti == 1 && user.Attivo) || (showClienti == 2 && !user.Attivo))
                {
                    cbClient.Items.Add(user.Cognome + " " + user.Nome);
                } 

                
            }
            if(cbClient.Items.Count > 0)
            {
                cbClient.SelectedIndex = 0;
            }
            loadUtentiTabella();

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
                    checkAttivo.IsChecked = utenteCorrente.Attivo;
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
                        

                    }
                    else
                    {
                        btnIngresso.IsEnabled = true;
                        btnUscita.IsEnabled = false;
                    }
                    loadPresenzeTabella();
                }

            }
            catch (Exception ex)
            {
                Helper.Logger("class=MainWindow riempiDatiUtente - " + ex.Message);
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
                if (winUtente.resultOK)
                {
                    Helper.saveUsers(listaUtenti);
                    reloadUtenti();
                }
                
                
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
                Helper.Logger("class=MainWindow trovaUtente - " + ex.Message);
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
            DBSqlLite.readProvince();
            //listaUtenti = Helper.readUsers();
            listaUtenti = DBSqlLite.readClienti();
            //DBSqlLite.aggiungiCliente(listaUtenti["1"]);
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
            if (utenteCorrente != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Eliminando il cliente verranno perse tutte le sue informazioni e la cronologia.Eliminare?", "Attezione", MessageBoxButton.YesNo);
                if (MessageBoxResult.Yes == result)
                {
                    utenteCorrente.Attivo = false;
                    DBSqlLite.modificaCliente(utenteCorrente);
                    reloadUtenti();
                }
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
                DBSqlLite.aggiungiPresenza(pres, utenteCorrente.Identifier);
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
                DBSqlLite.modificaPresenza(pres);
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
                Helper.Logger("class=MainWindow loadPresenzeTabella - " + ex.Message);
            }
        }

        private void loadUtentiTabella()
        {
            try
            {
                dataGridRiepilogo.Items.Clear();

                foreach (Utente user in listaUtenti.Values)
                {
                    dataGridRiepilogo.Items.Add(user);
                }

            }
            catch (Exception ex)
            {
                Helper.Logger("class=MainWindow loadUtentiTabella - " + ex.Message);
            }
        }

        private void loadVariazioni()
        {
            try
            {
                dataGridVariazioni.Items.Clear();

                VariazioneEconomica varTotale = new VariazioneEconomica(DateTime.Now,"TOTALE DARE",0,true);
                foreach (VariazioneEconomica var in listVariazioni)
                {
                    dataGridVariazioni.Items.Add(var);
                    if (var.isDare())
                    {
                        varTotale.ImportoDare += var.ImportoDare;
                    }
                    else
                    {
                        varTotale.ImportoAvere += var.ImportoAvere;
                    }
                }
                varTotale.DescrizioneAvere = "TOTALE AVERE";
                dataGridVariazioni.Items.Add(varTotale);

            }
            catch (Exception ex)
            {
                Helper.Logger("class=MainWindow loadVariazioni - " + ex.Message);
            }
        }

        private bool checkVariazioneCompleta()
        {
            return !String.IsNullOrEmpty(txtVariazione.Text) && !String.IsNullOrEmpty(txtImportoVariazione.Text);
        }

        private void txtVariazione_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnInserisciVariazione.IsEnabled = checkVariazioneCompleta();
        }

        private void txtImportoVariazione_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnInserisciVariazione.IsEnabled = checkVariazioneCompleta();
        }

        private void txtImportoVariazione_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnInserisciVariazione_Click(object sender, RoutedEventArgs e)
        {
            double importo = Double.Parse(txtImportoVariazione.Text);
            VariazioneEconomica var = new VariazioneEconomica(datePickerVariazione.SelectedDate.Value,txtVariazione.Text, importo,Convert.ToBoolean(RbDare.IsChecked));

            DBSqlLite.aggiungiVariazione(var);
            listVariazioni = DBSqlLite.readVariazioni();
            loadVariazioni();
        }

        private void dataGridVariazioni_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VariazioneEconomica varToDelete = (VariazioneEconomica)dataGridVariazioni.SelectedItem;
            if(varToDelete != null)
            {
                System.Windows.MessageBox.Show("Eliminare riga: " + varToDelete.Data + "," + (varToDelete.isDare() ? varToDelete.DescrizioneDare + "," + varToDelete.ImportoDare : varToDelete.DescrizioneAvere + "," + varToDelete.ImportoAvere));
            }
        }

        private void checkAttivo_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if(utenteCorrente != null)
            {
                utenteCorrente.Attivo = checkAttivo.IsChecked.Value;
                DBSqlLite.modificaCliente(utenteCorrente);
                reloadUtenti();
            }
        }

        private void comboStato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void comboStato_DropDownClosed(object sender, EventArgs e)
        {
            showClienti = 0;
            if (((System.Windows.Controls.CheckBox)comboStato.Items[0]).IsChecked.Value)
            {
                showClienti += 1;
            }
            if (((System.Windows.Controls.CheckBox)comboStato.Items[1]).IsChecked.Value)
            {
                showClienti += 2;
            }
            reloadUtenti();
        }
    }

   
}
