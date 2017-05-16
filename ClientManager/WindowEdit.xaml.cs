using ClientManager.domain;
using ClientManager.support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class WindowEdit : Window
    {
        private string type = "var";
        private VariazioneEconomica varToEdit;
        private Presenza presToEdit;
        private Storico storToEdit;

        public WindowEdit(VariazioneEconomica var)
        {
            InitializeComponent();
            type = "var";
            varToEdit = var;
            setVisibility();
            datePickerVariazione.SelectedDate = var.Data;
            if (var.isDare())
            {
                textBoxDescrVar.Text = var.DescrizioneDare;
                textBoxImportoVar.Text = var.ImportoDare.ToString();
            }
            else
            {
                textBoxDescrVar.Text = var.DescrizioneAvere;
                textBoxImportoVar.Text = var.ImportoAvere.ToString();
            }

            RbDare.IsChecked = var.isDare();
            rbAvere.IsChecked = !var.isDare();

            
        }

        public WindowEdit(Presenza pres)
        {
            InitializeComponent();
            type = "pres";
            presToEdit = pres;
            setVisibility();
            datePickerIngressi.SelectedDate = pres.Data;
            textBoxOraIn.Text = pres.OraIngresso.ToString();
            textBoxOraOut.Text = pres.OraUscita.ToString();
        }

        public WindowEdit(Storico stor)
        {
            InitializeComponent();
            type = "stor";
            storToEdit = stor;
            setVisibility();
            datePickerStorico.SelectedDate = stor.Data;
            textBoxDescrStorico.Text = stor.Descr;
            textBoxFatturaStorico.Text = stor.Fattura;
            textBoxImportoStorico.Text = stor.Importo.ToString();
        }

        private void setVisibility()
        {
            if(type != "var")
            {
                colVar1.Width = colVar2.Width = colVar3.Width = new GridLength(0);
                
            }
            if (type != "pres")
            {
                colPres1.Width = colPres2.Width = colPres3.Width = new GridLength(0);
            }
            if (type != "stor")
            {
                colStor1.Width = colStor2.Width = colStor3.Width = new GridLength(0);
            }
            this.Width = 300;
            switch (type)
            {
                case "pres":
                    btnConfirm.SetValue(Grid.ColumnProperty, 1);
                    btnAnnulla.SetValue(Grid.ColumnProperty, 2);
                    break;
                case "var":
                    btnConfirm.SetValue(Grid.ColumnProperty, 4);
                    btnAnnulla.SetValue(Grid.ColumnProperty, 5);
                    break;
                case "stor":
                    btnConfirm.SetValue(Grid.ColumnProperty, 7);
                    btnAnnulla.SetValue(Grid.ColumnProperty, 8);
                    break;
            }
            
        }

        private void textBoxImporto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case "var":
                    if (!Helper.checkVariazioneCompleta(textBoxDescrVar.Text, textBoxImportoVar.Text))
                    {
                        MessageBox.Show("Errore inserimento dati");
                        return;
                    }
                    varToEdit.Data = datePickerVariazione.SelectedDate.Value;
                    varToEdit.DareAvere = Convert.ToBoolean(RbDare.IsChecked) ? 'D' : 'A';
                    double importo = Double.Parse(textBoxImportoVar.Text);
                    if (RbDare.IsChecked.Value)
                    {
                        varToEdit.DescrizioneDare = textBoxDescrVar.Text;
                        varToEdit.DescrizioneAvere = "";
                        varToEdit.ImportoDare = importo;
                        varToEdit.ImportoAvere = 0;
                    }
                    else
                    {
                        varToEdit.DescrizioneDare = "";
                        varToEdit.DescrizioneAvere = textBoxDescrVar.Text;
                        varToEdit.ImportoDare = 0;
                        varToEdit.ImportoAvere = importo;
                    }
                    DBSqlLite.modificaVariazione(varToEdit);
                    break;
                case "pres":
                    presToEdit.Data = datePickerIngressi.SelectedDate.Value;
                    presToEdit.OraIngresso = TimeSpan.Parse(textBoxOraIn.Text);
                    presToEdit.OraUscita = TimeSpan.Parse(textBoxOraOut.Text);
                    DBSqlLite.modificaPresenza(presToEdit);
                    break;
                case "stor":
                    storToEdit.Data = datePickerStorico.SelectedDate.Value;
                    storToEdit.Descr = textBoxDescrStorico.Text;
                    storToEdit.Fattura = textBoxFatturaStorico.Text;
                    storToEdit.Importo = Double.Parse(textBoxImportoStorico.Text);
                    DBSqlLite.modificaStorico(storToEdit);
                    VariazioneEconomica varEco = new VariazioneEconomica(storToEdit.Data, storToEdit.Descr + " Fattura " + storToEdit.Fattura, storToEdit.Importo, true);
                    varEco.IdVariazione = storToEdit.Idvariazione;
                    DBSqlLite.modificaVariazione(varEco);
                    break;
            }
           
            this.Close();
        }

        private void btnAnnulla_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
