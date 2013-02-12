using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Doos : Vak
    {
        private bool _open = false;

        public bool Open
        {
            get { return _open; }
            set { _open = value; }
        }
        
        public Doos()
        {
            if (_open == false)
                MaakPlaatje("doos");
            else
                MaakPlaatje("doosopen");
        }

        public void opBestemming()
        {
            // Als het onderliggend vak een bestemming is wordt open op true gezet
            _open = true;
        }
    }
}
