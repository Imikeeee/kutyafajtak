using System;
using System.Collections.Generic;
using System.Text;

namespace Kutyak
{
    class KutyaFajtak
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string eredetinev { get; set; }

        public KutyaFajtak(string sor)
        {
            this.id = int.Parse(sor.Split(";")[0].Trim());
            this.nev = sor.Split(";")[1].Trim();
            this.eredetinev = sor.Split(";")[2].Trim();
        }
    }
}
