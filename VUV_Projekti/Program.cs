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
            Lokacija l3 = new Lokacija(Guid.NewGuid(),"Park Osijek 1", "31000", "Osijek", "45.55111", "18.69389");

            List<Lokacija> listaLokacija = new List<Lokacija>() { l1, l3 };


            Aktivnost a1 = new Aktivnost(Guid.NewGuid(), "Nabavi dozvole", "Nabaviti potrebne dozvole za vrstu bazena, provjeriti lokalne propise", DateTime.Now.AddDays(-60), DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });
            Aktivnost a2 = new Aktivnost(Guid.NewGuid(), "Dizajnirati bazen", "Uspostavite konačni dizajn bazena, uključujući oblikovanje, materijale, opremu i značajke.", DateTime.Now.AddDays(-30), DateTime.Now, l1, new List<ClanProjekta> { c4, c5}, l1.IdLokacije, new List<Guid> { c4.Id, c5.Id});
            Aktivnost a3 = new Aktivnost(Guid.NewGuid(), "Iskop i priprema terena", "Pripremite teren za izgradnju, uključujući iskopavanje rupe i izravnavanje tla.", DateTime.Now.AddDays(-20), DateTime.Now, l1, new List<ClanProjekta> { c7}, l1.IdLokacije, new List<Guid> { c7.Id });
            Aktivnost a4 = new Aktivnost(Guid.NewGuid(), "Izgradnja strukture bazena", "Ovisno o vrsti bazena, to može uključivati ​​izgradnju betonskih zidova, postavljanje liner-a ili montažu metalnih panela.", DateTime.Now.AddDays(-25), DateTime.Now, l1, new List<ClanProjekta> { c7}, l1.IdLokacije, new List<Guid> { c7.Id});
            Aktivnost a5 = new Aktivnost(Guid.NewGuid(), "Uravnotežite kemiju vode", "Dodajte potrebne kemikalije za održavanje čiste i zdrave vode.", DateTime.Now.AddDays(-10), DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });


            Aktivnost a6 = new Aktivnost(Guid.NewGuid(), "Razvijte priču", "Smislite privlačnu priču koja će voditi igru i angažirati igrače.", DateTime.Now.AddDays(-60), DateTime.Now, l3, new List<ClanProjekta> { c10, c11, c12 }, l3.IdLokacije, new List<Guid> { c13.Id, c14.Id, c15.Id });
            Aktivnost a7 = new Aktivnost(Guid.NewGuid(), "Definirati likove", "Kreirajte upečatljive likove s kojima će se igrači moći poistovjetiti i za koje će im biti stalo.", DateTime.Now.AddDays(-30), DateTime.Now, l3, new List<ClanProjekta> { c14 }, l3.IdLokacije, new List<Guid> { c14.Id});
            Aktivnost a8 = new Aktivnost(Guid.NewGuid(), "Izradite prototip", "Kreirajte jednostavnu verziju igre kako biste testirali koncepte i prikupili povratne informacije.", DateTime.Now.AddDays(-20), DateTime.Now, l3, new List<ClanProjekta> { c8, c9 }, l3.IdLokacije, new List<Guid> { c8.Id, c9.Id });
            Aktivnost a9 = new Aktivnost(Guid.NewGuid(), "Provedite testiranje igrača", "Omogućiti razlicitim ljudima da igraju igru.", DateTime.Now.AddDays(-25), DateTime.Now, l3, new List<ClanProjekta> { c10, c11, c12 }, l3.IdLokacije, new List<Guid> { c10.Id, c11.Id, c12.Id });
            Aktivnost a10 = new Aktivnost(Guid.NewGuid(), "Uravnotežite igru", "Pobrinite se da je igra izazovna, ali i pravedna za sve igrače.", DateTime.Now.AddDays(-10), DateTime.Now, l3, new List<ClanProjekta> { c13, c14, c15 }, l3.IdLokacije, new List<Guid> { c13.Id, c14.Id, c15.Id });

            Aktivnost a11 = new Aktivnost(Guid.NewGuid(), "Odredite cilj i viziju kluba", "Definirajte što želite postići s klubom, za koje dobne kategorije će biti namijenjen i kakvu atmosferu želite stvoriti.", DateTime.Now.AddDays(-60), DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });
            Aktivnost a12 = new Aktivnost(Guid.NewGuid(), "Registrirajte klub", "Legalizirajte svoj klub kod nadležnih tijela i dobijte potrebne dozvole za rad.", DateTime.Now.AddDays(-30), DateTime.Now, l1, new List<ClanProjekta> { c4, c5 }, l1.IdLokacije, new List<Guid> { c4.Id, c5.Id });
            Aktivnost a13 = new Aktivnost(Guid.NewGuid(), "Otvorete bankovni račun", "Postavite financijski okvir za klub i olakšajte plaćanje troškova..", DateTime.Now.AddDays(-20), DateTime.Now, l1, new List<ClanProjekta> { c7 }, l1.IdLokacije, new List<Guid> { c7.Id });
            Aktivnost a14 = new Aktivnost(Guid.NewGuid(), "Pronađite trenera", "Angažirajte iskusnog i kvalificiranog trenera koji će voditi timove i pomagati igračima da se razvijaju.", DateTime.Now.AddDays(-25), DateTime.Now, l1, new List<ClanProjekta> { c7 }, l1.IdLokacije, new List<Guid> { c7.Id });
            Aktivnost a15 = new Aktivnost(Guid.NewGuid(), "Pronađite sponzore", "Tražite financijsku podršku od lokalnih tvrtki i organizacija.", DateTime.Now.AddDays(-10), DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });



            Projekt p1 = new Projekt(Guid.NewGuid(), "Izgradnja Bazena",new List<Guid> {a1.IdAktivnosti  }, new List<Guid> {c1.Id,c2.Id,c3.Id,c4.Id,c5.Id,c6.Id,c7.Id }, c1.Id, "Marko Maric", l1.IdLokacije, 71369);
            Projekt p2 = new Projekt(Guid.NewGuid(), "Dizajn video igre",new List<Guid> { a2.IdAktivnosti}, new List<Guid> { c8.Id,c9.Id,c10.Id,c11.Id,c12.Id,c13.Id,c14.Id,c15.Id}, c14.Id, c11.Ime + " " + c11.Prezime, l3.IdLokacije, 50019);
            Projekt p3 = new Projekt(Guid.NewGuid(), "Izgradnja Bazena", new List<Guid> { a1.IdAktivnosti }, new List<Guid> { c1.Id, c2.Id, c3.Id, c4.Id, c5.Id, c6.Id, c7.Id }, c1.Id, c1.Ime + " " + c1.Prezime, l1.IdLokacije, 25000, true);



            a1.IdProjekta = p1.IdProjekta;
            a2.IdProjekta = p2.IdProjekta;

            List<Aktivnost> lAktivnosti = new List<Aktivnost>() { a1, a2, a3, a4, a5,a6,a7,a8,a9,a10, a11,a12,a13,a14,a15 };

            List<Projekt> lProjekta = new List<Projekt>() { p1, p2, p3};

            DatotetaKlasa dk1 = new DatotetaKlasa();

            Izbornik i1 = new Izbornik();


            dk1.ZapisiClanove(clanovi);
            dk1.ZapisiLokacije(listaLokacija);
            dk1.ZapisiAktivnosti(lAktivnosti);
            dk1.ZapisiProjekte(lProjekta);
            i1.PrikaziIzbornik();
            Console.ReadKey();
        }
    }
}


