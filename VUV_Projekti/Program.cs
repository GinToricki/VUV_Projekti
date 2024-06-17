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
            string xml = "";
            using(StreamReader sr = new StreamReader("C:\\Users\\exibo\\source\\repos\\VUV_Projekti\\VUV_Projekti\\Projekti.xml"))
            {
                xml = sr.ReadToEnd();
            }
            XmlDocument xmlObject = new XmlDocument();
            xmlObject.LoadXml(xml);
            XmlNode projektNode = xmlObject.SelectSingleNode("//Projekti");
            projektNode.RemoveAll();

            ClanProjekta c1 = new ClanProjekta(Guid.NewGuid(),"Tin", "Tinic", "1234567890", DateTime.Now) ;
            ClanProjekta c2 = new ClanProjekta(Guid.NewGuid(),"ime2", "prezime2", "123421890", DateTime.Now);
            Projekt p1 = new Projekt(Guid.NewGuid(),"teStniprojekt", new List<ClanProjekta> { c1, c2 }, null);
            p1.IzbrisiClana();
            XmlNode noviNode = xmlObject.CreateNode(XmlNodeType.Element, "projekt", null);
            XmlAttribute idAttr = xmlObject.CreateAttribute("id");
            idAttr.Value = p1.IdProjekta.ToString();
            noviNode.Attributes.Append(idAttr);
            XmlAttribute imeAttr = xmlObject.CreateAttribute("ime");
            imeAttr.Value = p1.ImeProjekta;
            noviNode.Attributes.Append(imeAttr);
            projektNode.AppendChild(noviNode);
            xmlObject.Save("C:\\Users\\exibo\\source\\repos\\VUV_Projekti\\VUV_Projekti\\Projekti.xml");
            Console.ReadKey();
        }
    }
}
