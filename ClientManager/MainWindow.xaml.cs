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
                    loadStoricoTabella();
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
            foreach(Utente user in listaUtenti.Values)
            {
                user.ListStorico = DBSqlLite.readStorico(user.Identifier);
            }
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
                gridPresenze.Items.Clear();
              
                foreach (Presenza pres in utenteCorrente.ListPresenze.Values)
                {
                    gridPresenze.Items.Add(pres);
                }
                
            }
            catch (Exception ex)
            {
                Helper.Logger("class=MainWindow loadPresenzeTabella - " + ex.Message);
            }


        }

        private void loadStoricoTabella()
        {
            try
            {
                gridStoricoUtente.Items.Clear();

                foreach (Storico pres in utenteCorrente.ListStorico.Values)
                {
                    gridStoricoUtente.Items.Add(pres);
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
            VariazioneEconomica var = new VariazioneEconomica(datePickerVariazione.SelectedDate.Value, txtVariazione.Text, importo, Convert.ToBoolean(RbDare.IsChecked));

            DBSqlLite.aggiungiVariazione(var);
            listVariazioni = DBSqlLite.readVariazioni();
            loadVariazioni();
        }

        private void dataGridVariazioni_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VariazioneEconomica varSelected = (VariazioneEconomica)dataGridVariazioni.SelectedItem;
            if(varSelected != null)
            {
                btnEliminaVar.IsEnabled = true;
                btnModificaVar.IsEnabled = true;
                
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

        private void btnModificaPresenza_Click(object sender, RoutedEventArgs e)
        {
            if(utenteCorrente != null && gridPresenze.SelectedItem != null)
            {
                Presenza pres = (Presenza)gridPresenze.SelectedItem;
                WindowEdit windowEdit = new WindowEdit(pres);
                windowEdit.ShowDialog();
                loadPresenzeTabella();
            }
        }

        private void btnModificaVar_Click(object sender, RoutedEventArgs e)
        {
            if (utenteCorrente != null && dataGridVariazioni.SelectedItem != null)
            {
                VariazioneEconomica var = (VariazioneEconomica)dataGridVariazioni.SelectedItem;
                WindowEdit windowEdit = new WindowEdit(var);
                windowEdit.ShowDialog();
                listVariazioni = DBSqlLite.readVariazioni();
                loadVariazioni();
            }
                
        }

        private void btnEliminaVar_Click(object sender, RoutedEventArgs e)
        {
            VariazioneEconomica varSelected = (VariazioneEconomica)dataGridVariazioni.SelectedItem;
            if (varSelected != null) { 
                MessageBoxResult res = System.Windows.MessageBox.Show("Eliminare riga: " + varSelected.Data + "," + (varSelected.isDare() ? varSelected.DescrizioneDare + "," + varSelected.ImportoDare : varSelected.DescrizioneAvere + "," + varSelected.ImportoAvere),"",MessageBoxButton.YesNo);
                if(res == MessageBoxResult.Yes)
                {
                    DBSqlLite.eliminaVariazione(varSelected);
                    listVariazioni = DBSqlLite.readVariazioni();
                    loadVariazioni();
                }
            }
        }

        private void btnInserisciStorico_Click(object sender, RoutedEventArgs e)
        {
            double importo = Double.Parse(txtImportoStorico.Text);
            Storico stor = new Storico();
            stor.Data = datePickerStorico.SelectedDate.Value;
            stor.Descr = txtDescrStorico.Text;
            stor.Fattura = txtFattura.Text;
            stor.Importo = importo;
            stor.Idcliente = utenteCorrente.Identifier;

            VariazioneEconomica varEco = new VariazioneEconomica(stor.Data, stor.Descr + " Fattura " + stor.Fattura, importo, true);
            
            
            DBSqlLite.aggiungiVariazione(varEco);
            stor.Idvariazione = varEco.IdVariazione;
            DBSqlLite.aggiungiStorico(stor);
            utenteCorrente.ListStorico = DBSqlLite.readStorico(utenteCorrente.Identifier);
            loadStoricoTabella();
            listVariazioni = DBSqlLite.readVariazioni();
            loadVariazioni();
        }

        private void btnEliminaStorico_Click(object sender, RoutedEventArgs e)
        {
            if(utenteCorrente != null && gridStoricoUtente.SelectedItem != null)
            {
                Storico stor = (Storico)gridStoricoUtente.SelectedItem;
                if (stor != null)
                {
                    MessageBoxResult res = System.Windows.MessageBox.Show("Eliminare riga: " + stor.Data + "," + stor.Descr + "," + stor.Fattura, "", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        DBSqlLite.eliminaStorico(stor);
                        utenteCorrente.ListStorico = DBSqlLite.readStorico(utenteCorrente.Identifier);
                        loadStoricoTabella();
                    }
                }
            }
        }

        private void btnModificaStorico_Click(object sender, RoutedEventArgs e)
        {
            if (utenteCorrente != null && gridStoricoUtente.SelectedItem != null)
            {
                Storico stor = (Storico)gridStoricoUtente.SelectedItem;
                WindowEdit windowEdit = new WindowEdit(stor);
                windowEdit.ShowDialog();
                utenteCorrente.ListStorico = DBSqlLite.readStorico(utenteCorrente.Identifier);
                loadStoricoTabella();
                listVariazioni = DBSqlLite.readVariazioni();
                loadVariazioni();
            }
        }

        //private void tabControlGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if(tabControlGeneral.SelectedIndex == 2)
        //    {
        //        listVariazioni = DBSqlLite.readVariazioni();
        //        loadVariazioni();
        //    }
        //}
    }

   
}
