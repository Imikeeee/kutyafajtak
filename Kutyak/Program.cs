using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Kutyak
{
    class Program
    {
        public static List<KutyaNevek> kutyanevek = new List<KutyaNevek>();
        public static List<KutyaFajtak> kutyafajtak = new List<KutyaFajtak>();
        public static List<Kutyak> kutyak = new List<Kutyak>();
        static void Main(string[] args)
        {
            beolvas();
            Console.WriteLine("3. feladat: "+kutyanevek.Count()) ;
            Console.WriteLine("6. feladat: A kutyák átlagéletkora: " + atlag().ToString("0.##"));
            hetedik();
            nyolcadik();
            kilencedik();
            tiz();
            Console.ReadKey();
        }

        public static void beolvas()
        {
            
            foreach (var item in File.ReadAllLines("KutyaNevek.csv", System.Text.Encoding.UTF8).Skip(1))
            {
                KutyaNevek kutya = new KutyaNevek(item);
                kutyanevek.Add(kutya);
            }

            foreach (var item in File.ReadAllLines("KutyaFajtak.csv", System.Text.Encoding.UTF8).Skip(1))
            {
                KutyaFajtak kutya = new KutyaFajtak(item);
                kutyafajtak.Add(kutya);
            }

            foreach (var item in File.ReadAllLines("Kutyak.csv", System.Text.Encoding.UTF8).Skip(1))
            {
                Kutyak kutya = new Kutyak(item);
                kutyak.Add(kutya);
            }
        }
        public static decimal atlag()
        {
            /* LINQ-es sum helyett hagyományos megoldás az összegzésre
            decimal total = 0;
            foreach (var item in kutyak)
            { total += item.kor;}        */
            decimal atlag = 0;
            var total = kutyak.Sum(a => a.kor);
            atlag =(decimal) total / (decimal)kutyak.Count();
            return atlag;
        }

        public static void hetedik()
        {
            /* hagyományos megoldás
            int maxkor = kutyak[0].kor;
            string neve = "", fajtaja = "";
            foreach (var item in kutyak)
            {
                if (item.kor > maxkor )
                {
                    maxkor = item.kor;
                    neve = kutyanevek.Where(a => a.azon == item.nevazon).FirstOrDefault().nev;   //itt is további foreach kellene LINQ nélkül :(
                    fajtaja = kutyafajtak.Where(a => a.id == item.fajtaazon).FirstOrDefault().nev;
                }
            }
            Console.WriteLine("7. Feladat: A legidősebb kutya neve és fajtája: " + neve + ", " + fajtaja); 
            */

            var kutya = kutyak.OrderByDescending(a => a.kor).First();
            Console.WriteLine("7. Feladat: A legidősebb kutya neve és fajtája: "+kutyanevek.Where(a=>a.azon==kutya.nevazon).FirstOrDefault().nev+", "+ kutyafajtak.Where(a => a.id == kutya.fajtaazon).FirstOrDefault().nev);
            
        }

        public static void nyolcadik()
        {

            Dictionary<string, int> fajtak = new Dictionary<string, int>();
            foreach (Kutyak kutya in kutyak)
            {
                if (kutya.ideje == DateTime.Parse("2018.01.10"))
                {
                    string key = kutyafajtak.Where(a => a.id == kutya.fajtaazon).Single().eredetinev;

                    if (fajtak.ContainsKey(key))
                                                 fajtak[key]++;
                    else
                        fajtak.Add(key, 1);
                }         
            }
            Console.WriteLine("8. feladat:");
            foreach (var item in fajtak)
            {
                Console.WriteLine("\t "+item.Key+": "+item.Value+" kutya");
            }

        }
        public static void kilencedik()
        {
            Dictionary<string, int> napok = new Dictionary<string, int>();
            foreach (Kutyak kutya in kutyak)
            {
                string key = kutya.ideje.ToString("yyyy-MM-dd");

                    if (napok.ContainsKey(key))
                    napok[key]++;
                    else
                    napok.Add(key, 1);
            }
            Console.WriteLine("9. feladat: Legjobban leterhelt nap: "+ napok.FirstOrDefault(x => x.Value == napok.Values.Max()).Key+ ": "+napok.Values.Max()+" kutya"); 

        }

        public static void tiz()
        {
            using (StreamWriter sw = new StreamWriter("Névstatisztika.txt",false, System.Text.Encoding.UTF8))
            {
                Dictionary<string, int> nevek = new Dictionary<string, int>();
                foreach (Kutyak kutya in kutyak)
                {
                    string key = kutyanevek.Where(a => a.azon == kutya.nevazon).Single().nev;

                    if (nevek.ContainsKey(key))
                        nevek[key]++;
                    else
                        nevek.Add(key, 1);
                }
               var sortednevek = from entry in nevek orderby entry.Value descending select entry;
                foreach (var item in sortednevek)
                {
                    sw.WriteLine(item.Key + ";" + item.Value);
                }
         
            }
            Console.WriteLine("10. feladat: névstatisztika.txt");
        }
    }
}
