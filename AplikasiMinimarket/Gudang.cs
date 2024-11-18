using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiMinimarket
{
    public class Gudang
    {
        public int IdGudang { get; set; }
        public string NamaGudang { get; set; }

        public override string ToString()
        {
            return NamaGudang;
        }
    }
}