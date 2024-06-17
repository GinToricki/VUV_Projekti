﻿using System;
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
            string xml = "";
            using(StreamReader sr = new StreamReader(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\ClanoviProjekta.xml"))
            {
                xml = sr.ReadToEnd();
            }
            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);
            XmlNode projektNode = xmlObject.SelectSingleNode("//Clanovi");
            projektNode.RemoveAll();

            ClanProjekta c1 = new ClanProjekta(Guid.NewGuid(),"Tin", "Tinic", "1234567890", DateTime.Now) ;
            ClanProjekta c2 = new ClanProjekta(Guid.NewGuid(),"Marko", "Maric", "3106465331", DateTime.Now);
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


            foreach(ClanProjekta cp in clanovi)
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

            Projekt p1 = new Projekt(Guid.NewGuid(),"teStniprojekt", new List<ClanProjekta> { c1, c2 }, null);
            p1.IzbrisiClana();
            xmlObject.Save(@"C:\Users\exibo\source\repos\VUV_Projekti\VUV_Projekti\ClanoviProjekta.xml");
            Console.ReadKey();
        }
    }
}
