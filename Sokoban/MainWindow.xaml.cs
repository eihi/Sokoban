using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        Timer timer = new Timer();

        public MainWindow()
        {
            // Initializen
            InitializeComponent();
        }


        public void createGrid(List<string> doolhof)
        {
            Bord bord = new Bord(doolhof);
            this.VakkenView.Children.Add(bord.toonGrid());
            timer.ResetTimer();
            timer.StartTimer();
            verstrekenTijd.Content = "Tijd: " + timer.waarde;
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

        private void tijd(object sender, MouseButtonEventArgs e)
        {
            verstrekenTijd.Content = "Tijd: " + timer.waarde;
        }
    }
}
