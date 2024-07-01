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
        private Guid _idLokacije;
        private bool _Obrisan;
        private double _Vrijednost;

        public Projekt(Guid idProjekta, string imeProjekta,List<Guid> lIdAktivnosti, List<Guid> ListaIdClanova, Guid idVoditelja, string nositelj, Guid idLokacije, double vrijednost)
        {
            _IdProjekta = idProjekta;
            _ImeProjekta = imeProjekta;
            _ListaIdAktivnosti = lIdAktivnosti;
            _ListaIdClanova = ListaIdClanova;
            _Nositelj = nositelj;
            _idVoditelja = idVoditelja;
            _idLokacije = idLokacije;
            _Vrijednost = vrijednost;
            _Obrisan = false;
        }

        public Projekt(Guid idProjekta, string imeProjekta, List<Guid> lIdAktivnosti, List<Guid> ListaIdClanova, Guid idVoditelja, string nositelj, Guid idLokacije, double vrijednost, bool status)
        {
            _IdProjekta = idProjekta;
            _ImeProjekta = imeProjekta;
            _ListaIdAktivnosti = lIdAktivnosti;
            _ListaIdClanova = ListaIdClanova;
            _Nositelj = nositelj;
            _idVoditelja = idVoditelja;
            _idLokacije = idLokacije;
            _Vrijednost = vrijednost;
            _Obrisan = status;
        }

        public double Vrijednost
        {
            get { return _Vrijednost; }
        }

        public Guid IdLokacije
        {
            get { return _idLokacije; }
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
            set { _Obrisan = value; }
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

        public void PromjenaVoditelja(List<ClanProjekta> ulaznaListaClanova)
        {
            List<ClanProjekta> lClanovaBezVoditelja = new List<ClanProjekta>();
            foreach(ClanProjekta cp in ulaznaListaClanova)
            {
                if (_ListaIdClanova.Contains(cp.Id) && cp.Id != _idVoditelja)
                {
                    lClanovaBezVoditelja.Add(cp);
                }
            }
            for(int i = 0; i < lClanovaBezVoditelja.Count; i++)
            {
                Console.WriteLine($"{i+1}. {lClanovaBezVoditelja[i].Ime} {lClanovaBezVoditelja[i].Prezime} ({lClanovaBezVoditelja[i].Oib})");
            }
            Console.WriteLine("Unesite odabir novog voditelja");
           try
           {
                int odabirNovogVoditelja = Convert.ToInt32(Console.ReadLine()) - 1;
                _idVoditelja = lClanovaBezVoditelja[odabirNovogVoditelja].Id;
           }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
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
                int dob = Convert.ToInt32(Console.ReadLine());
                ClanProjekta noviClan = new ClanProjekta(Guid.NewGuid(),ime, prezime, oib, DateTime.Now);
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

        public void PromjeniVrijednost()
        {
            try
            {
                Console.WriteLine("Unesite novu vrijednost");
                double novaVrijednost = Convert.ToDouble(Console.ReadLine());
                _Vrijednost = novaVrijednost;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public void PromjeniNositelja(List<ClanProjekta> ulaznaListaClanova)
        {
            string imeNositelja = "";
            foreach (ClanProjekta cp in ulaznaListaClanova)
            {
                imeNositelja = $"{cp.Ime} {cp.Prezime}";
                if(imeNositelja == _Nositelj)
                {
                    Console.WriteLine($"Trenutacni nositelj je {imeNositelja}");
                }
            }
            List<ClanProjekta> listaClanovaProjektaBezNositelja = new List<ClanProjekta>();
            foreach (ClanProjekta cp in ulaznaListaClanova)
            {
                string imeClana = $"{cp.Ime} {cp.Prezime}";
                if (_ListaIdClanova.Contains(cp.Id) && imeClana != _Nositelj)
                {
                    listaClanovaProjektaBezNositelja.Add(cp);
                }  
            }
            Console.WriteLine("Odaberite novog Nositelja");
            for(int i = 0; i < listaClanovaProjektaBezNositelja.Count; i++)
            {
                string imeClana = $"{listaClanovaProjektaBezNositelja[i].Ime} {listaClanovaProjektaBezNositelja[i].Prezime}";
                if(imeClana != _Nositelj)
                {
                    Console.WriteLine($"{i + 1}. {listaClanovaProjektaBezNositelja[i].Ime} {listaClanovaProjektaBezNositelja[i].Prezime}");
                }
            }
            try
            {
                int odabir = Convert.ToInt32(Console.ReadLine())-1;
                imeNositelja = $"{listaClanovaProjektaBezNositelja[odabir].Ime} {listaClanovaProjektaBezNositelja[odabir].Prezime}";
                _Nositelj = imeNositelja;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
