using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class Projekt
    {
        private string _ImeProjekta;
        private List<ClanProjekta> _ClanoviProjekta;
        private List<Aktivnost> _ListaAktivnosti;
        private bool _Obrisan;

        public Projekt(string imeProjekta, List<ClanProjekta> clanoviProjekta, List<Aktivnost> listaAktivnosti)
        {
            _ImeProjekta = imeProjekta;
            _ClanoviProjekta = clanoviProjekta;
            _ListaAktivnosti = listaAktivnosti;
            _Obrisan = false;
        }
    }
}
