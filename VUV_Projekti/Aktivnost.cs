using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class Aktivnost
    {
        private Guid _idAktivnosti;
        private string _Naziv;
        private string _Opis;
        private DateTime _VrijemePocetka;
        private DateTime _VrijemeKraja;
        private Lokacija _Lokacija;
        private List<ClanProjekta> _ClanProjekta;
        private Guid _idLokacije;
        private List<Guid> _lIdClanovaProjekta;

        public Aktivnost(Guid idAktivnosti, string naziv, string opis, DateTime VP, DateTime VK, Lokacija lokacija, List<ClanProjekta> clanProj, Guid idLokacije, List<Guid> lIdClanovaProjekta)
        {
            _idAktivnosti = idAktivnosti;
            _Naziv = naziv;
            _Opis = opis;
            _VrijemePocetka = VP;
            _VrijemeKraja = VK;
            _Lokacija = lokacija;
            _ClanProjekta = clanProj;
            _idLokacije = idLokacije;
            _lIdClanovaProjekta = lIdClanovaProjekta;
        }

        public Guid IdAktivnosti
        {
            get { return _idAktivnosti; }
        }

        public string Naziv
        {
            get { return _Naziv; }
        }

        public string Opis
        {
            get { return _Opis; }
        }

        public DateTime VrijemePocetka
        {
            get { return _VrijemePocetka; }
        }

        public DateTime VrijemeKraja
        {
            get { return _VrijemeKraja; }
        }

        public Lokacija Lokacija
        {
            get { return _Lokacija; }
        }

        public List<ClanProjekta> ClanoviProjekta
        {
            get { return _ClanProjekta; }
        }

        public Guid IdLokacije
        {
            get { return _idLokacije; }
        }

        public List<Guid> LIdClanovaProjekta
        {
            get { return _lIdClanovaProjekta; }
        }
    }
}
