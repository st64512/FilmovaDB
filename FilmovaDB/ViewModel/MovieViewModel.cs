using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmovaDB.Model;
using FilmovaDB.MovieEnums;
using FilmovaDB.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace FilmovaDB.ViewModel
{
    public class MovieViewModel : ObservableObject
    {
        private readonly MovieRepository movieRepository;
        private List<Genre> genres;

        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand SaveCommand { get; }
        public IRelayCommand<string> SearchCommand { get; }
        public IRelayCommand ClearFormCommand { get; }

        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
        
        private SelectedMovieViewModel selectedMovieVM;
        public SelectedMovieViewModel SelectedMovieVM 
        {
            get => selectedMovieVM;
            set {
                selectedMovieVM = value;
            }
        }
        public Movie? SelectedMovie { 
            get => selectedMovieVM?.SMovie;
            set
            {
                selectedMovieVM.SMovie = value;
                RemoveCommand.NotifyCanExecuteChanged();
                AddCommand.NotifyCanExecuteChanged();
                OnPropertyChanged("SelectedMovieVM");
            }
        }

        public MovieViewModel(MovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
            this.genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            this.selectedMovieVM = new SelectedMovieViewModel(null);
            AddCommand = new RelayCommand(DoAdd, () => SelectedMovie?.Id == 0);
            RemoveCommand = new RelayCommand(DoRemove, () => SelectedMovie != null && SelectedMovie?.Id != 0);
            SaveCommand = new RelayCommand(DoSaveMovie);
            SearchCommand = new RelayCommand<string>(DoSearch);
            ClearFormCommand = new RelayCommand(DoClearForm);

            LoadMovies();
        }

        public void LoadMovies() 
        {
            var data =  movieRepository.GetAll();

            Movies = new ObservableCollection<Movie>(data);

            OnPropertyChanged("Movies");
        }

        private void DoSaveMovie() 
        {
            foreach (var movie in Movies)
            {
                movieRepository.Update(movie);
            }
            LoadMovies();
        }

        private void DoSearch(string? searchQuery)
        {
            var collection = CollectionViewSource.GetDefaultView(Movies);

            if (!string.IsNullOrWhiteSpace(searchQuery))
                collection.Filter = (m) => ((Movie)m).Name != null ? ((Movie)m).Name.ToLower().Contains(searchQuery.ToLower()) : false;
            else
                collection.Filter = null;
        }

        private void DoRemove() 
        {
            if (SelectedMovie != null) {
                movieRepository.Delete(SelectedMovie.Id);
                SelectedMovie = null;
                LoadMovies();
            }
        }

        private void DoAdd()
        {
            if (SelectedMovie != null)
            {
                /*
                Actor a = new Actor { Name = "Franta", Surname="Pytlák" };
                Actor b = new Actor { Name = "Majda", Surname = "Vdolečka" };

                var ac = new ActorRepository();
                ac.Insert(a);
                ac.Insert(b);
                SelectedMovie.Actors = new List<Actor> { a, b };

                SelectedMovie.Genres = new List<Genre> { Genre.Western, Genre.Action, Genre.Horror};
                */

                movieRepository.Insert(SelectedMovie);
                LoadMovies();
            }
        }

        private void DoClearForm()
        {
            SelectedMovie = new Movie();
        }
    }
}
