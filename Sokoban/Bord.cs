using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sokoban
{
    class Bord
    {
        // loopt te zeiken dat ie niet wordt gebruikt maar als je het verwijdert werkt de rest niet xD
        List<List<Vak>> vakken;
        
        // string[] doolhof als parameter meegeven
        public Bord(string[] doolhof) 
        {
            // list aanmaken
            vakken = new List<List<Vak>>();
            
            for (int i = 0; i < doolhof.Length; i++)
            {
                // sublist aanmaken
                List<Vak> sublist = new List<Vak>();

                for (int j = 0; j < doolhof[j].Split().Length; j++)
                {
                    // Geeft string[] door aan BepaalVak() en voegt die toe aan sublist
                    sublist.Add(BepaalVak(doolhof[j].Split()));
                }
                // Voegt sublist toe aan vakken list
                vakken.Add(sublist);
            }
        }

        void LaatZien(Grid grid)
        {
        }
        //
        // Retourneert Vak 
        // Vak wordt bepaald aan de hand van een string
        public Vak BepaalVak(string[] dh)
        {
            for (int i = 0; i < dh.Length; i++)
            {
                switch (dh[i])
                {
                    case "#":
                        return new Muur();
                    case "o":
                        // nog niet aangemaakt
                        //return new Kist();
                        break;
                    case "x":
                        // nog niet aangemaakt
                        //return new Bestemming();
                        break;
                    case "@":
                        // nog niet aangemaakt
                        //return new Speler();
                        break;
                    default:
                        return new Vloer();
                }
            }
            return new Vloer();
        }
    }
}
