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
        string clanoviPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ClanoviProjekta.xml");
        string aktivnostPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Aktivnosti.xml");
        string projektiPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Projekti.xml");
        string lokacijePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Lokacije.xml");
        public List<ClanProjekta> UcitajClanove()
        {
            string xml = "";
            using(StreamReader sr = new StreamReader(clanoviPath))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<ClanProjekta> clanoviProjekta = new List<ClanProjekta>();

            XmlNodeList clanovi = xmlObject.SelectNodes("//Clanovi/ClanProjekta");
            foreach(XmlNode clanProjekta in clanovi)
            {
                try
                {
                    if(!bool.TryParse(clanProjekta.Attributes["_obrisan"].Value, out bool _))
                    {
                        string poruka = $"Za clana s id: {clanProjekta.Attributes["_id"].Value} nije pravilno unesen _obrisan";
                        throw new DatotekaException(poruka);
                    }
                    if (!DateTime.TryParse(clanProjekta.Attributes["_dob"].Value, out DateTime _))
                    {
                        string poruka = $"Za clana s id: {clanProjekta.Attributes["_id"].Value} nije pravilno unesen dateTime";
                        throw new DatotekaException(poruka);
                    }
                    clanoviProjekta.Add(new ClanProjekta(
                   new Guid(clanProjekta.Attributes["_id"].Value),
                   clanProjekta.Attributes["_ime"].Value,
                   clanProjekta.Attributes["_prezime"].Value,
                   clanProjekta.Attributes["_oib"].Value,
                   DateTime.Parse(clanProjekta.Attributes["_dob"].Value),
                   Convert.ToBoolean(clanProjekta.Attributes["_obrisan"].Value)
                   ));
                }catch(DatotekaException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            listaClanovaProjekta = clanoviProjekta;
            return clanoviProjekta;
        }

        public void ZapisiClanove(List<ClanProjekta> clanovi)
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(clanoviPath))
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

            xmlObject.Save(clanoviPath);
        }

        public List<Aktivnost> UcitajAktivnosti()
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(aktivnostPath))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<Aktivnost> lAktivnosti= new List<Aktivnost>();

            XmlNodeList aktivnosti = xmlObject.SelectNodes("//Aktivnosti/Aktivnost");
            foreach (XmlNode ak in aktivnosti)
            {
                try
                {
                    if (!bool.TryParse(ak.Attributes["_obrisan"].Value, out bool _))
                    {
                        string pogreska = $"za {ak.Attributes["_naziv"].Value} ne valja vrijednost atributa obrisan tipa boolean";
                        throw new DatotekaException(pogreska);
                    }
                    if (!DateTime.TryParse(ak.Attributes["_vp"].Value, out DateTime _))
                    {
                        string poruka = $"Za aktivnost s id: {ak.Attributes["_id"].Value} nije pravilno unesen dateTime";
                        throw new DatotekaException(poruka);
                    }
                    if (!DateTime.TryParse(ak.Attributes["_vk"].Value, out DateTime _))
                    {
                        string poruka = $"Za aktivnost s id: {ak.Attributes["_id"].Value} nije pravilno unesen dateTime";
                        throw new DatotekaException(poruka);
                    }
                    List<Guid> listaId = new List<Guid>();
                    for (XmlNode i = ak.FirstChild.FirstChild; i != null; i = i.NextSibling)
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
                        listaId,
                        new Guid(ak.Attributes["_idProj"].Value),
                        bool.Parse(ak.Attributes["_obrisan"].Value)));
                }
                catch(DatotekaException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            listaAktivnosti = lAktivnosti;
            return lAktivnosti;
        }

       

        public void ZapisiAktivnosti(List<Aktivnost> lAktivnosti)
        {
            string xml = "";
            
            using (StreamReader sr = new StreamReader(aktivnostPath))
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
                XmlAttribute idProj = xmlObject.CreateAttribute("_idProj");
                idProj.Value = ak.IdProjekta.ToString() ;
                noviNode.Attributes.Append(idProj);


                XmlAttribute obrisanAttr = xmlObject.CreateAttribute("_obrisan");
                obrisanAttr.Value = ak.Status.ToString();
                noviNode.Attributes.Append(obrisanAttr);

                noviNode.AppendChild(clanoviNode);
                projektNode.AppendChild(noviNode);
            }

            xmlObject.Save(aktivnostPath);
        }

        public List<Projekt> UcitajProjekte()
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(projektiPath))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<Projekt> lProjekta = new List<Projekt>();

            XmlNodeList projekti= xmlObject.SelectNodes("//Projekti/Projekt");
            foreach (XmlNode p in projekti)
            {
                try
                {
                    if (!Double.TryParse(p.Attributes["_vrijednost"].Value, out double _))
                    {
                        string poruka = $"Za projekt s id: {p.Attributes["_id"].Value} nije pravilno unesena vrijednost";
                        throw new DatotekaException(poruka);
                    }
                    if (!bool.TryParse(p.Attributes["_obrisan"].Value, out bool _))
                    {
                        string poruka = $"Za projekt s id: {p.Attributes["_id"].Value} nije pravilno bool _obrisan";
                        throw new DatotekaException(poruka);
                    }
                    List<Guid> listaIdClanova = new List<Guid>();
                    List<Guid> listaIdAktivnosti = new List<Guid>();
                    for (XmlNode i = p.FirstChild.FirstChild; i != null; i = i.NextSibling)
                    {
                        listaIdClanova.Add(new Guid(i.Attributes["_id"].Value));
                    }
                    for (XmlNode i = p.LastChild.FirstChild; i != null; i = i.NextSibling)
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
                        Convert.ToDouble(p.Attributes["_vrijednost"].Value),
                        bool.Parse(p.Attributes["_obrisan"].Value)
                        ));
                }
                catch(Exception e)
                {

                }
               
            }

           
            listaProjekta = lProjekta;
            return lProjekta;
        }

        public void ZapisiProjekte(List<Projekt> lProjekta)
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(projektiPath))
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

            xmlObject.Save(projektiPath);
        }

        public List<Lokacija> UcitajLokacije()
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(lokacijePath))
            {
                xml = sr.ReadToEnd();
            }

            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);

            List<Lokacija> lLokacija = new List<Lokacija>();

            XmlNodeList lokacije = xmlObject.SelectNodes("//Lokacije/Lokacija");
            foreach (XmlNode lok in lokacije)
            {
                try
                {
                    lLokacija.Add(new Lokacija(new Guid(lok.Attributes["_id"].Value),
                   lok.Attributes["_adresa"].Value,
                   lok.Attributes["_pBroj"].Value,
                   lok.Attributes["_grad"].Value,
                   lok.Attributes["_lat"].Value,
                   lok.Attributes["_long"].Value
                   ));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            listaLokacija = lLokacija;
            return lLokacija;
        }

        public void ZapisiLokacije(List<Lokacija> lLokacija)
        {
            string xml = "";
            using (StreamReader sr = new StreamReader(lokacijePath))
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

            xmlObject.Save(lokacijePath);
        }
    }
}
