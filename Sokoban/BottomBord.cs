using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sokoban
{
    class BottomBord : Bord
    {
        // string[] doolhof als parameter meegeven
        public BottomBord(List<string> doolhof, string richting) : base(doolhof, richting) { }
        
        //
        // Retourneert Vak 
        // Vak wordt bepaald aan de hand van een string
        public override Vak BepaalVak(string dh, string richting)
        {
            switch (dh)
            {
                case "#":
                    return new Muur();
                case "x":
                    return new Bestemming();
                default:
                    return new Vloer();
            }
        }
    }
}
