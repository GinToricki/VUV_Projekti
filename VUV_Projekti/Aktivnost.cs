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

        public Aktivnost(Guid idAktivnosti,string naziv, string opis, DateTime VP, DateTime VK, Lokacija lokacija, List<ClanProjekta> clanProj)
        {
            _idAktivnosti = idAktivnosti;
            _Naziv = naziv;
            _Opis = opis;
            _VrijemePocetka = VP;
            _VrijemeKraja = VK;
            _Lokacija = lokacija;
            _ClanProjekta = clanProj;
        }
    }
}
