using FilmovaDB.Model;
using FilmovaDB.Repository;
using FilmovaDB.Services;
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
            /*
            var x = movieService.GetAll();
            x.Wait();
            */

            var m = new Movie { Name = "John Wick" };

            movieService.Insert(m);

            var results = movieService.GetBySearch("i");
            var anotherResults = movieService.GetBySearch("rix");

            m.Name = "PEpa";

            movieService.Update(m);

            results = movieService.GetAll();

            movieService.Delete(1);
            movieService.Delete(2);

            results = movieService.GetAll();


            InitializeComponent();
        }
    }
}
