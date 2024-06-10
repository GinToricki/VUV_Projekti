using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class Lokacija
    {
        private string _Adresa;
        private string _pBroj;
        private string _Grad;
        private string _Latituda;
        private string _Longituda;

        public Lokacija(string Adresa, string pBroj, string grad, string lat, string longituda)
        {
            _Adresa = Adresa;
            _pBroj = pBroj;
            _Grad = grad;
            _Latituda = lat;
            _Longituda = longituda;
        }
    }
}
