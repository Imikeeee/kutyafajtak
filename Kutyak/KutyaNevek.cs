using System;
using System.Collections.Generic;
using System.Text;

namespace Kutyak
{
    public class KutyaNevek
    {
        public int azon { get; set; }
        public string nev { get; set; }

        public KutyaNevek(string sor)
        {
            this.azon = int.Parse(sor.Split(";")[0].Trim());
            this.nev = sor.Split(";")[1].Trim();
        }
    }
}
