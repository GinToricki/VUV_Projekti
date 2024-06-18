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
        private List<Guid> _ListaIdClanova;
        private List<Guid> _ListaIdAktivnosti;
        private string _Nositelj;
        private Guid _idVoditelja;
        private bool _Obrisan;

        public Projekt(Guid idProjekta, string imeProjekta,List<Guid> lIdAktivnosti, List<Guid> ListaIdClanova, Guid idVoditelja, string nositelj)
        {
            _IdProjekta = idProjekta;
            _ImeProjekta = imeProjekta;
            _ListaIdAktivnosti = lIdAktivnosti;
            _ListaIdClanova = ListaIdClanova;
            _Nositelj = nositelj;
            _idVoditelja = idVoditelja;
            _Obrisan = false;
        }

        
        public Guid IdProjekta
        {
            get { return _IdProjekta; }
        }

        public string Nositelj
        {
            get { return _Nositelj; }
        }

        public Guid IdVoditelja
        {
            get { return _idVoditelja; }
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

        
    }
}
