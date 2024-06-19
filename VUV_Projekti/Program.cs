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
        static void Main(string[] args)
        {


            ClanProjekta c1 = new ClanProjekta(Guid.NewGuid(), "Tin", "Tinic", "1234567890", 23);
            ClanProjekta c2 = new ClanProjekta(Guid.NewGuid(), "Marko", "Maric", "3106465331", 25);
            ClanProjekta c3 = new ClanProjekta(Guid.NewGuid(), "Luka", "Lukic", "4001141091", 26);
            ClanProjekta c4 = new ClanProjekta(Guid.NewGuid(), "Iva", "Ivic", "7329962608", 21);
            ClanProjekta c5 = new ClanProjekta(Guid.NewGuid(), "Ana", "Anic", "3255805385", 23);
            ClanProjekta c6 = new ClanProjekta(Guid.NewGuid(), "Lucija", "Lucic", "7445589915", 22);
            ClanProjekta c7 = new ClanProjekta(Guid.NewGuid(), "Lara", "Zeljko", "7107203201", 19);
            ClanProjekta c8 = new ClanProjekta(Guid.NewGuid(), "Neven", "Pavlovic", "1892122323", 19);
            ClanProjekta c9 = new ClanProjekta(Guid.NewGuid(), "Ivan", "Kanjka", "8535913317", 20);
            ClanProjekta c10 = new ClanProjekta(Guid.NewGuid(), "Filip", "Stanja", "4396431148", 20);
            ClanProjekta c11 = new ClanProjekta(Guid.NewGuid(), "Micoje", "Stanja", "8484462278", 20);
            ClanProjekta c12 = new ClanProjekta(Guid.NewGuid(), "Fran", "Jurkic", "5767991886", 20);
            ClanProjekta c13 = new ClanProjekta(Guid.NewGuid(), "Domagoj", "Paukovic", "7465203617", 20);
            ClanProjekta c14 = new ClanProjekta(Guid.NewGuid(), "Sebastijan", "Senic", "9769384345", 20);
            ClanProjekta c15 = new ClanProjekta(Guid.NewGuid(), "Luka", "Petras", "3995890681", 20);

            List<ClanProjekta> clanovi = new List<ClanProjekta>() { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15 };

            Lokacija l1 = new Lokacija(Guid.NewGuid(),"Savska cesta 32", "10000", "Zagreb", "45.815399", "15.966568");
            Lokacija l2 = new Lokacija(Guid.NewGuid(),"Poduzetnička zona 2", "33000", "Virovitica", "45.83194", "17.38389");
            Lokacija l3 = new Lokacija(Guid.NewGuid(),"IT park Osijek 1", "31000", "Osijek", "45.55111", "18.69389");

            List<Lokacija> listaLokacija = new List<Lokacija>() { l1, l2, l3 };


            Aktivnost a1 = new Aktivnost(Guid.NewGuid(), "Promjeni sučelje", "Promjeni sučelje aplikacije", DateTime.Now.AddDays(-60), DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });
            Aktivnost a2 = new Aktivnost(Guid.NewGuid(), "Update Logo", "Promjeni stari logo u novi poboljsani logo", DateTime.Now.AddDays(-30), DateTime.Now, l2, new List<ClanProjekta> { c4, c5, c6 }, l2.IdLokacije, new List<Guid> { c4.Id, c5.Id, c6.Id });
            Aktivnost a3 = new Aktivnost(Guid.NewGuid(), "Kupi materijal", "Kupi novi materijal", DateTime.Now.AddDays(-20), DateTime.Now, l2, new List<ClanProjekta> { c7, c8, c9 }, l2.IdLokacije, new List<Guid> { c7.Id, c8.Id, c9.Id });
            Aktivnost a4 = new Aktivnost(Guid.NewGuid(), "Business meeting", "Poslovni sastanak s klijentom", DateTime.Now.AddDays(-25), DateTime.Now, l3, new List<ClanProjekta> { c10, c11, c12 }, l3.IdLokacije, new List<Guid> { c10.Id, c11.Id, c12.Id });
            Aktivnost a5 = new Aktivnost(Guid.NewGuid(), "Obojaj Ured", "Obojaj ured iz zute u plavu", DateTime.Now.AddDays(-10), DateTime.Now, l3, new List<ClanProjekta> { c13, c14, c15 }, l3.IdLokacije, new List<Guid> { c13.Id, c14.Id, c15.Id });

            List<Aktivnost> lAktivnosti = new List<Aktivnost>() { a1, a2, a3, a4, a5 }; 


            Projekt p1 = new Projekt(Guid.NewGuid(), "teStniprojekt",new List<Guid> {a1.IdAktivnosti }, new List<Guid> {c1.Id,c2.Id,c3.Id }, c1.Id, "Marko Maric", l1.IdLokacije,5);
            Projekt p2 = new Projekt(Guid.NewGuid(), "NavySnail",new List<Guid> { a2.IdAktivnosti }, new List<Guid> { c4.Id, c5.Id, c6.Id }, c4.Id, "Ana Anic", l2.IdLokacije,6);
            Projekt p3 = new Projekt(Guid.NewGuid(), "BlackFlea",new List<Guid> { a3.IdAktivnosti }, new List<Guid> { c7.Id, c8.Id, c9.Id }, c7.Id, "Luka Lukic", l2.IdLokacije,7);
            Projekt p4 = new Projekt(Guid.NewGuid(), "AquaTitanium", new List<Guid> { a4.IdAktivnosti }, new List<Guid> { c10.Id, c11.Id, c12.Id }, c10.Id, "Iva Ivic", l3.IdLokacije,8);
            Projekt p5 = new Projekt(Guid.NewGuid(), "YellowZinc", new List<Guid> { a5.IdAktivnosti }, new List<Guid> { c13.Id, c14.Id, c15.Id }, c13.Id, "Tomo Tomic", l3.IdLokacije,9);
            Projekt p6 = new Projekt(Guid.NewGuid(), "BlackTitanium", new List<Guid>(), new List<Guid> { c1.Id }, c1.Id, "Tomo Tomic", l1.IdLokacije, 3, true);
            Projekt p7 = new Projekt(Guid.NewGuid(), "PurpleCopper", new List<Guid>(), new List<Guid> { c2.Id }, c2.Id, "Marko Maric", l1.IdLokacije, 3, true);
            Projekt p8 = new Projekt(Guid.NewGuid(), "NavySteel", new List<Guid>(), new List<Guid> { c3.Id }, c3.Id, "Tomo Tomic", l1.IdLokacije, 3, true);

            List<Projekt> lProjekta = new List<Projekt>() { p1, p2, p3, p4, p5,p6,p7,p8 };

            DatotetaKlasa dk1 = new DatotetaKlasa();

            Izbornik i1 = new Izbornik();


            /*dk1.ZapisiClanove(clanovi);
            dk1.ZapisiLokacije(listaLokacija);
            dk1.ZapisiAktivnosti(lAktivnosti);
            dk1.ZapisiProjekte(lProjekta);*/
            i1.PrikaziIzbornik();
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
