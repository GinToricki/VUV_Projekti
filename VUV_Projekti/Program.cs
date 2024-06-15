using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{

    class Program
    {
        static void PrikaziListuProjekata()
        {

        }

        static void DodajProjekt()
        {

        }

        static void AzurirajProjekt()
        {

        }

        static void DodajAktivnost()
        {

        }
        static void IspisiListuClanovaProjekta()
        {

        }
       
        static void DodajClana()
        {
            try
            {

                Console.WriteLine("Unesite ime člana");
                string ime = Console.ReadLine();
                Console.WriteLine("Unesite prezime člana");
                string prezime = Console.ReadLine();
                Console.WriteLine("Unesite OIB člana");
                string oib = Console.ReadLine();
                DateTime dob = DateTime.Now;
                ClanProjekta noviClan = new ClanProjekta(ime, prezime,oib, dob);
            }catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }
        static void BrisanjeClana()
        {

        }

        static void Statistika()
        {

        }
        static void Izbornik()
        {
            Console.WriteLine("1. Lista projekata\n2. Dodavanje projekta\n3. Ažuriranje projekta\n4. Dodavanje aktivnost\n5. Lista članova projekta\n6. Dodavanje člana\n7. Brisanje člana\n8. Statistika\n9.izlaz ");
            Console.WriteLine("Unesite vas odabir");
            bool pokreni = true;
            while (pokreni)
            {
                string odabir = Console.ReadLine();
                switch (odabir)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        DodajClana();
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        pokreni = false;
                        break;
                    default:
                        break;
                }
            }
        }
        static void Main(string[] args)
        {

            ClanProjekta c1 = new ClanProjekta("Tin", "Tinic", "1234567890", DateTime.Now);
            ClanProjekta c2 = new ClanProjekta("ime2", "prezime2", "123421890", DateTime.Now);
            Projekt p1 = new Projekt("teStniprojekt", new List<ClanProjekta> { c1, c2 }, null);
            p1.IzbrisiClana();
            Console.ReadKey();
        }
    }
}
