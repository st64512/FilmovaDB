using FilmovaDB.Model;
using FilmovaDB.Repository;
using LiteDB;
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

namespace FilmovaDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var movieService = new MovieService();
            movieService.Insert(new Movie { Name = "Matrix" });
            movieService.Insert(new Movie { Name = "John Wick" });

            var results = movieService.GetMovieRepoSearch("i");
            var anotherResults = movieService.GetMovieRepoSearch("rix");
            InitializeComponent();
            
        }
    }
}
