﻿using System;
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
    abstract class Bord
    {
        List<List<Vak>> vakken;

        // string[] doolhof als parameter meegeven
        public Bord(List<string> doolhof, string richting) 
        {
            // list aanmaken
            vakken = new List<List<Vak>>();
            
            for (int i = 0; i < doolhof.Count; i++)
            {
                // sublist aanmaken
                List<Vak> sublist = new List<Vak>();

                foreach (char c in doolhof[i])
                {
                    sublist.Add(BepaalVak(c.ToString(), richting));
                }

                // Voegt sublist toe aan vakken list
                vakken.Add(sublist);
            }
        }

        public Grid toonGrid()
        {
            Grid grid = new Grid();
            //int CellSize = 40;

            for(int i=0; i < vakken.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                //row.Height = new GridLength(CellSize);
                grid.RowDefinitions.Add(row);

                //System.Diagnostics.Debug.WriteLine("TEST:" + vakken[i].Count);

                for(int j=0; j < vakken[i].Count; j++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    //col.Width = new GridLength(CellSize);
                    grid.ColumnDefinitions.Add(col);

                    //Get vak
                    List<Vak> sublist = vakken[i];
                    Vak temp = sublist[j];

                    //Get image
                    Image img = new Image();
                    img.Source = temp.Image;

                    //Set image
                    img.SetValue(Grid.ColumnProperty, j);
                    img.SetValue(Grid.RowProperty, i);
                    img.Stretch = Stretch.UniformToFill;
                    grid.Children.Add(img);
                }
            }
            
            return grid;
        }

        //
        // Retourneert Vak 
        // Vak wordt bepaald aan de hand van een string
        public virtual Vak BepaalVak(string dh, string richting)
        {
            // retourneert standaard null omdat subklassen via override Bepaalvak overnemen
            return null;
        }
    }
}
