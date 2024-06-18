using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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


            ClanProjekta c1 = new ClanProjekta(Guid.NewGuid(), "Tin", "Tinic", "1234567890", DateTime.Now);
            ClanProjekta c2 = new ClanProjekta(Guid.NewGuid(), "Marko", "Maric", "3106465331", DateTime.Now);
            ClanProjekta c3 = new ClanProjekta(Guid.NewGuid(), "Luka", "Lukic", "4001141091", DateTime.Now);
            ClanProjekta c4 = new ClanProjekta(Guid.NewGuid(), "Iva", "Ivic", "7329962608", DateTime.Now);
            ClanProjekta c5 = new ClanProjekta(Guid.NewGuid(), "Ana", "Anic", "3255805385", DateTime.Now);
            ClanProjekta c6 = new ClanProjekta(Guid.NewGuid(), "Lucija", "Lucic", "7445589915", DateTime.Now);
            ClanProjekta c7 = new ClanProjekta(Guid.NewGuid(), "Lara", "Zeljko", "7107203201", DateTime.Now);
            ClanProjekta c8 = new ClanProjekta(Guid.NewGuid(), "Neven", "Pavlovic", "1892122323", DateTime.Now);
            ClanProjekta c9 = new ClanProjekta(Guid.NewGuid(), "Ivan", "Kanjka", "8535913317", DateTime.Now);
            ClanProjekta c10 = new ClanProjekta(Guid.NewGuid(), "Filip", "Stanja", "4396431148", DateTime.Now);
            ClanProjekta c11 = new ClanProjekta(Guid.NewGuid(), "Micoje", "Stanja", "8484462278", DateTime.Now);
            ClanProjekta c12 = new ClanProjekta(Guid.NewGuid(), "Fran", "Jurkic", "5767991886", DateTime.Now);
            ClanProjekta c13 = new ClanProjekta(Guid.NewGuid(), "Domagoj", "Paukovic", "7465203617", DateTime.Now);
            ClanProjekta c14 = new ClanProjekta(Guid.NewGuid(), "Sebastijan", "Senic", "9769384345", DateTime.Now);
            ClanProjekta c15 = new ClanProjekta(Guid.NewGuid(), "Luka", "Petras", "3995890681", DateTime.Now);

            List<ClanProjekta> clanovi = new List<ClanProjekta>() { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15 };

            Lokacija l1 = new Lokacija(Guid.NewGuid(),"Savska cesta 32", "10000", "Zagreb", "45.815399", "15.966568");
            Lokacija l2 = new Lokacija(Guid.NewGuid(),"Poduzetnička zona 2", "33000", "Virovitica", "45.83194", "17.38389");
            Lokacija l3 = new Lokacija(Guid.NewGuid(),"IT park Osijek 1", "31000", "Osijek", "45.55111", "18.69389");

            List<Lokacija> listaLokacija = new List<Lokacija>() { l1, l2, l3 };


            Aktivnost a1 = new Aktivnost(Guid.NewGuid(), "Promjeni sučelje", "Promjeni sučelje aplikacije", DateTime.Now, DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });
            Aktivnost a2 = new Aktivnost(Guid.NewGuid(), "Update Logo", "Promjeni stari logo u novi poboljsani logo", DateTime.Now, DateTime.Now, l2, new List<ClanProjekta> { c4, c5, c6 }, l2.IdLokacije, new List<Guid> { c4.Id, c5.Id, c6.Id });
            Aktivnost a3 = new Aktivnost(Guid.NewGuid(), "Kupi materijal", "Kupi novi materijal", DateTime.Now, DateTime.Now, l2, new List<ClanProjekta> { c7, c8, c9 }, l2.IdLokacije, new List<Guid> { c7.Id, c8.Id, c9.Id });
            Aktivnost a4 = new Aktivnost(Guid.NewGuid(), "Business meeting", "Poslovni sastanak s klijentom", DateTime.Now, DateTime.Now, l3, new List<ClanProjekta> { c10, c11, c12 }, l3.IdLokacije, new List<Guid> { c10.Id, c11.Id, c12.Id });
            Aktivnost a5 = new Aktivnost(Guid.NewGuid(), "Obojaj Ured", "Obojaj ured iz zute u plavu", DateTime.Now, DateTime.Now, l3, new List<ClanProjekta> { c13, c14, c15 }, l3.IdLokacije, new List<Guid> { c13.Id, c14.Id, c15.Id });

            List<Aktivnost> lAktivnosti = new List<Aktivnost>() { a1, a2, a3, a4, a5 }; 


            Projekt p1 = new Projekt(Guid.NewGuid(), "teStniprojekt",new List<Guid> {a1.IdAktivnosti }, new List<Guid> {c1.Id,c2.Id,c3.Id }, c1.Id, "Marko Maric");
            Projekt p2 = new Projekt(Guid.NewGuid(), "NavySnail",new List<Guid> { a2.IdAktivnosti }, new List<Guid> { c4.Id, c5.Id, c6.Id }, c4.Id, "Ana Anic");
            Projekt p3 = new Projekt(Guid.NewGuid(), "BlackFlea",new List<Guid> { a3.IdAktivnosti }, new List<Guid> { c7.Id, c8.Id, c9.Id }, c7.Id, "Luka Lukic");
            Projekt p4 = new Projekt(Guid.NewGuid(), "AquaTitanium", new List<Guid> { a4.IdAktivnosti }, new List<Guid> { c10.Id, c11.Id, c12.Id }, c10.Id, "Iva Ivic");
            Projekt p5 = new Projekt(Guid.NewGuid(), "YellowZinc", new List<Guid> { a5.IdAktivnosti }, new List<Guid> { c13.Id, c14.Id, c15.Id }, c13.Id, "Tomo Tomic");

            List<Projekt> lProjekta = new List<Projekt>() { p1, p2, p3, p4, p5 };

            DatotetaKlasa dk1 = new DatotetaKlasa();




            dk1.ZapisiClanove(clanovi);
            dk1.ZapisiLokacije(listaLokacija);
            dk1.ZapisiAktivnosti(lAktivnosti);
            dk1.ZapisiProjekte(lProjekta);
            dk1.UcitajAktivnosti();
            dk1.UcitajProjekte();
            dk1.UcitajLokacije();
            Console.ReadKey();
        }
    }
}


