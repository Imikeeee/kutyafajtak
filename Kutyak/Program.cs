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
            try
            {
                beolvas();
                Console.WriteLine("3. feladat: " + kutyanevek.Count());
                Console.WriteLine("6. feladat: A kutyák átlagéletkora: " + atlag().ToString("0.##"));
                hetedik();
                nyolcadik();
                kilencedik();
                tiz();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("One or more CSV files not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        public static void beolvas()
        {
            string kutyaNevekFile = "KutyaNevek.csv";
            string kutyaFajtakFile = "KutyaFajtak.csv";
            string kutyakFile = "Kutyak.csv";

            if (!File.Exists(kutyaNevekFile) || !File.Exists(kutyaFajtakFile) || !File.Exists(kutyakFile))
            {
                throw new FileNotFoundException("One or more CSV files not found.");
            }

            foreach (var item in File.ReadAllLines(kutyaNevekFile, System.Text.Encoding.UTF8).Skip(1))
            {
                KutyaNevek kutya = new KutyaNevek(item);
                kutyanevek.Add(kutya);
            }

            foreach (var item in File.ReadAllLines(kutyaFajtakFile, System.Text.Encoding.UTF8).Skip(1))
            {
                KutyaFajtak kutya = new KutyaFajtak(item);
                kutyafajtak.Add(kutya);
            }

            foreach (var item in File.ReadAllLines(kutyakFile, System.Text.Encoding.UTF8).Skip(1))
            {
                Kutyak kutya = new Kutyak(item);
                kutyak.Add(kutya);
            }
        }

        public static decimal atlag()
        {
            if (kutyak.Count == 0)
            {
                return 0;
            }

            decimal atlag = kutyak.Average(a => a.kor);
            return atlag;
        }

        public static void hetedik()
        {
            if (kutyak.Count == 0)
            {
                Console.WriteLine("7. Feladat: Nincs kutya az adatbázisban.");
                return;
            }

            var kutya = kutyak.OrderByDescending(a => a.kor).First();
            Console.WriteLine("7. Feladat: A legidősebb kutya neve és fajtája: " +
                kutyanevek.Where(a => a.azon == kutya.nevazon).FirstOrDefault()?.nev + ", " +
                kutyafajtak.Where(a => a.id == kutya.fajtaazon).FirstOrDefault()?.nev);
        }

        public static void nyolcadik()
        {
            DateTime targetDate = DateTime.Parse("2018.01.10");
            var filteredKutyak = kutyak.Where(a => a.ideje == targetDate);

            if (filteredKutyak.Count() == 0)
            {
                Console.WriteLine("8. feladat: Nincs kutya az adott napon.");
                return;
            }

            Dictionary<string, int> fajtak = new Dictionary<string, int>();
            foreach (Kutyak kutya in filteredKutyak)
            {
                string key = kutyafajtak.Where(a => a.id == kutya.fajtaazon).Single().eredetinev;

                if (fajtak.ContainsKey(key))
                    fajtak[key]++;
                else
                    fajtak.Add(key, 1);
            }

            Console.WriteLine("8. feladat:");
            foreach (var item in fajtak)
            {
                Console.WriteLine("\t " + item.Key + ": " + item.Value + " kutya");
            }
        }

        public static void kilencedik()
        {
            if (kutyak.Count == 0)
            {
                Console.WriteLine("9. feladat: Nincs kutya az adatbázisban.");
                return;
            }

            Dictionary<string, int> napok = new Dictionary<string, int>();
            foreach (Kutyak kutya in kutyak)
            {
                string key = kutya.ideje.ToString("yyyy-MM-dd");

                if (napok.ContainsKey(key))
                    napok[key]++;
                else
                    napok.Add(key, 1);
            }

            var maxNap = napok.OrderByDescending(x => x.Value).FirstOrDefault();
            Console.WriteLine("9. feladat: Legjobban leterhelt nap: " + maxNap.Key + ": " + maxNap.Value + " kutya");
        }

        public static void tiz()
        {
            if (kutyak.Count == 0)
            {
                Console.WriteLine("10. feladat: Nincs kutya az adatbázisban.");
                return;
            }

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

            using (StreamWriter sw = new StreamWriter("Névstatisztika.txt", false, System.Text.Encoding.UTF8))
            {
                foreach (var item in sortednevek)
                {
                    sw.WriteLine(item.Key + ";" + item.Value);
                }
            }

            Console.WriteLine("10. feladat: névstatisztika.txt");
        }
    }
}
