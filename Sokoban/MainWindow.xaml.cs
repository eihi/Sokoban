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
        DispatcherTimer timer;
        private int _seconden = 0;

        public MainWindow()
        {
            // Initializen
            InitializeComponent();
            Timer(); // Timer
        }


        public void createGrid(List<string> doolhof)
        {
            BottomBord bottomBord = new BottomBord(doolhof);
            TopBord topBord = new TopBord(doolhof);

            this.VakkenView.Children.Add(bottomBord.toonGrid());
            this.VakkenView.Children.Add(topBord.toonGrid());

            ResetTimer();
            StartTimer();
            verstrekenTijd.Content = "Tijd: " + _seconden;
        }

        public List<string> readLevel(string level)
        {
            List<string> doolhof = new List<string>();

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

            return doolhof;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            createGrid(readLevel("Doolhof1"));
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
        }

        private void CloseLevelButton(object sender, RoutedEventArgs e)
        {
            CloseLevel(); // Level sluiten
        }
    }
}
