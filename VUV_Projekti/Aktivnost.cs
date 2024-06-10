using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class Aktivnost
    {
        private string _Naziv;
        private string _Opis;
        private DateTime _VrijemePocetka;
        private DateTime _VrijemeKraja;
        private string _IDProjekta;
        private Lokacija _Lokacija;
        private ClanProjekta _ClanProjekta;

        public Aktivnost(string naziv, string opis, DateTime VP, DateTime VK, string idProjekta, Lokacija lokacija, ClanProjekta clanProj)
        {
            _Naziv = naziv;
            _Opis = opis;
            _VrijemePocetka = VP;
            _VrijemeKraja = VK;
            _IDProjekta = idProjekta;
            _Lokacija = lokacija;
            _ClanProjekta = clanProj;
        }
    }
}
