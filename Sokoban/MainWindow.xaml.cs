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
        List<string> doolhof = null;

        public List<string> Doolhof
        {
            get { return doolhof; }
            set { doolhof = value; }
        }

        List<string> originalDoolhof;

        public List<string> OriginalDoolhof
        {
            get { return originalDoolhof; }
            set { originalDoolhof = value; }
        }

        Highscores _highscores;
        DispatcherTimer timer;
        private int _seconden = 0;
        private int _zetten = 0;
        private int _maximum = 0;

        public void ResetZetten()
        {
            _zetten = 0;
        }

        public MainWindow()
        {
            // Initializen
            InitializeComponent();
            Timer(); // Timer
            //highscores = new Highscores();
        }

        public void createGrid(List<string> doolhof)
        {
            CloseLevel();
            BottomBord bottomBord = new BottomBord(doolhof, "");
            TopBord topBord = new TopBord(doolhof, "");

            this.VakkenView.Children.Add(bottomBord.toonGrid());
            this.SpeelBord.Children.Add(topBord.toonGrid());

            ResetTimer();
            StartTimer();
            verstrekenTijd.Content = "Tijd: " + _seconden;
            aantalZetten.Content = "Zetten: " + _zetten;
        }

        public void readLevel(string level)
        {
            doolhof = new List<string>();
            originalDoolhof = new List<string>();

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
                    originalDoolhof.Add(line);
                }
            }

            // Is dit gelijk aan het maximum?
            for (int i = 0; i < originalDoolhof.Count; i++)
            {
                foreach (char c in originalDoolhof[i])
                {
                    if (c.Equals(Convert.ToChar("x")))
                    {
                        _maximum++;
                    }
                }
            }
        }

        public void readHighscores(string highscores)
        {
            _highscores = new Highscores();

            Assembly thisExe = Assembly.GetExecutingAssembly();
            string path = thisExe.Location;
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            string folderName = dirinfo.Parent.FullName;
            path = folderName + "/Highscores/" + highscores + ".txt";

            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //_highscores.Add(line);
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

        public void StopTimer()
        {
            timer.Stop(); // timer stoppen
        }

        public void ResetTimer()
        {
            StopTimer(); // timer stoppen
            _seconden = 0; // seconden op 0 zetten
        }

        // Zolang de timer loopt wordt het elke seconde met 1 opgehoogd
        public void Each_Tick(object sender, EventArgs e)
        {
            _seconden = _seconden + 1;
            verstrekenTijd.Content = "Tijd: " + _seconden; // verstrekenTijd label updaten
        }

        public void CloseLevel()
        {
            this.VakkenView.Children.Clear(); // VakkenView Grid leeggooien
            this.SpeelBord.Children.Clear(); // SpeelBord Grid leeggooien
            verstrekenTijd.Content = ""; // timer label leegmaken
            ResetTimer(); // Timer resetten
            aantalZetten.Content = ""; // zetten label leegmaken
            doolhof = null;
            originalDoolhof = null;
            _maximum = 0;
        }

        private void CloseLevelButton(object sender, RoutedEventArgs e)
        {
            CloseLevel(); // Level sluiten
        }

        // Key Events
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (doolhof != null)
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
                        spelerRow = VindSpeler();
                        positieSpeler = row.IndexOf("@");

                        // Vind de nieuwe positie
                        if (row[positieSpeler - 1].Equals(Convert.ToChar(" ")) || row[positieSpeler - 1].Equals(Convert.ToChar("x")))
                        {
                            char[] temp = row.ToCharArray();
                            temp[positieSpeler - 1] = Convert.ToChar("@");
                            temp[positieSpeler] = Convert.ToChar(" ");

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
                        spelerRow = VindSpeler();
                        positieSpeler = row.IndexOf("@");

                        // Vind de nieuwe positie
                        if (row[positieSpeler + 1].Equals(Convert.ToChar(" ")) || row[positieSpeler + 1].Equals(Convert.ToChar("x")))
                        {
                            char[] temp = row.ToCharArray();
                            temp[positieSpeler + 1] = Convert.ToChar("@");
                            temp[positieSpeler] = Convert.ToChar(" ");

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
                        if (verticalRow[positieSpeler].Equals(Convert.ToChar(" ")) || verticalRow[positieSpeler].Equals(Convert.ToChar("x")))
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

                        // Staat er een doos?
                        if (verticalRow[positieSpeler].Equals(Convert.ToChar("o")))
                        {
                            if ((spelerRow - 2) >= 0)// Voorkom out of bounds
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

                        // Vind de positie van de 
                        positieSpeler = row.IndexOf("@");

                        // Vind de nieuwe positie
                        if (verticalRow[positieSpeler].Equals(Convert.ToChar(" ")) || verticalRow[positieSpeler].Equals(Convert.ToChar("x")))
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

                                    // Verander in onderste rij
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
            int xcounter = 0;

            // Vergelijk de dozen in het doolhof met de x'en in de statische
            for (int i = 0; i < doolhof.Count; i++)
            {
                int j = 0;

                foreach (char c in doolhof[i])
                {
                    if (c.Equals(Convert.ToChar("o")))
                    {
                        // Character in het origineel?
                        string tempRow = originalDoolhof[i];
                        if (tempRow[j].Equals(Convert.ToChar("x")))
                        {
                            xcounter++;
                        }
                    }
                    j++;
                }
            }

            if (xcounter == _maximum)
            {
                MessageBox.Show("Je hebt 16K gewonnen! Like a G6");
                CloseLevel();
            }
        }

        private void LevelHighscoresButton(object sender, RoutedEventArgs e)
        {
            readHighscores("highscores1");
        }

        private void Level2(object sender, RoutedEventArgs e)
        {
            readLevel("Doolhof2");
            createGrid(doolhof);
        }

        private void Level3(object sender, RoutedEventArgs e)
        {
            readLevel("Doolhof3");
            createGrid(doolhof);
        }

        private void Level4(object sender, RoutedEventArgs e)
        {
            readLevel("Doolhof4");
            createGrid(doolhof);
        }

        private void Level5(object sender, RoutedEventArgs e)
        {
            readLevel("Doolhof5");
            createGrid(doolhof);
        }

        private void Level6(object sender, RoutedEventArgs e)
        {
            readLevel("Doolhof6");
            createGrid(doolhof);
        }

    }
}
