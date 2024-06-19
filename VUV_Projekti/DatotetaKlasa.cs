using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using ConsoleTables;

namespace VUV_Projekti
{
    class DatotetaKlasa
    {
        private List<ClanProjekta> listaClanovaProjekta;
        private List<Aktivnost> listaAktivnosti;
        private List<Projekt> listaProjekta;
        private List<Lokacija> listaLokacija;
        public List<ClanProjekta> UcitajClanove()
        {
            string xml = "";
            using(StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\ClanoviProjekta.xml"))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<ClanProjekta> clanoviProjekta = new List<ClanProjekta>();

            XmlNodeList clanovi = xmlObject.SelectNodes("//Clanovi/ClanProjekta");
            foreach(XmlNode clanProjekta in clanovi)
            {
                clanoviProjekta.Add(new ClanProjekta(
                    new Guid(clanProjekta.Attributes["_id"].Value),
                    clanProjekta.Attributes["_ime"].Value,
                    clanProjekta.Attributes["_prezime"].Value,
                    clanProjekta.Attributes["_oib"].Value,
                    Convert.ToInt32(clanProjekta.Attributes["_dob"].Value)
                    ));
            }

            listaClanovaProjekta = clanoviProjekta;
            return clanoviProjekta;
        }

        public void ZapisiClanove(List<ClanProjekta> clanovi)
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\ClanoviProjekta.xml"))
            {
                xml = sr.ReadToEnd();
            }
            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);
            XmlNode projektNode = xmlObject.SelectSingleNode("//Clanovi");
            projektNode.RemoveAll();

            foreach (ClanProjekta cp in clanovi)
            {
                XmlNode noviNode = xmlObject.CreateNode(XmlNodeType.Element, "ClanProjekta", null);
                XmlAttribute idAttr = xmlObject.CreateAttribute("_id");
                idAttr.Value = cp.Id.ToString();
                noviNode.Attributes.Append(idAttr);
                XmlAttribute imeAttr = xmlObject.CreateAttribute("_ime");
                imeAttr.Value = cp.Ime;
                noviNode.Attributes.Append(imeAttr);
                XmlAttribute prezimeAttr = xmlObject.CreateAttribute("_prezime");
                prezimeAttr.Value = cp.Prezime;
                noviNode.Attributes.Append(prezimeAttr);
                XmlAttribute OibAttr = xmlObject.CreateAttribute("_oib");
                OibAttr.Value = cp.Oib;
                noviNode.Attributes.Append(OibAttr);
                XmlAttribute IzbrisanAttr = xmlObject.CreateAttribute("_obrisan");
                IzbrisanAttr.Value = cp.Obrisan.ToString();
                noviNode.Attributes.Append(IzbrisanAttr);
                XmlAttribute dobAttr = xmlObject.CreateAttribute("_dob");
                dobAttr.Value = cp.Dob.ToString();
                noviNode.Attributes.Append(dobAttr);

                projektNode.AppendChild(noviNode);
            }

            xmlObject.Save(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\ClanoviProjekta.xml");
        }

        public List<Aktivnost> UcitajAktivnosti()
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Aktivnosti.xml"))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<Aktivnost> lAktivnosti= new List<Aktivnost>();

            XmlNodeList aktivnosti = xmlObject.SelectNodes("//Aktivnosti/Aktivnost");
            foreach (XmlNode ak in aktivnosti)
            {
                List<Guid> listaId = new List<Guid>();
                for(XmlNode i = ak.FirstChild.FirstChild; i!=null;i = i.NextSibling)
                {
                    listaId.Add(new Guid(i.Attributes["_id"].Value));
                }
                lAktivnosti.Add(new Aktivnost(new Guid(ak.Attributes["_id"].Value),
                    ak.Attributes["_naziv"].Value,
                    ak.Attributes["_opis"].Value,
                    DateTime.Parse(ak.Attributes["_vp"].Value),
                    DateTime.Parse(ak.Attributes["_vk"].Value),
                    null,
                    null,
                    new Guid(ak.Attributes["_idLokacije"].Value),
                    listaId));
            }

            listaAktivnosti = lAktivnosti;
            return lAktivnosti;
        }

       

