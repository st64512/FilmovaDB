using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmovaDB.Model;
using FilmovaDB.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FilmovaDB.ViewModel
{
    public class MovieViewModel : ObservableObject
    {
        private readonly MovieRepository movieRepository;

        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand SaveCommand { get; }
        public IRelayCommand<string> SearchCommand { get; }

        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
        
        private Movie? selectedMovie;
        public Movie? SelectedMovie { 
            get => selectedMovie;
            set
            {
                SetProperty(ref selectedMovie, value);
                RemoveCommand.NotifyCanExecuteChanged();
            }
        }

        new public event PropertyChangedEventHandler? PropertyChanged;

        public MovieViewModel(MovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;

            AddCommand = new RelayCommand(DoAdd);
            RemoveCommand = new RelayCommand(DoRemove, () => SelectedMovie != null);
            SaveCommand = new RelayCommand(DoSave);
            SearchCommand = new RelayCommand<string>(DoSearch);
        }

        public void LoadMovies() 
        {
            var data =  movieRepository.GetAll();

            Movies = new ObservableCollection<Movie>(data);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Movies)));
        }

        private void DoSave() 
        {
            movieRepository.Update(selectedMovie);
        }

        private void DoSearch(string? searchQuery)
        {
            var collection = CollectionViewSource.GetDefaultView(Movies);

            if (!string.IsNullOrWhiteSpace(searchQuery))
                collection.Filter = (m) => ((Movie)m).Name.ToLower().Contains(searchQuery.ToLower());
            else
                collection.Filter = null;
        }

        private void DoRemove() 
        {
            if (SelectedMovie != null) {
                movieRepository.Delete(SelectedMovie.Id);
                SelectedMovie = null;
                OnPropertyChanged("Movies");
            }
        }

        private void DoAdd()
        {
            var movie = new Movie();
            movieRepository.Insert(movie);
            SelectedMovie = movie;
            OnPropertyChanged("Movies");
        }
    }
}
