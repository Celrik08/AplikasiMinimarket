﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiMinimarket
{
    internal class Gudang
    {
        public string IdGudang { get; set; }
        public string NamaGudang { get; set; }

        public override string ToString()
        {
            return $"{IdGudang} - {NamaGudang}";
        }
    }
}