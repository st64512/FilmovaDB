using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmovaDB.Interface;
using FilmovaDB.Model;
using FilmovaDB.MovieEnums;
using FilmovaDB.Repository;
using FilmovaDB.Views;
using Microsoft.Extensions.DependencyInjection;
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

        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand SaveCommand { get; }
        public IRelayCommand<string> SearchCommand { get; }
        public IRelayCommand ClearFormCommand { get; }
        public IRelayCommand OpenActorDialogCommand { get; }
        public IRelayCommand OpenDirectorDialogCommand { get; }

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

        public MovieViewModel(IGenericRepository<Movie> movieRepository)
        {
            this.movieRepository = (MovieRepository) movieRepository;
            this.SelectedMovieVM = App.Current.Services.GetService<SelectedMovieViewModel>();

            AddCommand = new RelayCommand(DoAdd, () => SelectedMovie?.Id == 0);
            RemoveCommand = new RelayCommand(DoRemove, () => SelectedMovie != null && SelectedMovie?.Id != 0);
            SaveCommand = new RelayCommand(DoSaveMovie);
            SearchCommand = new RelayCommand<string>(DoSearch);
            ClearFormCommand = new RelayCommand(DoClearForm);

            OpenActorDialogCommand = new RelayCommand(DoOpenActorDialog);
            OpenDirectorDialogCommand = new RelayCommand(DoOpenDirectorDialog);

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
                DoClearForm();
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
                DoClearForm();
            }
        }

        private void DoAdd()
        {
            if (SelectedMovie != null)
            {
                movieRepository.Insert(SelectedMovie);
                LoadMovies();
            }
        }

        private void DoClearForm()
        {
            SelectedMovie = new Movie();
        }

        private void DoOpenActorDialog() 
        {
            ActorView  actorViewWindow = new ActorView();
            actorViewWindow.ShowDialog();
            this.SelectedMovieVM.ReloadActors();
        }

        private void DoOpenDirectorDialog()
        {
            DirectorDialog directorDialog = new DirectorDialog();
            directorDialog.ShowDialog();
            this.SelectedMovieVM.ReloadDirectors();
        }
    }
}
