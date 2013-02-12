using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Sokoban
{
    class Timer : DispatcherTimer
    {
        public int waarde;
        private int _seconden = 0;

        public Timer()
        {
            waarde = _seconden;
        }

        public int Waarde
        {
            get { return waarde; }
        }

        public void StartTimer()
        {
            Interval = new TimeSpan(0, 0, 1); // 1 seconde
            Tick += new EventHandler(Each_Tick);
            Start();
        }

        public void ResetTimer()
        {
            waarde = 0;
        }
        
        // Zolang de timer loopt wordt het elke seconde met 1 opgehoogd
        public void Each_Tick(object sender, EventArgs e)
        {
            waarde = _seconden++;
        }

    }
}
