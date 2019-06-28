using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMSI_NIP
{
    class Adres
    {
        public string wojewodztwo { get; set; }
        public string powiat { get; set; }
        public string gmina { get; set; }
        public string miejscowosc { get; set; }
        public string kodPocztowy { get; set; }
        public string ulica { get; set; }
        public string nrNieruchomosci { get; set; }
        public string nrLokalu { get; set; }

        public Adres()
        {

        }
    }
}
