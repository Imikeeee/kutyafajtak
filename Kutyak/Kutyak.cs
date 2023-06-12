using System;
using System.Collections.Generic;
using System.Text;

namespace Kutyak
{
    class Kutyak
    {
        public int vizsgazon { get; set; }
        public int fajtaazon { get; set; }
        public int nevazon { get; set; }
        public int kor { get; set; }
        public DateTime ideje { get; set; }

        public Kutyak(string line)
        {
            this.vizsgazon = int.Parse(line.Split(";")[0].Trim());
            this.fajtaazon = int.Parse(line.Split(";")[1].Trim());
            this.nevazon = int.Parse(line.Split(";")[2].Trim());
            this.kor = int.Parse(line.Split(";")[3].Trim());
            this.ideje = DateTime.Parse(line.Split(";")[4]);
        }


    }
}
