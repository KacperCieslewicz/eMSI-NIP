using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMSI_NIP
{
    class Podmiot
    {
        public string regon { get; set; }
        public string statusNip { get; set; }
        public string nazwa { get; set; }
        public string typ { get; set; }
        public string dataZakonczeniaDzialalnosci { get; set; }
        public Adres adres { get; set; }

        public Podmiot()
        {

        }

    }
}
