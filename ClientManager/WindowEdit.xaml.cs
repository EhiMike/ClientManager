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

        public WindowEdit(VariazioneEconomica var)
        {
            InitializeComponent();
            type = "var";
            varToEdit = var;
            rowPresenza.Height = new GridLength(0);
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

            this.Height -= 40;
        }

        public WindowEdit(Presenza pres)
        {
            InitializeComponent();
            type = "pres";
            presToEdit = pres;
            rowVariazione.Height = new GridLength(0);
            datePickerIngressi.SelectedDate = pres.Data;
            textBoxOraIn.Text = pres.OraIngresso.ToString();
            textBoxOraOut.Text = pres.OraUscita.ToString();
            this.Height -= 40;
        }

        private void textBoxImporto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(type == "var")
            {
                if (!Helper.checkVariazioneCompleta(textBoxDescrVar.Text, textBoxImportoVar.Text))
                {
                    MessageBox.Show("Errore inserimento dati");
                    return;
                }
                varToEdit.Data = datePickerVariazione.SelectedDate.Value;
                varToEdit.DareAvere = Convert.ToBoolean(RbDare.IsChecked)?'D':'A';
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
            }else
            {
                presToEdit.Data = datePickerIngressi.SelectedDate.Value;
                presToEdit.OraIngresso = TimeSpan.Parse(textBoxOraIn.Text);
                presToEdit.OraUscita = TimeSpan.Parse(textBoxOraOut.Text);
                DBSqlLite.modificaPresenza(presToEdit);
            }
            this.Close();
        }

        private void btnAnnulla_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
