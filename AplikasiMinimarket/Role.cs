using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiMinimarket
{
    internal class Role
    {
        public int IdRole { get; set; }
        public string NamaRole { get; set; }

        public override string ToString()
        {
            return $"{IdRole} - {NamaRole}";
        }
    }
}
