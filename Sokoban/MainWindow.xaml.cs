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

namespace Sokoban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int nRows = 7;
            int nCols = 7;
            int CellSize = 40;

            for (int i = 0; i < nCols; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(CellSize);
                VakjesView.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < nRows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(CellSize);
                VakjesView.RowDefinitions.Add(row);
            }

            System.Reflection.Assembly thisExe =
            System.Reflection.Assembly.GetExecutingAssembly();
            string path = thisExe.Location;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            string folderName = dirInfo.Parent.FullName;

            BitmapImage bmpMuur = new BitmapImage(new Uri(folderName + "/Images/muur.png"));
            BitmapImage bmpVloer = new BitmapImage(new Uri(folderName + "/Images/vloer.png"));

            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    Image img = new Image();
                    if (i == 0 || j == 0 || i == nRows - 1 || j == nCols - 1)
                        img.Source = bmpMuur;
                    else
                        img.Source = bmpVloer;

                    img.SetValue(Grid.ColumnProperty, i);
                    img.SetValue(Grid.RowProperty, j);
                    VakjesView.Children.Add(img);
                }
            }
        }
    }
}
