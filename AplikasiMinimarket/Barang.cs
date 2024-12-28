using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiMinimarket
{
    public class Barang
    {
        public string IdBarang { get; set; }
        public string NamaBarang { get; set; }
        public int HargaBarang { get; set; }
        public int Diskon { get; set; }

        public override string ToString()
        {
            return $"{IdBarang}";
        }
    }
}