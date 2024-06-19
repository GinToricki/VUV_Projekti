using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace VUV_Projekti
{
    class Izbornik
    {
        public void PrikaziIzbornik()
        {
            DatotetaKlasa dk1 = new DatotetaKlasa();
            List<ClanProjekta> clanoviProjekta = dk1.UcitajClanove();
            List<Aktivnost> aktivnosti = dk1.UcitajAktivnosti();
            List<Projekt> projekti = dk1.UcitajProjekte();
            List<Lokacija> lokacije = dk1.UcitajLokacije();

            bool pokreni = true;

            while (pokreni)
            {
                Console.WriteLine("1. Lista projekata\n2. Dodavanje projekta\n3. Ažuriranje projekta\n4. Dodavanje aktivnost\n5. Lista članova projekta\n6. Dodavanje člana\n7. Brisanje člana\n8. Statistika\n9.izlaz ");
                Console.WriteLine("Unesite vas odabir");
                string odabir = "";
                try
                {
                    odabir = Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                switch (odabir)
                {
                    case "1":
                        prikaziProjekte(dk1);
                        break;
                    case "2":

                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        IspisiListuClanova(clanoviProjekta, projekti, aktivnosti);
                        break;
                    case "6":
                        DodavanjeClana(clanoviProjekta, dk1);
                        break;
                    case "7":
                        break;
                    case "8":
                        IspisiStatistiku(clanoviProjekta, projekti, aktivnosti);
                        break;
                    case "9":
                        pokreni = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private bool PotvrdiOib(List<ClanProjekta> ulaznaListaClanova, string oib)
        {
            List<string> lOiba = new List<string>();
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                lOiba.Add(cp.Oib);
            }
            if (lOiba.Contains(oib))
            {
                Console.WriteLine("Vec postoji taj oib");
                return false;
            }
            if(oib.Length != 10)
            {
                Console.WriteLine("Oib nema 10 znamenki");
                return false;
            }
            if(!Int64.TryParse(oib, out long _))
            {
                Console.WriteLine("Oib nije pravilno unesen");
                return false;
            }
            return true;
        }

        private void DodavanjeClana(List<ClanProjekta> ulaznaListaClanova, DatotetaKlasa dk)
        {
            try
            {
                Console.WriteLine("Unesite ime clana");
                string imeClana = Console.ReadLine();
                Console.WriteLine("Unesite prezime clana");
                string prezimeClana = Console.ReadLine();
                string oib = "";
                do
                {
                    Console.WriteLine("Unesite oib");
                    oib = Console.ReadLine();
                }while(!PotvrdiOib(ulaznaListaClanova,oib ));
                Console.WriteLine("Unesite dob");
                int dob = Convert.ToInt32(Console.ReadLine());
                ClanProjekta noviClan = new ClanProjekta(new Guid(), imeClana, prezimeClana, oib, dob);
                ulaznaListaClanova.Add(noviClan);
                dk.ZapisiClanove(ulaznaListaClanova);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void IspisiListuClanova(List<ClanProjekta> ulaznaListaClanova, List<Projekt> ulaznaListaProjekta, List<Aktivnost> ulaznaListaAktivnosti)
        {
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                Console.WriteLine($"Clan projekta : {cp.Ime} {cp.Prezime} ({cp.Oib}) sudjeluju u projektima:\n");
                foreach(Projekt proj in ulaznaListaProjekta)
                {
                    if (proj.ListaIdClanova.Contains(cp.Id))
                    {
                        Console.WriteLine($"{proj.ImeProjekta}");
                    }
                }
                Console.WriteLine($"\nAktivnosti:\n");
                foreach(Aktivnost ak in ulaznaListaAktivnosti)
                {
                    if (ak.LIdClanovaProjekta.Contains(cp.Id))
                    {
                        Console.WriteLine(ak.Naziv+ "\n");
                    }
                }
            }
            Console.WriteLine();
        }

        private void IspisiStatistiku(List<ClanProjekta> ulaznaListaClanova, List<Projekt> ulaznaListaProjekta, List<Aktivnost> ulaznaListaAktivnosti)
        {
            Dictionary<string,int> clanoviPoProjektu = new Dictionary<string, int>();
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                int vrijednost = 0;
                foreach(Projekt proj in ulaznaListaProjekta)
                {
                    if (proj.ListaIdClanova.Contains(cp.Id))
                    {
                        vrijednost++;
                    }
                }
                string clan = $"{cp.Ime} {cp.Prezime} ({cp.Oib})";
                clanoviPoProjektu.Add(clan, vrijednost);
            }
            var sortedDict = from entry in clanoviPoProjektu orderby entry.Value ascending select entry;
            for(int i = 0; i < 5; i++)
            {
                KeyValuePair<string, int> kvp = clanoviPoProjektu.ElementAt(i);
                Console.WriteLine($"Clan : {kvp.Key} sudjeluje u broju projekata : {kvp.Value}");
            }
            Dictionary<string, int> clanoviPoAktivnosti = new Dictionary<string, int>();
            foreach (ClanProjekta cp in ulaznaListaClanova)
            {
                int vrijednost = 0;
                foreach (Aktivnost ak in ulaznaListaAktivnosti)
                {
                    if (ak.LIdClanovaProjekta.Contains(cp.Id))
                    {
                        vrijednost++;
                    }
                }
                string clan = $"{cp.Ime} {cp.Prezime} ({cp.Oib})";
                clanoviPoAktivnosti.Add(clan, vrijednost);
            }
            Console.WriteLine();
            var sortedDict2 = from entry in clanoviPoAktivnosti orderby entry.Value ascending select entry;
            for (int i = 0; i < 5; i++)
            {
                KeyValuePair<string, int> kvp = clanoviPoAktivnosti.ElementAt(i);
                Console.WriteLine($"Clan : {kvp.Key} sudjeluje u broju aktivnosti : {kvp.Value}");
            }
            /*var myList = clanoviPoProjektu.ToList();
            myList.Sort((a, b) => b.Value.CompareTo(a.Value));
            Console.WriteLine(myList[0]);*/

        }
        private void IspisiDetaljeOProjektu(Projekt prikazaniProjekt, List<ClanProjekta> ulaznaListaClanova, List<Aktivnost> ulaznaListaAktivnosti, List<Lokacija> ulaznaListaLokacija, List<Projekt> ulaznaListaProjekta)
        {
            Console.WriteLine();
            Console.WriteLine($"Ovo je ime projekta: {prikazaniProjekt.ImeProjekta}");
            Console.WriteLine($"Nositelj projekta je  {prikazaniProjekt.Nositelj}");
            string statusProjekta = "";
            string voditelj = "";
            if (prikazaniProjekt.Obrisan)
            {
                statusProjekta = "Neaktivan";
            }
            else
            {
                statusProjekta = "Aktivan";
            }
            Console.WriteLine("Projekt je " + statusProjekta);
            Console.WriteLine("Clanovi tog projekta su: ");
            foreach (Guid idClana in prikazaniProjekt.ListaIdClanova)
            {
                foreach (ClanProjekta cp in ulaznaListaClanova)
                {
                    if (cp.Id == idClana)
                    {
                        Console.WriteLine($"{cp.Ime} {cp.Prezime} {cp.Oib}");
                    }
                    if (cp.Id == prikazaniProjekt.IdVoditelja)
                    {
                        voditelj = $"{cp.Ime} {cp.Prezime} {cp.Oib}";
                    }
                }
            }
            Console.WriteLine("Voditelj tog projekta je " + voditelj);
            foreach (Guid idAktivnosti in prikazaniProjekt.ListaIdAktivnosti)
            {
                foreach (Aktivnost ak in ulaznaListaAktivnosti)
                {
                    if (ak.IdAktivnosti == idAktivnosti)
                    {
                        Console.WriteLine($"Naziv aktivnosti je {ak.Naziv}");
                        Console.WriteLine($"Opis aktivnosti je {ak.Opis}");
                        Console.WriteLine($"Vrijeme pocetka aktivnost je {ak.VrijemePocetka}");
                        string lokacija = "";
                        foreach (Lokacija lok in ulaznaListaLokacija)
                        {
                            if (lok.IdLokacije == ak.IdLokacije)
                            {
                                lokacija = $"{lok.Grad}, {lok.pBroj}, {lok.Adresa}, {lok.Lat}, {lok.Long}";
                            }
                        }
                        Console.WriteLine($"Lokacija Aktivnosti je {lokacija}");
                        Console.WriteLine("Clanovi koji sudjeluju na toj aktivnosti su: ");
                        foreach (Guid idClanovaAktivnosti in ak.LIdClanovaProjekta)
                        {
                            foreach (ClanProjekta cp in ulaznaListaClanova)
                            {
                                if (cp.Id == idClanovaAktivnosti)
                                {
                                    Console.WriteLine($"{cp.Ime} {cp.Prezime} {cp.Oib}");
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        public void prikaziProjekte(DatotetaKlasa dk)
        {
            List<ClanProjekta> lClanova = dk.UcitajClanove();
            List<Aktivnost> lAktivnosti = dk.UcitajAktivnosti();
            List<Lokacija> lLokacija = dk.UcitajLokacije();
            List<Projekt> lProjekta = dk.UcitajProjekte();
            int brojNeaktivnihProjekata = 0;
            int vrijednostSvihProjekata = 0;
            int vrijednostAktivnihProjekata = 0;
            var Table = new ConsoleTable("R.br", "Naziv", "Nositelj", "Vrijednost", "Status", "Voditelj", "Lokacija");
            for (int i = 0; i < lProjekta.Count; i++)
            {
                vrijednostSvihProjekata += lProjekta[i].Vrijednost;

                string status = "Aktivan";
                string lokacija = "";
                string Voditelj = "";
                foreach (ClanProjekta cp in lClanova)
                {
                    if (cp.Id == lProjekta[i].IdVoditelja)
                    {
                        Voditelj = $"{cp.Ime} {cp.Prezime}";
                    }
                }
                if (lProjekta[i].Obrisan)
                {
                    status = "Neaktivan";
                    brojNeaktivnihProjekata++;
                }
                else
                {
                    vrijednostAktivnihProjekata += lProjekta[i].Vrijednost;
                }
                foreach (Lokacija lok in lLokacija)
                {
                    if (lok.IdLokacije == lProjekta[i].IdLokacije)
                    {
                        lokacija = lok.Grad;
                    }
                }
                Table.AddRow(i + 1, lProjekta[i].ImeProjekta, lProjekta[i].Nositelj, lProjekta[i].Vrijednost, status, Voditelj, lokacija);
            }
            Table.Write();
            float postotakVrijednostiAktivnihProjekata = (float)(vrijednostAktivnihProjekata) / vrijednostSvihProjekata;
            Console.WriteLine($"Broj aktivnih projekata je {(float)(lProjekta.Count - brojNeaktivnihProjekata) / lProjekta.Count * 100}%, a vrijednost je {vrijednostAktivnihProjekata} sto je {postotakVrijednostiAktivnihProjekata * 100}%");
            Console.WriteLine($"Vrijednost svih projekata je {vrijednostSvihProjekata}");
            while (true)
            {
                Console.WriteLine("Unesite odabir za vise detalje projektu ili 0 za povratak");
                try
                {
                    int odabir = Convert.ToInt32(Console.ReadLine());
                    if (odabir == 0)
                    {
                        break;
                    }
                    else
                    {
                        IspisiDetaljeOProjektu(lProjekta[odabir - 1], lClanova, lAktivnosti, lLokacija, lProjekta);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Povratak u izbornik");


        }

        public Aktivnost DodavanjeAktivnosti()
        {
            try
            {
                Console.WriteLine("Unesite naziv aktivnosti");
                string naziv = Console.ReadLine();
                Console.WriteLine("Unesite opis aktivnosti");
                string opis = Console.ReadLine();
                DateTime vp = DateTime.Now;
                DateTime vk = DateTime.Now;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Aktivnost();
        }
        public void DodavanjeProjekta(DatotetaKlasa dk)
        {
            List<ClanProjekta> listaClanovaProjekta = dk.UcitajClanove();
            Console.WriteLine("Unesite ime projekta");
            try
            {
                string imeProjekta = Console.ReadLine();
                List<Guid> listaClanova = new List<Guid>();
                List<Guid> listaAktivnosti = new List<Guid>();
                while (true)
                {
                    Console.WriteLine("Odaberite clanove za projekt. Unesite 0 za povratak");
                    for (int i = 0; i < listaClanovaProjekta.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {listaClanovaProjekta[i].Ime} {listaClanovaProjekta[i].Prezime} ({listaClanovaProjekta[i].Oib})");
                    }
                    int odabir = Convert.ToInt32(Console.ReadLine());
                    if (odabir == 0)
                    {
                        break;
                    }
                    else
                    {
                        listaClanova.Add(listaClanovaProjekta[odabir + 1].Id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
