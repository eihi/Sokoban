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
    class TopBord : Bord
    {
       // string[] doolhof als parameter meegeven
        public TopBord(List<string> doolhof, string richting) : base(doolhof, richting) { }
        
        //
        // Retourneert Vak 
        // Vak wordt bepaald aan de hand van een string
        public override Vak BepaalVak(string dh, string richting)
        {
            switch (dh)
            {
                case "o":
                    return new Doos();
                case "@":
                    return new Speler(richting);
                case "#":
                    return new Muur();
                default:
                    return new Leegte();
            }
        } 
    }
}
