using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiMinimarket
{
    public class Pegawai
    {
        public int IdPegawai { get; set; }
        public string NamaPegawai { get; set; }

        public override string ToString()
        {
            return NamaPegawai;
        }
    }
}
