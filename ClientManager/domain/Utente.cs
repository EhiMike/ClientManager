using ClientManager.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager
{
    public class Utente
    {
        private string identifier;
        private string nome, cognome, codiceFiscale,indirizzo,luogoNascita,stato,sesso,telefono,email;
        private Provincia provincia;
        private DateTime dataDiNascita, scadenzaAbb, scadenzaVisitaMedica;
        private Boolean attivo = true;
        private Abbonamento abbonamento;
        private SortedList<DateTime, Presenza> listPresenze = new SortedList<DateTime, Presenza>();
        private SortedList<DateTime, Storico> listStorico = new SortedList<DateTime, Storico>();

        public Utente(string id = null)
        {
            DateTime now = DateTime.Now;
            if(id == null)
            {
                Identifier = now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
            }else
            {
                identifier = id;
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

        public string Cognome
        {
            get
            {
                return cognome;
            }

            set
            {
                cognome = value;
            }
        }

        public string CodiceFiscale
        {
            get
            {
                return codiceFiscale;
            }

            set
            {
                codiceFiscale = value;
            }
        }

        public string Indirizzo
        {
            get
            {
                return indirizzo;
            }

            set
            {
                indirizzo = value;
            }
        }

        public string LuogoNascita
        {
            get
            {
                return luogoNascita;
            }

            set
            {
                luogoNascita = value;
            }
        }

        

        public string Stato
        {
            get
            {
                return stato;
            }

            set
            {
                stato = value;
            }
        }

        public string Sesso
        {
            get
            {
                return sesso;
            }

            set
            {
                sesso = value;
            }
        }

        public string Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public DateTime DataDiNascita
        {
            get
            {
                return dataDiNascita;
            }

            set
            {
                dataDiNascita = value;
            }
        }

        public DateTime ScadenzaAbb
        {
            get
            {
                return scadenzaAbb;
            }

            set
            {
                scadenzaAbb = value;
            }
        }

        public DateTime ScadenzaVisitaMedica
        {
            get
            {
                return scadenzaVisitaMedica;
            }

            set
            {
                scadenzaVisitaMedica = value;
            }
        }

        public string Identifier
        {
            get
            {
                return identifier;
            }

            set
            {
                identifier = value;
            }
        }

        public Provincia Provincia
        {
            get
            {
                return provincia;
            }

            set
            {
                provincia = value;
            }
        }

        public bool Attivo
        {
            get
            {
                return attivo;
            }

            set
            {
                attivo = value;
            }
        }

        public Abbonamento Abbonamento
        {
            get
            {
                return abbonamento;
            }

            set
            {
                abbonamento = value;
            }
        }

        internal SortedList<DateTime, Presenza> ListPresenze
        {
            get
            {
                return listPresenze;
            }

        }

        public SortedList<DateTime, Storico> ListStorico
        {
            get
            {
                return listStorico;
            }

            set
            {
                listStorico = value;
            }
        }

        public string getPathUser()
        {
            return Helper.pathData + Identifier + "\\";
        }

        public string getDocumentPath(string type)
        {
            if (type.ToUpper().Equals("VM"))
            {
               return  getPathUser() + "visita.pdf";
            }else
            {
                return getPathUser() + "CI.pdf";
            }
        }

        public DateTime getLastKey()
        {
            DateTime lastKey = ListPresenze.Keys[ListPresenze.Keys.Count - 1];
            return lastKey;
        }
    }
}
