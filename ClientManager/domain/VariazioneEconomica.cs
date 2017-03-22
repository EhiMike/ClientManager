using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.domain
{
    public class VariazioneEconomica
    {
        private DateTime data;
        private string descrizioneDare = "";
        private double importoDare = 0;
        private string descrizioneAvere = "";
        private double importoAvere = 0;
        private char dareAvere = 'D'; // D = dare -  A = Avere 

        public VariazioneEconomica(DateTime date,string descr, double imp, bool dare)
        {
            data = date;
           
            if (dare)
            {
                DareAvere = 'D';
                DescrizioneDare = descr;
                ImportoDare = imp;
            }
            else
            {
                DareAvere = 'A';
                DescrizioneAvere = descr;
                ImportoAvere = imp;
            }
        }

        public DateTime Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        

        public char DareAvere
        {
            get
            {
                return dareAvere;
            }

            set
            {
                dareAvere = value;
            }
        }

        public string DescrizioneDare
        {
            get
            {
                return descrizioneDare;
            }

            set
            {
                descrizioneDare = value;
            }
        }

        public double ImportoDare
        {
            get
            {
                return importoDare;
            }

            set
            {
                importoDare = value;
            }
        }

        public string DescrizioneAvere
        {
            get
            {
                return descrizioneAvere;
            }

            set
            {
                descrizioneAvere = value;
            }
        }

        public double ImportoAvere
        {
            get
            {
                return importoAvere;
            }

            set
            {
                importoAvere = value;
            }
        }

        public bool isDare()
        {
            return dareAvere == 'D';
        }
    }
}