/*
 
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


            Aktivnost a6 = new Aktivnost(Guid.NewGuid(), "Update Logo", "Promjeni stari logo u novi poboljsani logo", DateTime.Now.AddDays(-60), DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });
            Aktivnost a7 = new Aktivnost(Guid.NewGuid(), "Promjeni sučelje", "Promjeni sučelje aplikacije", DateTime.Now.AddDays(-30), DateTime.Now, l2, new List<ClanProjekta> { c4, c5, c6 }, l2.IdLokacije, new List<Guid> { c4.Id, c5.Id, c6.Id });
            Aktivnost a8 = new Aktivnost(Guid.NewGuid(), "Obojaj Ured", "Obojaj ured iz zute u plavu", DateTime.Now.AddDays(-20), DateTime.Now, l2, new List<ClanProjekta> { c7, c8, c9 }, l2.IdLokacije, new List<Guid> { c7.Id, c8.Id, c9.Id });
            Aktivnost a9 = new Aktivnost(Guid.NewGuid(), "Kupi materijal", "Kupi novi materijal", DateTime.Now.AddDays(-25), DateTime.Now, l3, new List<ClanProjekta> { c10, c11, c12 }, l3.IdLokacije, new List<Guid> { c10.Id, c11.Id, c12.Id });
            Aktivnost a10 = new Aktivnost(Guid.NewGuid(), "Business meeting", "Poslovni sastanak s klijentom", DateTime.Now.AddDays(-10), DateTime.Now, l3, new List<ClanProjekta> { c13, c14, c15 }, l3.IdLokacije, new List<Guid> { c13.Id, c14.Id, c15.Id });


            Aktivnost a11 = new Aktivnost(Guid.NewGuid(), "Obojaj Ured", "Obojaj ured iz zute u plavu", DateTime.Now.AddDays(-60), DateTime.Now, l1, new List<ClanProjekta> { c1, c2, c3 }, l1.IdLokacije, new List<Guid> { c1.Id, c2.Id, c3.Id });
            Aktivnost a12 = new Aktivnost(Guid.NewGuid(), "Kupi materijal", "Kupi novi materijal", DateTime.Now.AddDays(-30), DateTime.Now, l2, new List<ClanProjekta> { c4, c5, c6 }, l2.IdLokacije, new List<Guid> { c4.Id, c5.Id, c6.Id });
            Aktivnost a13 = new Aktivnost(Guid.NewGuid(), "Business meeting", "Poslovni sastanak s klijentom", DateTime.Now.AddDays(-20), DateTime.Now, l2, new List<ClanProjekta> { c7, c8, c9 }, l2.IdLokacije, new List<Guid> { c7.Id, c8.Id, c9.Id });
            Aktivnost a14 = new Aktivnost(Guid.NewGuid(), "Update Logo", "Promjeni stari logo u novi poboljsani logo", DateTime.Now.AddDays(-25), DateTime.Now, l3, new List<ClanProjekta> { c10, c11, c12 }, l3.IdLokacije, new List<Guid> { c10.Id, c11.Id, c12.Id });
            Aktivnost a15 = new Aktivnost(Guid.NewGuid(), "Promjeni sučelje", "Promjeni sučelje aplikacije", DateTime.Now.AddDays(-10), DateTime.Now, l3, new List<ClanProjekta> { c13, c14, c15 }, l3.IdLokacije, new List<Guid> { c13.Id, c14.Id, c15.Id });




            Projekt p1 = new Projekt(Guid.NewGuid(), "teStniprojekt",new List<Guid> {a1.IdAktivnosti,a6.IdAktivnosti,a11.IdAktivnosti  }, new List<Guid> {c1.Id,c2.Id,c3.Id }, c1.Id, "Marko Maric", l1.IdLokacije, 713691011);
            Projekt p2 = new Projekt(Guid.NewGuid(), "NavySnail",new List<Guid> { a2.IdAktivnosti,a7.IdAktivnosti,a12.IdAktivnosti }, new List<Guid> { c4.Id, c5.Id, c6.Id }, c4.Id, "Ana Anic", l2.IdLokacije, 500192330);
            Projekt p3 = new Projekt(Guid.NewGuid(), "BlackFlea",new List<Guid> { a3.IdAktivnosti,a8.IdAktivnosti,a13.IdAktivnosti }, new List<Guid> { c7.Id, c8.Id, c9.Id }, c7.Id, "Luka Lukic", l2.IdLokacije, 608416784);
            Projekt p4 = new Projekt(Guid.NewGuid(), "AquaTitanium", new List<Guid> { a4.IdAktivnosti, a9.IdAktivnosti, a14.IdAktivnosti }, new List<Guid> { c10.Id, c11.Id, c12.Id }, c10.Id, "Iva Ivic", l3.IdLokacije, 902257809);
            Projekt p5 = new Projekt(Guid.NewGuid(), "YellowZinc", new List<Guid> { a5.IdAktivnosti,a10.IdAktivnosti,a15.IdAktivnosti }, new List<Guid> { c13.Id, c14.Id, c15.Id }, c13.Id, "Tomo Tomic", l3.IdLokacije, 480386624);
            Projekt p6 = new Projekt(Guid.NewGuid(), "BlackTitanium", new List<Guid>(), new List<Guid> { c1.Id }, c1.Id, "Tomo Tomic", l1.IdLokacije, 038704259, true);
            Projekt p7 = new Projekt(Guid.NewGuid(), "PurpleCopper", new List<Guid>(), new List<Guid> { c2.Id }, c2.Id, "Marko Maric", l1.IdLokacije, 467455345, true);
            Projekt p8 = new Projekt(Guid.NewGuid(), "NavySteel", new List<Guid>(), new List<Guid> { c3.Id }, c3.Id, "Tomo Tomic", l1.IdLokacije, 445590282, true);
           

            a1.IdProjekta = p1.IdProjekta;
            a2.IdProjekta = p2.IdProjekta;
            a3.IdProjekta = p3.IdProjekta;
            a4.IdProjekta = p4.IdProjekta;
            a5.IdProjekta = p5.IdProjekta;

            List<Aktivnost> lAktivnosti = new List<Aktivnost>() { a1, a2, a3, a4, a5 };

            List<Projekt> lProjekta = new List<Projekt>() { p1, p2, p3, p4, p5,p6,p7,p8 };

            DatotetaKlasa dk1 = new DatotetaKlasa();

            Izbornik i1 = new Izbornik();


            /*dk1.ZapisiClanove(clanovi);
            dk1.ZapisiLokacije(listaLokacija);
            dk1.ZapisiAktivnosti(lAktivnosti);
            dk1.ZapisiProjekte(lProjekta);*/