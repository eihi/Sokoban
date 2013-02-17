using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Speler : Vak
    {
        private string _richting = "beneden";

        public string Richting
        {
            get { return _richting; }
            set { _richting = value; }
        }

        public Speler(string r)
        {
            if (r.Equals(""))
            {
                MaakPlaatje(_richting);
            }
            else
            {
                MaakPlaatje(r);
            }
        }
    }
}