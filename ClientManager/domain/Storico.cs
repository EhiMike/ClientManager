using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.domain
{
    public class Storico
    {
        private int idstorico;
        private string idcliente;
        private DateTime data;
        private string descr;
        private string fattura;
        private Double importo;

        public Storico() { }

        public int Idstorico
        {
            get
            {
                return idstorico;
            }

            set
            {
                idstorico = value;
            }
        }

        public string Idcliente
        {
            get
            {
                return idcliente;
            }

            set
            {
                idcliente = value;
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

        public string Descr
        {
            get
            {
                return descr;
            }

            set
            {
                descr = value;
            }
        }

        public string Fattura
        {
            get
            {
                return fattura;
            }

            set
            {
                fattura = value;
            }
        }

        public double Importo
        {
            get
            {
                return importo;
            }

            set
            {
                importo = value;
            }
        }
    }
}
