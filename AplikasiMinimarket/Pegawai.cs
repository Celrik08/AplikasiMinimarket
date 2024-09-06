using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiMinimarket
{
    internal class Pegawai
    {
        public string IdPegawai { get; set; }
        public string NamaPegawai { get; set; }

        public override string ToString()
        {
            return $"{IdPegawai} - {NamaPegawai}";
        }
    }
}
