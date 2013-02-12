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
using System.IO;

namespace Sokoban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt;
        private int _seconden = 0;

        public MainWindow()
        {
            // Initializen
            InitializeComponent();

            // Timer
            Timer();
        }


        public void createGrid(List<string> doolhof)
        {
            Bord bord = new Bord(doolhof);
            this.VakkenView.Children.Add(bord.toonGrid());
            ResetTimer();
            StartTimer();
            verstrekenTijd.Content = "Tijd: " + _seconden;
        }

        public List<string> readLevel(string level)
        {
            List<string> doolhof = new List<string>();

            System.Reflection.Assembly thisExe = System.Reflection.Assembly.GetExecutingAssembly();
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
            dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 1); // 1 seconde
            dt.Tick += new EventHandler(Each_Tick);
        }

        public void StartTimer() 
        {
            dt.Start();
        }

        public void ResetTimer()
        {
            _seconden = 0;
        }

        // Zolang de timer loopt wordt het elke seconde met 1 opgehoogd
        public void Each_Tick(object sender, EventArgs e)
        {
            verstrekenTijd.Content = "Tijd: " + _seconden++;
        }
    }
}
