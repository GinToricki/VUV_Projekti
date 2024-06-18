using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class Lokacija
    {
        private Guid _idLokacije;
        private string _Adresa;
        private string _pBroj;
        private string _Grad;
        private string _Latituda;
        private string _Longituda;

        public Lokacija(Guid idLokacije, string Adresa, string pBroj, string grad, string lat, string longituda)
        {
            _idLokacije = idLokacije;
            _Adresa = Adresa;
            _pBroj = pBroj;
            _Grad = grad;
            _Latituda = lat;
            _Longituda = longituda;
        }

        public Guid IdLokacije
        {
            get { return _idLokacije; }
        }

        public string Adresa
        {
            get { return _Adresa; }
        }

        public string pBroj
        {
            get { return _pBroj; }
        }

        public string Grad
        {
            get { return _Grad; }
        }

        public string Lat
        {
            get { return _Latituda; }
        }

        public string Long
        {
            get { return _Longituda; }
        }
    }
}
