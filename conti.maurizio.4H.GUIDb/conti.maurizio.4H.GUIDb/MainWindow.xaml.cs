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

using SQLite;

namespace conti.maurizio._4H.GUIDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cn1 = new SQLiteConnection("chinook.db");

            List<Artist> tblArtists;

            // Prende tutti gli artisti
            tblArtists = cn1.Query<Artist>("select * from artists where name like 'a%'");
            dgDati.ItemsSource = tblArtists;

        }
    }
}
