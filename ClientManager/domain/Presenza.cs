using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.domain
{
   public class Presenza
    {
        private int idPresenza = -1;
        private DateTime data;
        private TimeSpan oraIngresso;
        private TimeSpan oraUscita;

        public Presenza()
        {

        }

        public Presenza(DateTime data, TimeSpan oraIngresso, TimeSpan oraUscita)
        {
            this.Data = data;
            this.OraIngresso = oraIngresso;
            this.OraUscita = oraUscita;
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

        public TimeSpan OraIngresso
        {
            get
            {
                return oraIngresso;
            }

            set
            {
                oraIngresso = value;
            }
        }

        public TimeSpan OraUscita
        {
            get
            {
                return oraUscita;
            }

            set
            {
                oraUscita = value;
            }
        }

        public int IdPresenza
        {
            get
            {
                return idPresenza;
            }

            set
            {
                idPresenza = value;
            }
        }

        public String getDataString()
        {
            return Data.ToShortDateString();
        }
    }
}
