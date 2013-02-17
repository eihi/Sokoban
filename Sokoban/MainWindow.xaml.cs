using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;

namespace Sokoban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> doolhof;

        public List<string> Doolhof
        {
            get { return doolhof; }
            set { doolhof = value; }
        }

        DispatcherTimer timer;
        private int _seconden = 0;

        public MainWindow()
        {
            // Initializen
            InitializeComponent();
            this.SizeChanged += SizeChangedHandler;
            Timer(); // Timer
        }

        private void SizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
            RowDefinition[] rows =  VakkenView.RowDefinitions.ToArray();
            ColumnDefinition[] columns = VakkenView.ColumnDefinitions.ToArray();

            int _cellSize = getCellSize(VakkenView.RowDefinitions.Count,VakkenView.ColumnDefinitions.Count);

            foreach(RowDefinition row in rows)
            { 
                row.Height = new GridLength(_cellSize);
            }

            foreach (ColumnDefinition column in columns)
            {
                column.Width = new GridLength(_cellSize);
            }
        }

        private int getCellSize(int nRows, int nCols)
        {
            double w = VakkenView.ActualWidth;
            double h = VakkenView.ActualHeight - 76;


            int hCellSize = (int)(h / nRows);
            int wCellSize = (int)(w / nCols);


            if (hCellSize < wCellSize)
            {
                return hCellSize;
            }
            else
            {
                return wCellSize;
            }
        }

        public void createGrid(List<string> doolhof)
        {
            BottomBord bottomBord = new BottomBord(doolhof, "");
            TopBord topBord = new TopBord(doolhof, "");

            this.VakkenView.Children.Add(bottomBord.toonGrid());
            this.SpeelBord.Children.Add(topBord.toonGrid());

            ResetTimer();
            StartTimer();
            verstrekenTijd.Content = "Tijd: " + _seconden;
        }

        public void readLevel(string level)
        {
            doolhof = new List<string>();

            Assembly thisExe = Assembly.GetExecutingAssembly();
            string path = thisExe.Location;
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            string folderName = dirinfo.Parent.FullName;
            path = folderName + "/Levels/" + level + ".txt";

            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    doolhof.Add(line);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            readLevel("Doolhof1");
            createGrid(doolhof);
        }

        public void Timer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1); // 1 seconde
            timer.Tick += new EventHandler(Each_Tick);
        }

        public void StartTimer() 
        {
            timer.Start(); // timer starten
        }

        public void ResetTimer()
        {            
            timer.Stop(); // timer stoppen
            
            _seconden = 0; // seconden op 0 zetten
        }

        // Zolang de timer loopt wordt het elke seconde met 1 opgehoogd
        public void Each_Tick(object sender, EventArgs e)
        {
            verstrekenTijd.Content = "Tijd: " + _seconden++; // verstrekenTijd label updaten
        }

        public void CloseLevel()
        {
            this.VakkenView.Children.Clear(); // VakkenView Grid leeggooien
            this.SpeelBord.Children.Clear(); // SpeelBord Grid leeggooien
            verstrekenTijd.Content = ""; // timer label leegmaken
            ResetTimer(); // Timer resetten

            // Leeg de doolhof List??
        }

        private void CloseLevelButton(object sender, RoutedEventArgs e)
        {
            CloseLevel(); // Level sluiten
        }

        // Key Events
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            List<string> tempDoolhof = doolhof;
            string row;
            string verticalRow;
            string secondVerticalRow;
            int positieSpeler;
            int spelerRow;

            string richting;

            switch (e.Key)
            {
                case Key.Left:
                    richting = "links";    

                        // Vind de rij van de speler
                        row = tempDoolhof[VindSpeler()];

                        // Vind de positie van de speler
                        positieSpeler = row.IndexOf("@");

                        // Vind de nieuwe positie
                        if (row[positieSpeler - 1].Equals(Convert.ToChar(" ")))
                        {
                            char[] temp = row.ToCharArray();
                            temp[positieSpeler - 1] = Convert.ToChar("@");
                            temp[positieSpeler] = Convert.ToChar(" ");
 
                            tempDoolhof[VindSpeler()] = new string(temp);
                        }

                        if (row[positieSpeler - 1].Equals(Convert.ToChar("x")))
                        {
                            char[] temp = row.ToCharArray();
                            temp[positieSpeler - 1] = Convert.ToChar("@");
                            temp[positieSpeler] = Convert.ToChar("x");

                            tempDoolhof[VindSpeler()] = new string(temp);
                        }

                        // Staat er een doos?
                        if (row[positieSpeler - 1].Equals(Convert.ToChar("o")))
                        {
                            // Kan de doos bewegen?
                            if (row[positieSpeler - 2].Equals(Convert.ToChar(" ")) || row[positieSpeler - 2].Equals(Convert.ToChar("x")))
                            {
                                // Beweeg doos
                                char[] temp = row.ToCharArray();
                                temp[positieSpeler - 2] = Convert.ToChar("o");
                                temp[positieSpeler - 1] = Convert.ToChar("@");
                                temp[positieSpeler] = Convert.ToChar(" ");

                                tempDoolhof[VindSpeler()] = new string(temp);
                            }
                        }
                    break;

                case Key.Right:
                    richting = "rechts"; 

                    // Vind de rij van de speler
                    row = tempDoolhof[VindSpeler()];

                    // Vind de positie van de speler
                    positieSpeler = row.IndexOf("@");

                    // Vind de nieuwe positie
                    if (row[positieSpeler + 1].Equals(Convert.ToChar(" ")))
                    {
                        char[] temp = row.ToCharArray();
                        temp[positieSpeler + 1] = Convert.ToChar("@");
                        temp[positieSpeler] = Convert.ToChar(" ");

                        tempDoolhof[VindSpeler()] = new string(temp);
                    }

                    if (row[positieSpeler + 1].Equals(Convert.ToChar("x")))
                    {
                        char[] temp = row.ToCharArray();
                        temp[positieSpeler + 1] = Convert.ToChar("@");
                        temp[positieSpeler] = Convert.ToChar("x");

                        tempDoolhof[VindSpeler()] = new string(temp);
                    }

                    // Staat er een doos?
                    if (row[positieSpeler + 1].Equals(Convert.ToChar("o")))
                    {
                        // Kan de doos bewegen?
                        if (row[positieSpeler + 2].Equals(Convert.ToChar(" ")) || row[positieSpeler + 2].Equals(Convert.ToChar("x")))
                        {
                            // Beweeg doos
                            char[] temp = row.ToCharArray();
                            temp[positieSpeler + 2] = Convert.ToChar("o");
                            temp[positieSpeler + 1] = Convert.ToChar("@");
                            temp[positieSpeler] = Convert.ToChar(" ");

                            tempDoolhof[VindSpeler()] = new string(temp);
                        }
                    }

                    break;

                case Key.Up:
                    richting = "boven"; 

                    // Vind de rij van de speler
                    row = tempDoolhof[VindSpeler()];
                    spelerRow = VindSpeler();
                    verticalRow = tempDoolhof[spelerRow - 1];

                    // Vind de positie van de speler
                    positieSpeler = row.IndexOf("@");

                    // Vind de nieuwe positie
                    if (verticalRow[positieSpeler].Equals(Convert.ToChar(" ")))
                    {
                        // Verander in bovenste rij
                        char[] temp = verticalRow.ToCharArray();
                        temp[positieSpeler] = Convert.ToChar("@");
                        tempDoolhof[spelerRow - 1] = new string(temp);

                        // Verander in onderste rij
                        char[] temp2 = row.ToCharArray();
                        temp2[positieSpeler] = Convert.ToChar(" ");
                        tempDoolhof[spelerRow] = new string(temp2);
                    }

                    if (verticalRow[positieSpeler].Equals(Convert.ToChar("x")))
                    {
                        // Verander in bovenste rij
                        char[] temp = verticalRow.ToCharArray();
                        temp[positieSpeler] = Convert.ToChar("@");
                        tempDoolhof[spelerRow - 1] = new string(temp);

                        // Verander in onderste rij
                        char[] temp2 = row.ToCharArray();
                        temp2[positieSpeler] = Convert.ToChar("x");
                        tempDoolhof[spelerRow] = new string(temp2);
                    }

                    // Staat er een doos?
                    if (verticalRow[positieSpeler].Equals(Convert.ToChar("o")))
                    {
                        if ((spelerRow - 2) >= 0 )// Voorkom out of bounds
                        {
                            secondVerticalRow = tempDoolhof[spelerRow - 2];

                            // Kan de doos bewegen?
                            if (secondVerticalRow[positieSpeler].Equals(Convert.ToChar(" ")) || secondVerticalRow[positieSpeler].Equals(Convert.ToChar("x")))
                            {
                                // Beweeg doos

                                // Verander in bovenste rij
                                char[] temp = secondVerticalRow.ToCharArray();
                                temp[positieSpeler] = Convert.ToChar("o");
                                tempDoolhof[spelerRow - 2] = new string(temp);

                                // Verander in middelste rij
                                char[] temp2 = verticalRow.ToCharArray();
                                temp2[positieSpeler] = Convert.ToChar("@");
                                tempDoolhof[spelerRow - 1] = new string(temp2);

                                // Verander in onderste rij
                                char[] temp3 = row.ToCharArray();
                                temp3[positieSpeler] = Convert.ToChar(" ");
                                tempDoolhof[spelerRow] = new string(temp3);
                            }
                        }
                    }

                    break;

                case Key.Down:
                    richting = "beneden"; 

                    // Vind de rij van de speler
                    row = tempDoolhof[VindSpeler()];
                    spelerRow = VindSpeler();
                    verticalRow = tempDoolhof[spelerRow + 1];

                    // Vind de positie van de speler
                    positieSpeler = row.IndexOf("@");

                    // Vind de nieuwe positie
                    if (verticalRow[positieSpeler].Equals(Convert.ToChar(" ")))
                    {
                        // Verander in bovenste rij
                        char[] temp = row.ToCharArray();
                        temp[positieSpeler] = Convert.ToChar(" ");
                        tempDoolhof[spelerRow] = new string(temp);

                        // Verander in onderste rij
                        char[] temp2 = verticalRow.ToCharArray();
                        temp2[positieSpeler] = Convert.ToChar("@");
                        tempDoolhof[spelerRow + 1] = new string(temp2);
                    }

                    if (verticalRow[positieSpeler].Equals(Convert.ToChar("x")))
                    {
                        // Verander in bovenste rij
                        char[] temp = row.ToCharArray();
                        temp[positieSpeler] = Convert.ToChar("x");
                        tempDoolhof[spelerRow] = new string(temp);

                        // Verander in onderste rij
                        char[] temp2 = verticalRow.ToCharArray();
                        temp2[positieSpeler] = Convert.ToChar("@");
                        tempDoolhof[spelerRow + 1] = new string(temp2);
                    }

                    // Staat er een doos?
                    if (verticalRow[positieSpeler].Equals(Convert.ToChar("o")))
                    {
                        if ((spelerRow + 2) <= tempDoolhof.Count)// Voorkom out of bounds
                        {
                            secondVerticalRow = tempDoolhof[spelerRow + 2];

                            // Kan de doos bewegen?
                            if (secondVerticalRow[positieSpeler].Equals(Convert.ToChar(" ")) || secondVerticalRow[positieSpeler].Equals(Convert.ToChar("x")))
                            {
                                // Beweeg doos

                                // Verander in onderste rij
                                char[] temp = secondVerticalRow.ToCharArray();
                                temp[positieSpeler] = Convert.ToChar("o");
                                tempDoolhof[spelerRow + 2] = new string(temp);

                                // Verander in middelste rij
                                char[] temp2 = verticalRow.ToCharArray();
                                temp2[positieSpeler] = Convert.ToChar("@");
                                tempDoolhof[spelerRow + 1] = new string(temp2);

                                // Verander in bovenste rij
                                char[] temp3 = row.ToCharArray();
                                temp3[positieSpeler] = Convert.ToChar(" ");
                                tempDoolhof[spelerRow] = new string(temp3);
                            }

                        }
                    }

                    break;

                default:
                    return;
            }

            // Update doolhof
            doolhof = tempDoolhof;

            // Refresh TopBord
            this.SpeelBord.Children.Clear();
            TopBord topBord = new TopBord(doolhof, richting);
            this.SpeelBord.Children.Add(topBord.toonGrid());

            // Heeft Gewonnen?
            HeeftGewonnen();
        }

        public int VindSpeler()
        {
            for(int i = 0; i < doolhof.Count; i++)
            {
                foreach (char c in doolhof[i])
                {
                    if(c.Equals(Convert.ToChar("@")))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public void HeeftGewonnen()
        {
            // Als er geen x'en meer in het doolhof zitten, heeft de speler gewonnen
            int xcount = 0;

            for (int i = 0; i < doolhof.Count; i++)
            {
                foreach (char c in doolhof[i])
                {
                    if (c.Equals(Convert.ToChar("x")))
                    {
                        xcount++;
                    }
                }
            }

            if (xcount == 0)
            {
                MessageBox.Show("Je hebt 16K gewonnen! Like a G6");
            }   
        }

    }
}
