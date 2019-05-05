using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace kolokvijOS
{
    class Program
    {
        static void sortirajPrioritet(string putanja)
        {
            Process[] procesi;
            procesi = Process.GetProcesses();

            using (StreamWriter sw = new StreamWriter(putanja))
            {
                sw.WriteLine("{0,-25} {1,-5}{2,-15}", "Naziv", "ID", "Prioritet");
                sw.WriteLine("{0,-25} {1,-5}{2,-15}", "=========================", "=====", "===============");
                Console.WriteLine("{0,-25} {1,-5}{2,-15}", "Naziv", "ID", "Prioritet");
                Console.WriteLine("{0,-25} {1,-5}{2,-15}", "=========================", "=====", "===============");
                foreach (Process p in procesi)
                {

                    sw.WriteLine("{0,-25} {1,-5}{2,-15}", p.ProcessName, p.Id, p.BasePriority);
                    Console.WriteLine("{0,-25} {1,-5}{2,-15}", p.ProcessName, p.Id, p.BasePriority);

                }

            }

        }

        static void detalji(int Id, string putanja)
        {
            Process proces = Process.GetProcessById(Id);
            Console.WriteLine("Naziv: {0}", proces.ProcessName);
            Console.WriteLine("Broj threadova: {0}", proces.Threads.Count);
            Console.WriteLine("Vrijeme pokretanja: {0}"/*, proces.StartTime.ToString()*/);
            Console.WriteLine("Naziv racunala na kojem je pokrenut: {0}", proces.MachineName);
            Console.WriteLine("Ukupno iskoristeno vrijeme procesora: {0}"/*, proces.TotalProcessorTime*/);
            Console.WriteLine("Peak memorije: {0} MB", proces.PeakVirtualMemorySize64 / (1024 * 1024));

            using (StreamWriter sw = new StreamWriter(putanja + @"\" + Id.ToString() + ".txt"))
            {
                sw.WriteLine("Naziv: {0}", proces.ProcessName);
                sw.WriteLine("Broj threadova: {0}", proces.Threads.Count);
                sw.WriteLine("Vrijeme pokretanja: {0}"/*, proces.StartTime*/);
                sw.WriteLine("Naziv racunala na kojem je pokrenut: {0}", proces.MachineName);
                sw.WriteLine("Ukupno iskoristeno vrijeme procesora: {0}"/*, proces.TotalProcessorTime*/);
                sw.WriteLine("Peak memorije: {0} MB", proces.PeakVirtualMemorySize64 / (1024 * 1024));

            }

        }

        static void zauzimajuViseOd(int velicina)
        {
            IEnumerable<Process> procesi;
            procesi = Process.GetProcesses()
                .Where(proces => proces.PrivateMemorySize64 / (1024 * 1024) > velicina);

            Console.WriteLine("{0,-25} {1,-5} {2,-15}", "Naziv", "ID", "Memorija");
            Console.WriteLine("{0,-25} {1,-5} {2,-15}", "=========================", "=====", "===============");
            foreach (Process p in procesi)
            {

                Console.WriteLine("{0,-25} {1,-5} {2,-15}MB", p.ProcessName, p.Id, p.PrivateMemorySize64 / (1024 * 1024));


            }
        }

        static void procitajTxt(string putanja)
        {
            using (StreamReader sr = new StreamReader(putanja))
            {

                string line;
                int brojac = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    brojac++;
                    Process proces = new Process();
                    proces.StartInfo.FileName = line;
                    proces.Start();
                    Console.WriteLine("Proces {0} je uspješno pokrenut!", line);

                }

                Console.WriteLine("Ukupno je pokrenuto {0} procesa cija su se imena nalazila u {1}", brojac.ToString(), putanja);

            }
        }

        static void filtrirajPremaSlovu(char slovo)
        {
            IEnumerable<Process> procesi = Process.GetProcesses()
                .Where(proces => (proces.ProcessName[0] == Char.ToLower(slovo)) || (proces.ProcessName[0]==Char.ToUpper(slovo)));

            int broj = procesi.Count();


            Console.WriteLine("{0,-25} {1,-5}{2,-15}", "Naziv", "ID", "Prioritet");
            Console.WriteLine("{0,-25} {1,-5}{2,-15}", "=========================", "=====", "===============");
            foreach (Process p in procesi)
            {
                Console.WriteLine("{0,-25} {1,-5}{2,-15}", p.ProcessName, p.Id, p.BasePriority);
            }

            Console.WriteLine("Ukupno {0} procesa počinje sa slovom {1}", broj.ToString(), slovo);
        }


        static void Main(string[] args)
        {
      
        }


    }
}
