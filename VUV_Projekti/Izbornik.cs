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
                dk1.ZapisiClanove(clanoviProjekta);
                dk1.ZapisiAktivnosti(aktivnosti);
                dk1.ZapisiProjekte(projekti);
                clanoviProjekta = dk1.UcitajClanove();
                aktivnosti = dk1.UcitajAktivnosti();
                projekti = dk1.UcitajProjekte();
                lokacije = dk1.UcitajLokacije();
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
                        DodavanjeProjekta(dk1, projekti, clanoviProjekta, aktivnosti, lokacije);
                        break;
                    case "3":
                        AzuriranjeProjekata(projekti,clanoviProjekta,dk1);
                        break;
                    case "4":
                        DodavanjeAktivnosti(clanoviProjekta, projekti, aktivnosti, lokacije, dk1);
                        break;
                    case "5":
                        IspisiListuClanova(clanoviProjekta, projekti, aktivnosti);
                        break;
                    case "6":
                        DodavanjeClana(clanoviProjekta, dk1);
                        break;
                    case "7":
                        IzbrisiClana(clanoviProjekta, projekti, dk1);
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
       
        private void IzbrisiClana(List<ClanProjekta> ulaznaListaClanova, List<Projekt> ulaznaListaProjekata, DatotetaKlasa dk) 
        {
            List<ClanProjekta> listaAktivnihClanova = new List<ClanProjekta>();
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                if (!cp.Obrisan)
                {
                    listaAktivnihClanova.Add(cp);
                }
            }
            for(int i = 0; i < listaAktivnihClanova.Count; i++)
            {
                Console.WriteLine($"{i+1}. {listaAktivnihClanova[i].Ime} {listaAktivnihClanova[i].Prezime} ({listaAktivnihClanova[i].Oib})");
            }
            Console.WriteLine("Unesite odabir kojeg clana zelite izbrisati");
            int odabir = Convert.ToInt32(Console.ReadLine()) -1;

            foreach(Projekt proj in ulaznaListaProjekata)
            {
                if (proj.ListaIdClanova.Contains(listaAktivnihClanova[odabir].Id))
                {
                    if (proj.ListaIdClanova.Count == 1)
                    {
                        proj.ListaIdClanova.Remove(listaAktivnihClanova[odabir].Id);
                        proj.Obrisan = true;
                        Console.WriteLine($"Projekt: {proj.ImeProjekta} postaje neaktivan");
                    }
                }
            }
            foreach (ClanProjekta cp in ulaznaListaClanova)
            {
                if(cp.Id == listaAktivnihClanova[odabir].Id)
                {
                    cp.Obrisan = true;
                    Console.WriteLine($"Izbrisali smo clana {cp.Ime} {cp.Prezime} ({cp.Oib})");
                }
            }
            dk.ZapisiClanove(ulaznaListaClanova);
            dk.ZapisiProjekte(ulaznaListaProjekata);
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

        private bool CheckString(string unos)
        {
            return unos.Any(ch => !char.IsLetterOrDigit(ch)) || unos.Any(ch => char.IsDigit(ch));
        }

        private ClanProjekta DodavanjeClana(List<ClanProjekta> ulaznaListaClanova, DatotetaKlasa dk)
        {
            string imeClana = "";
            string prezimeClana = "";
            try
            {
                while (true)
                {
                    Console.WriteLine("Unesite ime clana");
                    imeClana = Console.ReadLine();
                    while (CheckString(imeClana))
                    {
                        Console.WriteLine("Krivo ste unijeli ime clana. Ponovite unos");
                        imeClana = Console.ReadLine();
                    }
                    Console.WriteLine("Unesite prezime clana");
                    prezimeClana = Console.ReadLine();
                    string oib = "";
                    do
                    {
                        Console.WriteLine("Unesite oib");
                        oib = Console.ReadLine();
                    } while (!PotvrdiOib(ulaznaListaClanova, oib));
                    Console.WriteLine("Unesite dob");
                    string dobString = Console.ReadLine(); //Dodati da pregleda string
                    DateTime dob = DateTime.Parse(dobString);
                    ClanProjekta noviClan = new ClanProjekta(new Guid(), imeClana, prezimeClana, oib, dob);
                    ulaznaListaClanova.Add(noviClan);
                    dk.ZapisiClanove(ulaznaListaClanova);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new ClanProjekta();
        }
        private void IspisiListuClanova(List<ClanProjekta> ulaznaListaClanova, List<Projekt> ulaznaListaProjekta, List<Aktivnost> ulaznaListaAktivnosti)
        {
            var Table = new ConsoleTable("Ime clana", "Projekti", "Aktivnosti");
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                if (!cp.Obrisan)
                {
                    int br = 0;
                    string imenaProjekata = "";
                    foreach (Projekt proj in ulaznaListaProjekta)
                    {
                        if (proj.ListaIdClanova.Contains(cp.Id))
                        {
                            if (br == 0)
                            {
                                imenaProjekata += $"{proj.ImeProjekta}";
                            }
                            else
                            {
                                imenaProjekata += $", {proj.ImeProjekta}";
                            }
                            br++;
                        }
                    }


                    int br2 = 0;
                    string imenaAktivnosti = "";
                    foreach (Aktivnost ak in ulaznaListaAktivnosti)
                    {
                        if (ak.LIdClanovaProjekta.Contains(cp.Id))
                        {
                            if (br2 == 0)
                            {
                                imenaAktivnosti += ak.Naziv;
                            }
                            else
                            {
                                imenaAktivnosti += $", {ak.Naziv}";
                            }
                            br2++;
                        }
                    }
                    Table.AddRow($"{cp.Ime} {cp.Prezime} ({cp.Oib})", imenaProjekata, imenaAktivnosti);
                }
               
            }
            Table.Write();
            Console.WriteLine();
        }

        private void IspisiStatistiku(List<ClanProjekta> ulaznaListaClanova, List<Projekt> ulaznaListaProjekta, List<Aktivnost> ulaznaListaAktivnosti)
        {
            Dictionary<string,int> clanoviPoProjektu = new Dictionary<string, int>();
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                if (!cp.Obrisan)
                {
                    int vrijednost = 0;
                    foreach (Projekt proj in ulaznaListaProjekta)
                    {
                        if (proj.ListaIdClanova.Contains(cp.Id))
                        {
                            vrijednost++;
                        }
                    }
                    string clan = $"{cp.Ime} {cp.Prezime} ({cp.Oib})";
                    clanoviPoProjektu.Add(clan, vrijednost);
                }
            }
            var sortedDict = from entry in clanoviPoProjektu orderby entry.Value ascending select entry;
            Console.WriteLine("Clanovi po broju projekta");
            var TablePoProjektu = new ConsoleTable("R.Br.", "Ime Clana (OIB)", "Broj projekata");
            for(int i = 0; i < 5; i++)
            {
                KeyValuePair<string, int> kvp = clanoviPoProjektu.ElementAt(i);
                TablePoProjektu.AddRow(i + 1, kvp.Key, kvp.Value);
            }
            TablePoProjektu.Write();
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
            Console.WriteLine("Clanovi po broju aktivnosti");
            var TablePoAktivnosti = new ConsoleTable("R.Br.", "Ime Clana (oib)", "Broj Aktivnosti");
            var sortedDict2 = from entry in clanoviPoAktivnosti orderby entry.Value ascending select entry;
            for (int i = 0; i < 5; i++)
            {
                KeyValuePair<string, int> kvp = clanoviPoAktivnosti.ElementAt(i);
                TablePoAktivnosti.AddRow(i + 1, kvp.Key, kvp.Value);
            }
            TablePoAktivnosti.Write();
            /*var myList = clanoviPoProjektu.ToList();
            myList.Sort((a, b) => b.Value.CompareTo(a.Value));
            Console.WriteLine(myList[0]);*/
            Dictionary<string, double> projektiPoVrijednosti = new Dictionary<string, double>();
            Console.WriteLine("Projekti po vrijednosti");
            var TableProjVrijednost = new ConsoleTable("R.Br.", "Ime projekta", "Vrijednost");
            foreach(Projekt proj in ulaznaListaProjekta)
            {
                projektiPoVrijednosti.Add(proj.ImeProjekta, proj.Vrijednost);
            }
            var sortedDict3 = from entry in projektiPoVrijednosti orderby entry.Value ascending select entry;
            Console.WriteLine();
            for(int i = 0; i < 5; i++)
            {
                KeyValuePair<string, double> kvp = projektiPoVrijednosti.ElementAt(i);
                TableProjVrijednost.AddRow(i + 1, kvp.Key, kvp.Value);
            }
            TableProjVrijednost.Write();
            Console.WriteLine();
            Console.WriteLine("Clanovi po postotku projekta");
            var TableClanProjekt = new ConsoleTable("Ime Clana (oib)", "Broj Projekata", "Postotak projekta");
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                int brojProjekata = 0;
                foreach(Projekt proj in ulaznaListaProjekta)
                {
                    if (proj.ListaIdClanova.Contains(cp.Id))
                    {
                        brojProjekata++;
                    }
                }
                TableClanProjekt.AddRow($"{cp.Ime} {cp.Prezime} ({cp.Oib})", brojProjekata, $"{(float)brojProjekata / ulaznaListaProjekta.Count * 100}%");
            }
            TableClanProjekt.Write();
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
            var tableAktivnost = new ConsoleTable("Ime Aktivnosti", "Opis Aktivnosti", "Vrijeme Pocetka", "Lokacija", "Clanovi");
            foreach (Guid idAktivnosti in prikazaniProjekt.ListaIdAktivnosti)
            {
                foreach (Aktivnost ak in ulaznaListaAktivnosti)
                {
                    if (ak.IdAktivnosti == idAktivnosti)
                    {
                        string lokacija = "";
                        foreach (Lokacija lok in ulaznaListaLokacija)
                        {
                            if (lok.IdLokacije == ak.IdLokacije)
                            {
                                lokacija = $"{lok.Grad}, {lok.pBroj}, {lok.Adresa}, {lok.Lat}, {lok.Long}";
                            }
                        }
                        string clanovi = "";
                        int br2 = 0;
                        foreach (Guid idClanovaAktivnosti in ak.LIdClanovaProjekta)
                        {
                            foreach (ClanProjekta cp in ulaznaListaClanova)
                            {
                                if (cp.Id == idClanovaAktivnosti)
                                {
                                    if (br2 == 0)
                                    {
                                        clanovi += $"{cp.Ime} {cp.Prezime} {cp.Oib}";
                                    }
                                    else
                                    {
                                        clanovi += $", {cp.Ime} {cp.Prezime} {cp.Oib}";
                                    }
                                    br2++;
                                }
                            }
                        }
                        tableAktivnost.AddRow(ak.Naziv, ak.Opis, ak.VrijemePocetka, lokacija, clanovi);
                    }
                }
            }
            tableAktivnost.Write();
            Console.WriteLine();
        }

        public void prikaziProjekte(DatotetaKlasa dk)
        {
            List<ClanProjekta> lClanova = dk.UcitajClanove();
            List<Aktivnost> lAktivnosti = dk.UcitajAktivnosti();
            List<Lokacija> lLokacija = dk.UcitajLokacije();
            List<Projekt> lProjekta = dk.UcitajProjekte();
            int brojNeaktivnihProjekata = 0;
            double vrijednostSvihProjekata = 0;
            double vrijednostAktivnihProjekata = 0;
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
            double postotakVrijednostiAktivnihProjekata = (float)(vrijednostAktivnihProjekata) / vrijednostSvihProjekata;
            Console.WriteLine($"Broj aktivnih projekata je {(float)(lProjekta.Count - brojNeaktivnihProjekata) / lProjekta.Count * 100}%, a vrijednost je {vrijednostAktivnihProjekata} sto je {postotakVrijednostiAktivnihProjekata * 100}%");
            Console.WriteLine($"Vrijednost svih projekata je {vrijednostSvihProjekata}");
            while (true)
            {
                Console.WriteLine("Unesite odabir za vise detalje projektu ili 0 za povratak");
                try
                {
                    int odabir = Convert.ToInt32(Console.ReadLine());
                    if(odabir < 0)
                    {
                        Console.WriteLine("Odabir ne smije biti negativan broj. Ponovite Unos");
                    }else if(odabir > lProjekta.Count)
                    {
                        Console.WriteLine("Odabir ne smije biti veci od broja elemenata. Ponovite Unos");
                    }
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
                    
                }
            }
            Console.WriteLine("Povratak u izbornik");


        }

        private Lokacija BiranjeLokacije(List<Lokacija> ulaznaListaLokacija)
        {
            for(int i = 0; i < ulaznaListaLokacija.Count; i++)
            {
                Console.WriteLine($"{i+1}. {ulaznaListaLokacija[i].Adresa} {ulaznaListaLokacija[i].Grad} {ulaznaListaLokacija[i].pBroj}");
            }
            int odabir = Convert.ToInt32(Console.ReadLine())-1;
            return ulaznaListaLokacija[odabir];
        }

        private Guid BiranjeProjekta(List<Projekt> ulaznaListaProjekata)
        {
            for(int i = 0; i < ulaznaListaProjekata.Count; i++)
            {
                Console.WriteLine($"{i+1}. {ulaznaListaProjekata[i].ImeProjekta}");
            }
            int odabir = Convert.ToInt32(Console.ReadLine()) - 1;

            return ulaznaListaProjekata[odabir].IdProjekta;
        }

        

        private List<Guid> BiranjeClanova(List<ClanProjekta> ulaznaListaClanova, DatotetaKlasa dk)
        {
            List<Guid> lDodanihClanova = new List<Guid>();
            bool vrti = true;
            while (vrti)
            {
                Console.WriteLine("1. Za dodavanje postojecih clanova\n2. Za dodavanje novih clanova\n3. Za izlaz");
                string odabirSwitch = Console.ReadLine();
                
                switch (odabirSwitch)
                {
                    case "1":
                        List<ClanProjekta> lAktivnihClanova = new List<ClanProjekta>();
                        for (int i = 0; i < ulaznaListaClanova.Count; i++)
                        {
                            if (lAktivnihClanova.Contains(ulaznaListaClanova[i]))
                            {
                                Console.WriteLine("Sadrzi ovog clana");
                            }
                            if (!ulaznaListaClanova[i].Obrisan && !lDodanihClanova.Contains(ulaznaListaClanova[i].Id))
                            {
                                lAktivnihClanova.Add(ulaznaListaClanova[i]);
                            }
                        }
                        for(int i = 0; i < lAktivnihClanova.Count; i++)
                        {
                            Console.WriteLine($"{i+1}. {lAktivnihClanova[i].Ime} {lAktivnihClanova[i].Prezime} ({lAktivnihClanova[i].Oib})");
                        }
                        string odabir = "";
                        while (true)
                        {
                            odabir = Console.ReadLine();
                            if(!Int32.TryParse(odabir, out int _))
                            {
                                Console.WriteLine("Pogresan unos. Morate unesti broj");
                            }
                            else if(Convert.ToInt32(odabir)-1 < 0)
                            {
                                Console.WriteLine("Odabir clana ne smije biti manji od nula. Ponovite unos");
                            }
                            else if(Convert.ToInt32(odabir) - 1 >= lAktivnihClanova.Count)
                            {
                                Console.WriteLine("Odabir clana ne smije biti veci od " + lAktivnihClanova.Count + ". Ponovite unos");
                            }
                            else
                            {
                                break;
                            }
                        }
                        int odabirClana = Convert.ToInt32(odabir)-1;
                        lDodanihClanova.Add(lAktivnihClanova[odabirClana].Id);
                        break;
                    case "2":
                        lDodanihClanova.Add(DodavanjeClana(ulaznaListaClanova, dk).Id);
                        break;
                    case "3":
                        vrti = false;
                        break;
                    default:
                        Console.WriteLine("Doslo je do pogreske pri odabiru");
                        break;
                }
            }
            return lDodanihClanova;
        }

        public Aktivnost DodavanjeAktivnosti(List<ClanProjekta> ulaznaListaClanova, List<Projekt> ulaznaListaProjekata, List<Aktivnost> ulaznaListaAktivnosti, List<Lokacija> ulaznaListaLokacija, DatotetaKlasa dk)
        {
            try
            {
                Console.WriteLine("Unesite naziv aktivnosti");
                string naziv = Console.ReadLine();
                Console.WriteLine("Unesite opis aktivnosti");
                string opis = Console.ReadLine();
                DateTime vp = DateTime.Now;
                DateTime vk = DateTime.Now;
                Lokacija lokacijaAktivnosti = BiranjeLokacije(ulaznaListaLokacija);
                Guid idProjekta = BiranjeProjekta(ulaznaListaProjekata);
                List<Guid> lIdClanova = BiranjeClanova(ulaznaListaClanova, dk);
                Aktivnost a = new Aktivnost(Guid.NewGuid(), naziv, opis, vp, vk, lokacijaAktivnosti, null, lokacijaAktivnosti.IdLokacije, lIdClanova, idProjekta);
                ulaznaListaAktivnosti.Add(a);
                dk.ZapisiAktivnosti(ulaznaListaAktivnosti);
                return a;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Aktivnost();
        }

        public string OdabirNositelja (List<ClanProjekta> ulaznaListaClanova)
        {
            Console.WriteLine("Odabir Nositelja");
            string nositelj = "";
            try
            {
                for(int i = 0; i < ulaznaListaClanova.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {ulaznaListaClanova[i].Ime} {ulaznaListaClanova[i].Prezime}");
                }
                Console.WriteLine("Unesite odabir");
                int odabir = Convert.ToInt32(Console.ReadLine()) -1;
                nositelj = $"{ulaznaListaClanova[odabir].Ime} {ulaznaListaClanova[odabir].Prezime}";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return nositelj;
        }
        private Guid OdabirVoditelja (List<ClanProjekta> ulaznaListaClanova)
        {
            Console.WriteLine("Odabir Voditelja");
            Guid idVoditelja = new Guid();
            try
            {
                for (int i = 0; i < ulaznaListaClanova.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ulaznaListaClanova[i].Ime} {ulaznaListaClanova[i].Prezime}");
                }
                Console.WriteLine("Unesite vas odabir");
                int odabir = Convert.ToInt32(Console.ReadLine())-1;
                idVoditelja = ulaznaListaClanova[odabir].Id;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return idVoditelja;
        }
        private Lokacija DodavanjeLokacije(DatotetaKlasa dk, List<Lokacija> ulaznaListaLokacija)
        {
            Lokacija trenutacnaLokacije = new Lokacija();
            bool vrti = true;
            while (vrti)
            {
                Console.WriteLine("1. Biranje postojecih lokacija\n2. Dodavanje Nove lokacije\n3. Izlaz");
                string odabir = Console.ReadLine();
                switch (odabir)
                {
                    case "1":
                        trenutacnaLokacije = BiranjeLokacije(ulaznaListaLokacija);
                        vrti = false;
                        break;
                    case "2":
                        Console.WriteLine("Unesite adresu");
                        string adresa = Console.ReadLine();
                        Console.WriteLine("Unesite postanski broj");
                        string postanskiBroj = Console.ReadLine();
                        Console.WriteLine("Unesite grad");
                        string grad = Console.ReadLine();
                        Console.WriteLine("Unesite latitudu");
                        string lat = Console.ReadLine();
                        Console.WriteLine("Unesite longitudu");
                        string longituda = Console.ReadLine();
                        trenutacnaLokacije = new Lokacija(Guid.NewGuid(), adresa, postanskiBroj, grad, lat, longituda);
                        vrti = false;
                        break;
                    case "3":
                        vrti = false;
                        break;
                    default:
                        Console.WriteLine("Pogresan unos");
                        break;
                }
            }
            return trenutacnaLokacije;
        }
        private void DodavanjeProjekta( DatotetaKlasa dk, List<Projekt> ulaznaListaProjekata, List<ClanProjekta> ulaznaListaClanova, List<Aktivnost> ulaznaListaAktivnosti, List<Lokacija> ulaznaListaLokacija)
        {
            List<ClanProjekta> listaClanovaProjektaUcitana = dk.UcitajClanove();
            while (true)
            {
                try
                {
                    Console.WriteLine("Unesite ime projekta");
                    string imeProjekta = Console.ReadLine();
                    while (CheckString(imeProjekta))
                    {
                        Console.WriteLine("Krivo ste unijeli ime projekta. Ime projekta ne smije sadrzavati brojeve i posebne znakove. Ponovite Unos.");
                        imeProjekta = Console.ReadLine();
                    }
                    List<Guid> listaAktivnosti = new List<Guid>();
                    List<Guid> listaClanova = BiranjeClanova(ulaznaListaClanova, dk);
                    List<ClanProjekta> ListaClanovaProjekta = new List<ClanProjekta>();
                    foreach (ClanProjekta cp in ulaznaListaClanova)
                    {
                        if (listaClanova.Contains(cp.Id))
                        {
                            ListaClanovaProjekta.Add(cp);
                        }
                    }
                    Lokacija lokacijaProjekta = DodavanjeLokacije(dk,ulaznaListaLokacija);
                    List<Lokacija> listaLokacijaProjekta = new List<Lokacija>() { lokacijaProjekta };

                    Console.WriteLine("Unesite vrijednost projekta");
                    string vrijednostProjektaString = Console.ReadLine();
                    long vrijednostProjekta = 0;
                    while (!Int64.TryParse(vrijednostProjektaString, out long _))
                    {
                        Console.WriteLine("Pogresan unos vrijednosti. Ponovite unos");
                        vrijednostProjektaString = Console.ReadLine();
                    }
                    vrijednostProjekta = Convert.ToInt64(vrijednostProjektaString);
                    string nositelj = OdabirNositelja(ListaClanovaProjekta);

                    Projekt projekt = new Projekt(Guid.NewGuid(), imeProjekta, listaAktivnosti, listaClanova, OdabirVoditelja(ListaClanovaProjekta), nositelj, lokacijaProjekta.IdLokacije, vrijednostProjekta);
                    List<Projekt> lProjektSam = new List<Projekt>() { projekt };
                    Console.WriteLine("Dodavanje aktivnosti");
                    Aktivnost a = DodavanjeAktivnosti(ListaClanovaProjekta, lProjektSam, ulaznaListaAktivnosti, listaLokacijaProjekta, dk);
                    listaAktivnosti.Add(a.IdAktivnosti);
                    ulaznaListaProjekata.Add(projekt);
                    dk.ZapisiProjekte(ulaznaListaProjekata);
                }
                catch (Exception e)
                {
                }
            }
            
        }

        private void AzuriranjeProjekata(List<Projekt> ulaznaListaProjekata,List<ClanProjekta> ulaznaListaClanova,DatotetaKlasa dk)
        {
            for(int i = 0; i < ulaznaListaProjekata.Count; i++)
            {
                Console.WriteLine($"{i+1}. {ulaznaListaProjekata[i].ImeProjekta}");
            }
            Console.WriteLine("Odaberite projekt koji zelite azurirati");
            try
            {
                int odabir = Convert.ToInt32(Console.ReadLine())-1;
                Console.WriteLine($"Azuriramo projekt {ulaznaListaProjekata[odabir].ImeProjekta}");
                Console.WriteLine("1. Promjena Nositelja\n2. Promjena Voditelja \n3. Promjena Vrijednosti");
                string odabirAzuriranja = Console.ReadLine();
                switch (odabirAzuriranja)
                {
                    case "1":
                        ulaznaListaProjekata[odabir].PromjeniNositelja(ulaznaListaClanova);
                        break;
                    case "2":
                        ulaznaListaProjekata[odabir].PromjenaVoditelja(ulaznaListaClanova);
                        break;
                    case "3":
                        ulaznaListaProjekata[odabir].PromjeniVrijednost();
                        break;
                    default:
                        Console.WriteLine("Pogreska");
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
