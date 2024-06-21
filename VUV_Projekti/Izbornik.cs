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
                        DodavanjeProjekta(dk1, projekti, clanoviProjekta, aktivnosti, lokacije); //TODO
                        break;
                    case "3":
                        AzuriranjeProjekata(); //TODO
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
                    proj.ListaIdClanova.Remove(listaAktivnihClanova[odabir].Id);
                    if (proj.ListaIdClanova.Count == 1)
                    {
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
            Dictionary<string, int> projektiPoVrijednosti = new Dictionary<string, int>();

            foreach(Projekt proj in ulaznaListaProjekta)
            {
                projektiPoVrijednosti.Add(proj.ImeProjekta, proj.Vrijednost);
            }
            var sortedDict3 = from entry in projektiPoVrijednosti orderby entry.Value ascending select entry;
            Console.WriteLine();
            for(int i = 0; i < 5; i++)
            {
                KeyValuePair<string, int> kvp = projektiPoVrijednosti.ElementAt(i);
                Console.WriteLine($"{i+1}. {kvp.Key} ima vrijednost {kvp.Value}");
            }
            Console.WriteLine();
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
                Console.WriteLine($"{cp.Ime} {cp.Prezime} ({cp.Oib}) sudjeluje u {brojProjekata} sto je {(float)brojProjekata/ulaznaListaProjekta.Count*100}% od ukupnog broja projekata");
            }
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

        private List<Guid> BiranjeClanova(List<ClanProjekta> ulaznaListaClanova)
        {
            List<Guid> lDodanihClanova = new List<Guid>();
            bool vrti = true;
            while (vrti)
            {
                Console.WriteLine("1. Za dodavanje jos clanova\n2. za izlaz");
                string odabirSwitch = Console.ReadLine();
                
                switch (odabirSwitch)
                {
                    case "1":
                        Console.WriteLine("Sad smo u case 1");
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
                        int odabirClana = Convert.ToInt32(Console.ReadLine())-1;
                        lDodanihClanova.Add(lAktivnihClanova[odabirClana].Id);
                        break;
                    case "2":
                        vrti = false;
                        break;
                    default:
                        Console.WriteLine("Doslo je do pogreske");
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
                List<Guid> lIdClanova = BiranjeClanova(ulaznaListaClanova);
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

        public string OdabirNositelja ()
        {
            string nositelj = "";
            try
            {
                Console.WriteLine("Unesite odabir nositelja");
                string odabir = Console.ReadLine();
                switch (odabir)
                {
                    case "1":
                        nositelj = "Marko Maric";
                        break;
                    case "2":
                        nositelj = "Ana Anic";
                        break;
                    case "3":
                        nositelj = "Luka Lukic";
                        break;
                    case "4":
                        nositelj = "Iva Ivic";
                        break;
                    case "5":
                        nositelj = "Tomo Tomic";
                        break;
                    default:
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return nositelj;
        }
        private void DodavanjeProjekta( DatotetaKlasa dk, List<Projekt> ulaznaListaProjekata, List<ClanProjekta> ulaznaListaClanova, List<Aktivnost> ulaznaListaAktivnosti, List<Lokacija> ulaznaListaLokacija)
        {
            List<ClanProjekta> listaClanovaProjekta = dk.UcitajClanove();
            Console.WriteLine("Unesite ime projekta");
            try
            {
                string imeProjekta = Console.ReadLine();
                List<Guid> listaAktivnosti = new List<Guid>();
                List<Guid> listaClanova = BiranjeClanova(ulaznaListaClanova);
                Lokacija lokacijaProjekta = BiranjeLokacije(ulaznaListaLokacija);
                Aktivnost a = DodavanjeAktivnosti(ulaznaListaClanova, ulaznaListaProjekata, ulaznaListaAktivnosti, ulaznaListaLokacija, dk);
                listaAktivnosti.Add(a.IdAktivnosti);
                //dodati odaberi voditelja
                Console.WriteLine("Unesite vrijednost projekta");
                int vrijednostProjekta = Convert.ToInt32(Console.ReadLine());
                Projekt projekt = new Projekt(Guid.NewGuid(), imeProjekta, listaAktivnosti, listaClanova, listaClanova[0], OdabirNositelja(), lokacijaProjekta.IdLokacije,vrijednostProjekta);
                ulaznaListaProjekata.Add(projekt);
                dk.ZapisiProjekte(ulaznaListaProjekata);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void AzuriranjeProjekata()
        {

        }
    }
}
