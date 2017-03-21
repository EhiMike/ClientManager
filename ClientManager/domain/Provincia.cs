using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.domain
{
   public class Provincia
    {
        private String abbr;
        private String nome;
        private String regione;

        public Provincia(string a,string n,string r)
        {
            abbr = a;
            nome = n;
            regione = r;
        }

        public string Abbr
        {
            get
            {
                return abbr;
            }

            set
            {
                abbr = value;
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public string Regione
        {
            get
            {
                return regione;
            }

            set
            {
                regione = value;
            }
        }

        public string getTestoProvincia()
        {
            return nome + "(" + abbr.ToUpper() +")";
        }
    }
}
