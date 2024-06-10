using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class ClanProjekta : Osoba
    {
        public ClanProjekta(string ime, string prezime, string oib, DateTime dob)
        {
            _Ime = ime;
            _Prezime = prezime;
            _Oib = oib;
            _Dob = dob;
            _Obrisan = false;
        }
    }
}