/*
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
                IzbrisanAttr.Value = cp.Obrisan.ToString() ;
                noviNode.Attributes.Append(IzbrisanAttr
                    );
                projektNode.AppendChild(noviNode);
            }

            xmlObject.Save(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\ClanoviProjekta.xml");


        ostatak clanova


                    XmlAttribute imeAttr2 = xmlObject.CreateAttribute("_ime");
                    imeAttr2.Value = cp.Ime;
                    noviNode2.Attributes.Append(imeAttr2);
                    XmlAttribute prezimeAttr = xmlObject.CreateAttribute("_prezime");
                    prezimeAttr.Value = cp.Prezime;
                    noviNode2.Attributes.Append(prezimeAttr);
                    XmlAttribute OibAttr = xmlObject.CreateAttribute("_oib");
                    OibAttr.Value = cp.Oib;
                    noviNode2.Attributes.Append(OibAttr);
                    XmlAttribute IzbrisanAttr = xmlObject.CreateAttribute("_obrisan");
                    IzbrisanAttr.Value = cp.Obrisan.ToString();
                    noviNode2.Attributes.Append(IzbrisanAttr);
 
 */




/*
 string xml = "";
            using (StreamReader sr = new StreamReader(@"C:\Users\Korisnik\Source\Repos\GinToricki\VUV_Projekti\VUV_Projekti\Projekti.xml"))
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
                foreach(Guid a in p.ListaIdAktivnosti)
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
                noviNode.AppendChild(clanoviNode);
                noviNode.AppendChild(AktivnostiNode);
                projektNode.AppendChild(noviNode);
            }

            xmlObject.Save(@"C:\Users\Korisnik\Source\Repos\GinToricki\VUV_Projekti\VUV_Projekti\Projekti.xml");*/