        public void ZapisiAktivnosti(List<Aktivnost> lAktivnosti)
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Aktivnosti.xml"))
            {
                xml = sr.ReadToEnd();
            }
            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);
            XmlNode projektNode = xmlObject.SelectSingleNode("//Aktivnosti");
            projektNode.RemoveAll();

            foreach (Aktivnost ak in lAktivnosti)
            {
                XmlNode noviNode = xmlObject.CreateNode(XmlNodeType.Element, "Aktivnost", null);
                XmlAttribute idAttr = xmlObject.CreateAttribute("_id");
                idAttr.Value = ak.IdAktivnosti.ToString();
                noviNode.Attributes.Append(idAttr);
                XmlAttribute nazivAttr = xmlObject.CreateAttribute("_naziv");
                nazivAttr.Value = ak.Naziv;
                noviNode.Attributes.Append(nazivAttr);
                XmlAttribute opisAttr = xmlObject.CreateAttribute("_opis");
                opisAttr.Value = ak.Opis;
                noviNode.Attributes.Append(opisAttr);
                XmlAttribute vpAttr = xmlObject.CreateAttribute("_vp");
                vpAttr.Value = ak.VrijemePocetka.ToString();
                noviNode.Attributes.Append(vpAttr);
                XmlAttribute vkAttr = xmlObject.CreateAttribute("_vk");
                vkAttr.Value = ak.VrijemeKraja.ToString();
                noviNode.Attributes.Append(vkAttr);
                XmlAttribute LokacijaAttr = xmlObject.CreateAttribute("_idLokacije");
                LokacijaAttr.Value = ak.IdLokacije.ToString();
                noviNode.Attributes.Append(LokacijaAttr);
                XmlNode clanoviNode = xmlObject.CreateNode(XmlNodeType.Element, "Clanovi", null);
                foreach (Guid cp in ak.LIdClanovaProjekta)
                {
                    XmlNode noviNode2 = xmlObject.CreateNode(XmlNodeType.Element, "Clan", null);
                    XmlAttribute idAttr2 = xmlObject.CreateAttribute("_id");
                    idAttr2.Value = cp.ToString();
                    noviNode2.Attributes.Append(idAttr2);

                    clanoviNode.AppendChild(noviNode2);
                }
                noviNode.AppendChild(clanoviNode);
                projektNode.AppendChild(noviNode);
            }

            xmlObject.Save(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Aktivnosti.xml");
        }

        public List<Projekt> UcitajProjekte()
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Projekti.xml"))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<Projekt> lProjekta = new List<Projekt>();

            XmlNodeList projekti= xmlObject.SelectNodes("//Projekti/Projekt");
            foreach (XmlNode p in projekti)
            {
                List<Guid> listaIdClanova = new List<Guid>();
                List<Guid> listaIdAktivnosti = new List<Guid>();
                for (XmlNode i = p.FirstChild.FirstChild; i != null; i = i.NextSibling)
                {
                    listaIdClanova.Add(new Guid(i.Attributes["_id"].Value));
                }
                for(XmlNode i = p.LastChild.FirstChild; i != null; i = i.NextSibling)
                {
                    listaIdAktivnosti.Add(new Guid(i.Attributes["_id"].Value));
                }
                lProjekta.Add(new Projekt(new Guid(p.Attributes["_id"].Value),
                    p.Attributes["_ime"].Value,
                    listaIdAktivnosti,
                    listaIdClanova,
                    new Guid(p.Attributes["_idVoditelja"].Value),
                    p.Attributes["_nositelj"].Value,
                    new Guid(p.Attributes["_idLokacije"].Value),
                    Convert.ToInt32(p.Attributes["_vrijednost"].Value),
                    bool.Parse(p.Attributes["_obrisan"].Value)
                    ));
            }

           
            listaProjekta = lProjekta;
            return lProjekta;
        }

        public void ZapisiProjekte(List<Projekt> lProjekta)
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Projekti.xml"))
            {
                xml = sr.ReadToEnd();
            }
            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);
            XmlNode projektNode = xmlObject.SelectSingleNode("//Projekti");
            projektNode.RemoveAll();

            foreach (Projekt p in lProjekta)
            {
                XmlNode noviNode = xmlObject.CreateNode(XmlNodeType.Element, "Projekt", null);
                XmlAttribute idAttr = xmlObject.CreateAttribute("_id");
                idAttr.Value = p.IdProjekta.ToString();
                noviNode.Attributes.Append(idAttr);
                XmlAttribute imeAttr = xmlObject.CreateAttribute("_ime");
                imeAttr.Value = p.ImeProjekta;
                noviNode.Attributes.Append(imeAttr);
                XmlNode clanoviNode = xmlObject.CreateNode(XmlNodeType.Element, "Clanovi", null);
                foreach (Guid cp in p.ListaIdClanova)
                {
                    XmlNode noviNode2 = xmlObject.CreateNode(XmlNodeType.Element, "Clan", null);
                    XmlAttribute idAttr2 = xmlObject.CreateAttribute("_id");
                    idAttr2.Value = cp.ToString();
                    noviNode2.Attributes.Append(idAttr2);

                    clanoviNode.AppendChild(noviNode2);
                }
                XmlNode AktivnostiNode = xmlObject.CreateNode(XmlNodeType.Element, "Aktivnosti", null);
                foreach (Guid a in p.ListaIdAktivnosti)
                {
                    XmlNode aktivnost = xmlObject.CreateNode(XmlNodeType.Element, "Aktivnost", null);
                    XmlAttribute idAktivnosti = xmlObject.CreateAttribute("_id");
                    idAktivnosti.Value = a.ToString();
                    aktivnost.Attributes.Append(idAktivnosti);

                    AktivnostiNode.AppendChild(aktivnost);
                }
                XmlAttribute obrisanAttr = xmlObject.CreateAttribute("_obrisan");
                obrisanAttr.Value = p.Obrisan.ToString();
                noviNode.Attributes.Append(obrisanAttr);
                XmlAttribute idVoditeljaAttr = xmlObject.CreateAttribute("_idVoditelja");
                idVoditeljaAttr.Value = p.IdVoditelja.ToString();
                noviNode.Attributes.Append(idVoditeljaAttr);
                XmlAttribute nositelj = xmlObject.CreateAttribute("_nositelj");
                nositelj.Value = p.Nositelj;
                noviNode.Attributes.Append(nositelj);
                XmlAttribute vrijednost = xmlObject.CreateAttribute("_vrijednost");
                vrijednost.Value = p.Vrijednost.ToString();
                noviNode.Attributes.Append(vrijednost);
                XmlAttribute idLokacije = xmlObject.CreateAttribute("_idLokacije");
                idLokacije.Value = p.IdLokacije.ToString();
                noviNode.Attributes.Append(idLokacije);
                noviNode.AppendChild(clanoviNode);
                noviNode.AppendChild(AktivnostiNode);
                projektNode.AppendChild(noviNode);
            }

            xmlObject.Save(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Projekti.xml");
        }

        public List<Lokacija> UcitajLokacije()
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Lokacije.xml"))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<Lokacija> lLokacija = new List<Lokacija>();

            XmlNodeList lokacije = xmlObject.SelectNodes("//Lokacije/Lokacija");
            foreach (XmlNode lok in lokacije)
            {
                lLokacija.Add(new Lokacija(new Guid(lok.Attributes["_id"].Value),
                    lok.Attributes["_adresa"].Value,
                    lok.Attributes["_pBroj"].Value,
                    lok.Attributes["_grad"].Value,
                    lok.Attributes["_lat"].Value,
                    lok.Attributes["_long"].Value
                    )) ;
            }
            listaLokacija = lLokacija;
            return lLokacija;
        }

        public void ZapisiLokacije(List<Lokacija> lLokacija)
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Lokacije.xml"))
            {
                xml = sr.ReadToEnd();
            }
            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);
            XmlNode projektNode = xmlObject.SelectSingleNode("//Lokacije");
            projektNode.RemoveAll();

            foreach (Lokacija lok in lLokacija)
            {
                XmlNode noviNode = xmlObject.CreateNode(XmlNodeType.Element, "Lokacija", null);
                XmlAttribute idAttr = xmlObject.CreateAttribute("_id");
                idAttr.Value = lok.IdLokacije.ToString();
                noviNode.Attributes.Append(idAttr);
                XmlAttribute adresaAttr = xmlObject.CreateAttribute("_adresa");
                adresaAttr.Value = lok.Adresa;
                noviNode.Attributes.Append(adresaAttr);
                XmlAttribute pbrojAttr = xmlObject.CreateAttribute("_pBroj");
                pbrojAttr.Value = lok.pBroj;
                noviNode.Attributes.Append(pbrojAttr);
                XmlAttribute GradAttr = xmlObject.CreateAttribute("_grad");
                GradAttr.Value = lok.Grad;
                noviNode.Attributes.Append(GradAttr);
                XmlAttribute LatitudaAttr = xmlObject.CreateAttribute("_lat");
                LatitudaAttr.Value = lok.Lat;
                noviNode.Attributes.Append(LatitudaAttr);
                XmlAttribute longAttr = xmlObject.CreateAttribute("_long");
                longAttr.Value = lok.Long;
                noviNode.Attributes.Append(longAttr);
                projektNode.AppendChild(noviNode);
            }

            xmlObject.Save(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\Lokacije.xml");
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
            foreach(Guid idClana in prikazaniProjekt.ListaIdClanova)
            {
                foreach(ClanProjekta cp in ulaznaListaClanova)
                {
                    if(cp.Id == idClana)
                    {
                        Console.WriteLine($"{cp.Ime} {cp.Prezime} {cp.Oib}");
                    }
                    if(cp.Id == prikazaniProjekt.IdVoditelja)
                    {
                        voditelj = $"{cp.Ime} {cp.Prezime} {cp.Oib}";
                    }
                }
            }
            Console.WriteLine("Voditelj tog projekta je " + voditelj);
            foreach(Guid idAktivnosti in prikazaniProjekt.ListaIdAktivnosti)
            {
                foreach(Aktivnost ak in ulaznaListaAktivnosti)
                {
                    if(ak.IdAktivnosti == idAktivnosti)
                    {
                        Console.WriteLine($"Naziv aktivnosti je {ak.Naziv}");
                        Console.WriteLine($"Opis aktivnosti je {ak.Opis}");
                        Console.WriteLine($"Vrijeme pocetka aktivnost je {ak.VrijemePocetka}");
                        string lokacija = "";
                        foreach(Lokacija lok in ulaznaListaLokacija)
                        {
                            if(lok.IdLokacije == ak.IdLokacije)
                            {
                                lokacija = $"{lok.Grad}, {lok.pBroj}, {lok.Adresa}, {lok.Lat}, {lok.Long}";
                            }
                        }
                        Console.WriteLine($"Lokacija Aktivnosti je {lokacija}");
                        Console.WriteLine("Clanovi koji sudjeluju na toj aktivnosti su: ");
                        foreach(Guid idClanovaAktivnosti in ak.LIdClanovaProjekta)
                        {
                            foreach(ClanProjekta cp in ulaznaListaClanova)
                            {
                                if(cp.Id == idClanovaAktivnosti)
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

        public void prikaziProjekte()
        {
            List<ClanProjekta> lClanova = UcitajClanove();
            List<Aktivnost> lAktivnosti = UcitajAktivnosti();
            List<Lokacija> lLokacija =  UcitajLokacije();
            List<Projekt> lProjekta =  UcitajProjekte();
            int brojNeaktivnihProjekata = 0;
            int vrijednostSvihProjekata = 0;
            int vrijednostAktivnihProjekata = 0;
            var Table = new ConsoleTable("R.br", "Naziv", "Nositelj", "Vrijednost", "Status", "Voditelj", "Lokacija");
            for(int i = 0; i < lProjekta.Count; i++)
            {
                vrijednostSvihProjekata += lProjekta[i].Vrijednost;
                
                string status = "Aktivan";
                string lokacija = "";
                string Voditelj = "";
                foreach(ClanProjekta cp in lClanova)
                {
                    if(cp.Id == lProjekta[i].IdVoditelja)
                    {
                        Voditelj = $"{cp.Ime} {cp.Prezime}";
                    }
                }
                if (lProjekta[i].Obrisan)
                {
                    status = "Neaktivan";
                    brojNeaktivnihProjekata++;
                }else
                {
                    vrijednostAktivnihProjekata += lProjekta[i].Vrijednost;
                }
                foreach(Lokacija lok in lLokacija)
                {
                    if(lok.IdLokacije == lProjekta[i].IdLokacije)
                    {
                        lokacija = lok.Grad;
                    }
                }
                Table.AddRow(i + 1, lProjekta[i].ImeProjekta, lProjekta[i].Nositelj, lProjekta[i].Vrijednost, status, Voditelj, lokacija);
            }
            Table.Write();
            float postotakVrijednostiAktivnihProjekata = (float)(vrijednostAktivnihProjekata) / vrijednostSvihProjekata;
            Console.WriteLine($"Broj aktivnih projekata je {(float)(lProjekta.Count-brojNeaktivnihProjekata)/ lProjekta.Count*100}%, a vrijednost je {vrijednostAktivnihProjekata} sto je {postotakVrijednostiAktivnihProjekata*100}%");
            Console.WriteLine($"Vrijednost svih projekata je {vrijednostSvihProjekata}");
            while (true)
            {
                Console.WriteLine("Unesite odabir za vise detalje projektu ili 0 za povratak");
                try
                {
                    int odabir = Convert.ToInt32(Console.ReadLine());
                    if(odabir == 0)
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
        public void DodavanjeProjekta()
        {
            Console.WriteLine("Unesite ime projekta");
            try
            {
                string imeProjekta = Console.ReadLine();
                List<Guid> listaClanova = new List<Guid>();
                List<Guid> listaAktivnosti = new List<Guid>();
                while (true)
                {
                    Console.WriteLine("Odaberite clanove za projekt. Unesite 0 za povratak");
                    for(int i = 0; i < listaClanovaProjekta.Count; i++)
                    {
                        Console.WriteLine($"{i+1}. {listaClanovaProjekta[i].Ime} {listaClanovaProjekta[i].Prezime} ({listaClanovaProjekta[i].Oib})");
                    }
                    int odabir = Convert.ToInt32(Console.ReadLine());
                    if(odabir == 0)
                    {
                        break;
                    }else
                    {
                        listaClanova.Add(listaClanovaProjekta[odabir + 1].Id);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
