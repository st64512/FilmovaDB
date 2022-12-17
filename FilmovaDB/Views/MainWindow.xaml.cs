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
            var movieRepository = new MovieRepository();

            /*
            var actorRepository = new ActorRepository();
            Actor a = new Actor { Name = "Pepa" };
            Actor b = new Actor { Name = "Ema" };
            actorRepository.Insert(a);
            actorRepository.Insert(b);

            List<Actor> actors = new List<Actor> { a, b };

            movieService.Insert(new Movie { Name = "Honba za pokladem", Actors = actors });

            var results = movieService.GetAll();

            Actor actor = results[0].Actors[0];
            */

            InitializeComponent();
            FilmovaDB.ViewModel.MainWindowViewModel mainWindowViewModelObject = new ViewModel.MainWindowViewModel(movieRepository);
            mainWindowViewModelObject.LoadMovies();

            DataContext = mainWindowViewModelObject;
        }
    }
}
