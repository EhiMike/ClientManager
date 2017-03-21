using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.domain
{
    public class Abbonamento
    {
        private int idAbb;
        private int tipoAbb;
        private DateTime dataIscrizione;
        private DateTime dataScadenza;
        private int idCliente;

        public Abbonamento(int idA,int type,DateTime dataI,DateTime dataS,int idC)
        {
            idAbb = idA;
            tipoAbb = type;
            dataIscrizione = dataI;
            dataScadenza = dataS;
            idCliente = idC;
        }

        public int IdAbb
        {
            get
            {
                return idAbb;
            }

            set
            {
                idAbb = value;
            }
        }

        public int TipoAbb
        {
            get
            {
                return tipoAbb;
            }

            set
            {
                tipoAbb = value;
            }
        }

        public DateTime DataIscrizione
        {
            get
            {
                return dataIscrizione;
            }

            set
            {
                dataIscrizione = value;
            }
        }

        public DateTime DataScadenza
        {
            get
            {
                return dataScadenza;
            }

            set
            {
                dataScadenza = value;
            }
        }

        public int IdCliente
        {
            get
            {
                return idCliente;
            }

            set
            {
                idCliente = value;
            }
        }

        public static List<String> getTypeAbbonamentoList()
        {
            List<String> listAbbonamento = new List<String>();
            foreach (TIPO_ABBONAMENTO t in Enum.GetValues(typeof(TIPO_ABBONAMENTO)))
            {
                listAbbonamento.Add(t.ToString());
            }
            return listAbbonamento;
        }
    }

    public enum TIPO_ABBONAMENTO
    {
        MENSILE = 1, BIMESTRALE = 2, TRIMESTRALE = 3, SEMESTRALE = 4, ANNUALE = 5
    }

    
}
