using ClientManager.domain;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientManager
{
    /// <summary>
    /// Logica di interazione per WindowUtente.xaml
    /// </summary>
    public partial class WindowUtente : Window
    {
        private Utente mUtente = null;
        private SortedList<string, Utente> users;
        private SortedList<string, Provincia> provinciaBinding = new SortedList<string, Provincia>();
        private Boolean update = false;

        public WindowUtente(Utente utente = null)
        {
            InitializeComponent();
            mUtente = utente;
            update = mUtente != null;

            if (update)
            {
                this.Title = "Modifica utente";
            }else
            {
                this.Title = "Nuovo utente";
            }

            comboBoxProvincia.Items.Clear();
            foreach (Provincia prov in Helper.getProvince().Values)
            {
                comboBoxProvincia.Items.Add(prov.getTestoProvincia());
                provinciaBinding.Add(prov.getTestoProvincia(), prov);
            }
            comboBoxStato.Items.Clear();
            foreach (string prov in Helper.getStati())
            {
                comboBoxStato.Items.Add(prov);
            }

            if (mUtente != null)
            {
                riempiDatiUtente();
            }
        }

        public void setUsersList(SortedList<string,Utente> listusers)
        {
            users = listusers;
        }

        private void riempiDatiUtente()
        {
            textBoxNome.Text = mUtente.Nome;
            textBoxCognome.Text = mUtente.Cognome;
            textBoxCF.Text = mUtente.CodiceFiscale;
            textBoxIndirizzo.Text = mUtente.Indirizzo;
            textBoxLuogoNascita.Text = mUtente.LuogoNascita;
            textBoxMail.Text = mUtente.Email;
            textBoxTelefono.Text = mUtente.Telefono;
            datePickerNascita.SelectedDate = mUtente.DataDiNascita;
            datePickerAbb.SelectedDate = mUtente.ScadenzaAbb;
            datePickerVM.SelectedDate = mUtente.ScadenzaVisitaMedica;
            comboBoxProvincia.SelectedItem = mUtente.Provincia.getTestoProvincia();
            comboBoxStato.SelectedItem = mUtente.Stato;
            rbSessoM.IsChecked = mUtente.Sesso == "M";
            rbSessoF.IsChecked = mUtente.Sesso == "F";


            if (File.Exists(mUtente.getDocumentPath("CI")))
            {
                lblCIPresente.Content = "SI";
            }else
            {
                lblCIPresente.Content = "NO";
            }

            if (File.Exists(mUtente.getDocumentPath("VM")))
            {
                lblPresenteVM.Content = "SI";
            }
            else
            {
                lblPresenteVM.Content = "NO";
            }

        }

        private void btnAnnulla_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox tb = ((TextBox)sender);
        //    tb.Text = tb.Text.ToUpper();
        //    tb.CaretIndex = tb.Text.Length;
        //}

        //private void textBoxMail_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    textBoxMail.Text = textBoxMail.Text.ToLower();
        //    textBoxMail.CaretIndex = textBoxMail.Text.Length;
        //}

        private void btnConferma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkAllFilled()) {
                    if (mUtente == null)
                    {
                        mUtente = new Utente();
                        if(users != null)
                        {
                            users.Add(mUtente.Identifier,mUtente);
                        }
                    }

                    mUtente.Nome = textBoxNome.Text;
                    mUtente.Cognome = textBoxCognome.Text;
                    mUtente.DataDiNascita = datePickerNascita.SelectedDate.Value;
                    mUtente.Email = textBoxMail.Text;
                    mUtente.Indirizzo = textBoxIndirizzo.Text;
                    mUtente.LuogoNascita = textBoxLuogoNascita.Text;
                    mUtente.ScadenzaAbb = datePickerAbb.SelectedDate.Value;
                    mUtente.ScadenzaVisitaMedica = datePickerVM.SelectedDate.Value;
                    mUtente.Sesso = rbSessoM.IsChecked.Value ? "M" : "F";
                    mUtente.Stato = comboBoxStato.SelectedItem.ToString();
                    mUtente.Telefono = textBoxTelefono.Text;
                    mUtente.CodiceFiscale = textBoxCF.Text;
                    if (provinciaBinding.ContainsKey(comboBoxProvincia.SelectedItem.ToString()))
                    {
                        mUtente.Provincia = provinciaBinding[comboBoxProvincia.SelectedItem.ToString()];
                    }

                    if (update)
                    {
                        DBHelper.modificaCliente(mUtente);
                    }
                    else
                    {
                        DBHelper.aggiungiCliente(mUtente);
                    }
                    
                    this.Close();
                }else
                {
                    MessageBox.Show("Completare tutti i campi");
                }
                

            }
            catch(Exception ex)
            {
                Helper.Logger(ex.Message);
            }
        }

        private bool checkAllFilled()
        {
            try
            {
                if (String.IsNullOrEmpty(textBoxNome.Text))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(textBoxCognome.Text))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(textBoxIndirizzo.Text))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(textBoxLuogoNascita.Text))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(textBoxCF.Text))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(textBoxMail.Text))
                {
                    return false;
                }
                if (String.IsNullOrEmpty(textBoxTelefono.Text))
                {
                    return false;
                }
                if(comboBoxProvincia.SelectedIndex == 0)
                {
                    return false;
                }
                if (comboBoxStato.SelectedIndex == 0)
                {
                    return false;
                }
                if(!rbSessoF.IsChecked.Value && !rbSessoM.IsChecked.Value)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Helper.Logger(ex.Message);
            }
            return true;
        }

        private void btnCaricaCI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create an instance of the open file dialog box.
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // Set filter options and filter index.
                openFileDialog1.Filter = "Text Files (.pdf)|*.pdf|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;

                openFileDialog1.Multiselect = true;

                // Call the ShowDialog method to show the dialog box.
                bool? userClickedOK = openFileDialog1.ShowDialog();

                // Process input if the user clicked OK.
                if (userClickedOK == true)
                {
                    // Open the selected file to read.
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        string fileName = System.IO.Path.GetFileName(openFileDialog1.FileName);
                        string clientFolder = Helper.pathAppData + mUtente.Identifier;
                        if (!Directory.Exists(clientFolder))
                        {
                            Directory.CreateDirectory(clientFolder);
                        }
                        File.Copy(openFileDialog1.FileName, clientFolder + "\\CI.pdf");
                        lblCartaIdentita.Content = "SI";
                    }

                }
            }
            catch(Exception ex)
            {
                Helper.Logger(ex.Message);
            }
        }

        private void btnCaricaVM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create an instance of the open file dialog box.
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // Set filter options and filter index.
                openFileDialog1.Filter = "Text Files (.pdf)|*.pdf|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;

                openFileDialog1.Multiselect = true;

                // Call the ShowDialog method to show the dialog box.
                bool? userClickedOK = openFileDialog1.ShowDialog();

                // Process input if the user clicked OK.
                if (userClickedOK == true)
                {
                    // Open the selected file to read.
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        string fileName = System.IO.Path.GetFileName(openFileDialog1.FileName);
                        string clientFolder = Helper.pathAppData + mUtente.Identifier;
                        if (!Directory.Exists(clientFolder))
                        {
                            Directory.CreateDirectory(clientFolder);
                        }
                        File.Copy(openFileDialog1.FileName, clientFolder + "\\visita.pdf");
                        lblPresenteVM.Content = "SI";
                    }

                }
            }
            catch (Exception ex)
            {
                Helper.Logger(ex.Message);
            }
        }
    }
}
