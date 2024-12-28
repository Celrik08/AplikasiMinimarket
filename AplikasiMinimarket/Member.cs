using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiMinimarket
{
    public class Member
    {
        public string IdMember { get; set; }
        public string NamaMember { get; set; }

        public override string ToString()
        {
            return $"{IdMember}";
        }
    }
}