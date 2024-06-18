using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class Projekt
    {
        private Guid _IdProjekta;
        private string _ImeProjekta;
        private List<ClanProjekta> _ClanoviProjekta;
        private List<Guid> _ListaIdClanova;
        private List<Aktivnost> _ListaAktivnosti;
        private List<Guid> _ListaIdAktivnosti;
        private bool _Obrisan;

        public Projekt(Guid idProjekta, string imeProjekta, List<ClanProjekta> clanoviProjekta, List<Aktivnost> listaAktivnosti, List<Guid> lIdAktivnosti, List<Guid> ListaIdClanova)
        {
            _IdProjekta = idProjekta;
            _ImeProjekta = imeProjekta;
            _ClanoviProjekta = clanoviProjekta;
            _ListaAktivnosti = listaAktivnosti;
            _ListaIdAktivnosti = lIdAktivnosti;
            _ListaIdClanova = ListaIdClanova;
            _Obrisan = false;
        }

        
        public Guid IdProjekta
        {
            get { return _IdProjekta; }
        }

        public string ImeProjekta
        {
            get { return _ImeProjekta; }
        }
        public List<Guid> ListaIdAktivnosti
        {
            get { return _ListaIdAktivnosti; }
        }

        public List<Guid> ListaIdClanova
        {
            get { return _ListaIdClanova; }
        }
        public List<ClanProjekta> ClanoviProjekta
        {
            get { return _ClanoviProjekta; }
        }

        public List<Aktivnost> ListaAktivnosti
        {
            get { return _ListaAktivnosti; }
        }

        public bool Obrisan
        {
            get { return _Obrisan; }
        }

        private bool PotvrdiOib(string oib)
        {
            //dodati da ne mogu biti 2 ista oiba
            if(Int32.TryParse(oib, out int _) && oib.Length == 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DodavanjeClana()
        {
            try
            {
                Console.WriteLine("Unesite ime člana");
                string ime = Console.ReadLine();
                Console.WriteLine("Unesite prezime člana");
                string prezime = Console.ReadLine();
                Console.WriteLine("Unesite OIB člana");
                string oib = Console.ReadLine();
                while (PotvrdiOib(oib))
                {
                    Console.WriteLine("Ponovo unesite oib");
                }
                DateTime dob = DateTime.Now;
                ClanProjekta noviClan = new ClanProjekta(Guid.NewGuid(),ime, prezime, oib, dob);
                Console.WriteLine("Uspijesno ste dodali clana Projekta");
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }

        private bool PotvrdiOdabir(int odabir, int brojClanova)
        {
            if(odabir > 0 && odabir < brojClanova)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void IzbrisiClana()
        {
            Console.WriteLine("Odaberite kojeg clana zelite izbrisati");
            for (int i = 0; i < _ClanoviProjekta.Count; i++)
            {
                if (!_ClanoviProjekta[i].Obrisan)
                {
                    Console.WriteLine($"{i + 1}. {_ClanoviProjekta[i].Ime} {_ClanoviProjekta[i].Prezime}. ({_ClanoviProjekta[i].Oib})");
                }
            }
            while (true)
            {

                try
                {
                    int odabir = Convert.ToInt32(Console.ReadLine());
                    if(PotvrdiOdabir(odabir, _ClanoviProjekta.Count))
                    {
                        _ClanoviProjekta[odabir - 1].Obrisan = true;
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
