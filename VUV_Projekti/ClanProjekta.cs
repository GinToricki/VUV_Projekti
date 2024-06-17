using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class ClanProjekta : Osoba
    {
        private Guid _id;
        public ClanProjekta(Guid id,string ime, string prezime, string oib, DateTime dob)
        {
            _id = id;
            _Ime = ime;
            _Prezime = prezime;
            _Oib = oib;
            _Dob = dob;
            _Obrisan = false;
        }
        public Guid Id
        {
            get { return _id; }
        }
        public string Ime
        {
            get { return _Ime; }
        }

        public string Prezime
        {
            get { return _Prezime; }
        }

        public string Oib
        {
            get { return _Oib; }
        }

        public bool Obrisan
        {
            get { return _Obrisan; }
            set { _Obrisan = value; }
        }
    }
}
