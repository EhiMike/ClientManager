using support;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logica di interazione per WindowAbi.xaml
    /// </summary>
    public partial class WindowAbi : Window
    {
        private string toVerify;
        public WindowAbi()
        {
            InitializeComponent();
            DateTime dateNow = DateTime.Now;
            toVerify = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ff",
                                            CultureInfo.InvariantCulture).Replace("-", "").Replace(":", "").Replace(".", "").Replace(" ","");
            labelSerial.Content += " " + toVerify;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string code = tbSerial1.Text + tbSerial2.Text + tbSerial3.Text + tbSerial4.Text;
            if (CoreGenerator.calculate(toVerify).Equals(code))
            {
                string SN = Helper.mergeString(toVerify,code);
                Helper.writeRegistryKey("SN", SN);
                Helper.writeRegistryKey("DSK", Helper.diskSerial());
                DialogResult = true;
            }else
            {
                DialogResult = false;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(toVerify);
        }
    }
}
