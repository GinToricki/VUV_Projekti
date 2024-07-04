using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_Projekti
{
    class DatotekaException : Exception
    {
        public DatotekaException () { }

        public DatotekaException (string message) : base (message) { }

        public DatotekaException(string message, Exception inner) : base (message, inner) { }
    }
}
