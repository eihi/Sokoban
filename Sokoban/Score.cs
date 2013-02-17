using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Score
    {
        private int _seconden = 0;
        private int _zetten = 0;

        public Score(int seconden, int zetten)
        {
            _seconden = seconden;
            _zetten = zetten;
        }

        public int Seconden
        {
            get { return _seconden; }
            set { _seconden = value; }
        }

        public int Zetten
        {
            get { return _zetten; }
            set { _zetten = value; }
        }
    }
}
