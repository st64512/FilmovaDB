using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmovaDB.Interface;
using FilmovaDB.Model;
using FilmovaDB.MovieEnums;
using FilmovaDB.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.ViewModel
{
    public class SelectedMovieViewModel : ObservableObject
    {

        private readonly ActorRepository actorRepository;
        private readonly DirectorRepository directorRepository;

        private Movie? sMovie;
        public Movie? SMovie { 
            get => sMovie;
            set 
            {
                SetProperty(ref sMovie, value);
                if (value != null)
                {
                    ReloadSMovieCollections();
                }
            }
        }

        public ObservableCollection<Genre> Genres { get; set; } = new ObservableCollection<Genre>();
        public ObservableCollection<Actor> Actors { get; set; } = new ObservableCollection<Actor>();
        public ObservableCollection<Director> Directors { get; set; } = new ObservableCollection<Director>();

        public ObservableCollection<Genre> OtherGenres { get; set; } = new ObservableCollection<Genre>();
        public ObservableCollection<Actor> OtherActors { get; set; } = new ObservableCollection<Actor>();
        public ObservableCollection<Director> OtherDirectors { get; set; } = new ObservableCollection<Director>();

        public IRelayCommand<Object> RemoveGenreCommand { get; }
        public IRelayCommand<Object> AddGenreCommand { get; }
        public IRelayCommand<Actor> RemoveActorCommand { get; }
        public IRelayCommand<Actor> AddActorCommand { get; }
        public IRelayCommand<Director> RemoveDirectorCommand { get; }
        public IRelayCommand<Director> AddDirectorCommand { get; }

        public SelectedMovieViewModel(IGenericRepository<Actor> actorRepository, IGenericRepository<Director> directorRepository) 
        {
            this.actorRepository = (ActorRepository) actorRepository;
            this.directorRepository = (DirectorRepository) directorRepository;
            this.SMovie = new Movie();

            this.OtherGenres = new ObservableCollection<Genre>(Enum.GetValues(typeof(Genre)).Cast<Genre>());
            
            RemoveGenreCommand = new RelayCommand<Object>(DoRemoveGenre);
            AddGenreCommand = new RelayCommand<Object>(DoAddGenre);
            RemoveActorCommand = new RelayCommand<Actor>(DoRemoveActor);
            AddActorCommand = new RelayCommand<Actor>(DoAddActor);
            RemoveDirectorCommand = new RelayCommand<Director>(DoRemoveDirector);
            AddDirectorCommand = new RelayCommand<Director>(DoAddDirector);
        }

        private void ReloadGenres()
        {
            if (SMovie.Genres != null) { 
                Genres = new ObservableCollection<Genre>(SMovie.Genres);
            }
            OnPropertyChanged("Genres");
            ReloadOtherGenres();
        }

        private void ReloadOtherGenres() 
        {
            var allGenresList = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            var diffGenresList = allGenresList.Except(this.Genres.ToList());
            this.OtherGenres = new ObservableCollection<Genre>(diffGenresList);
            OnPropertyChanged("OtherGenres");
        }

        private void ReloadActors()
        {

            if (SMovie.Actors != null)
            {
                Actors = new ObservableCollection<Actor>(SMovie.Actors);
            }
            OnPropertyChanged("Actors");
            ReloadOtherActors();
        }

        private void ReloadOtherActors()
        {
            var allActorsList = actorRepository.GetAll();
            var casc = this.Actors.ToList();
            var diffActorsList = allActorsList.Except(this.Actors.ToList());
            this.OtherActors = new ObservableCollection<Actor>(diffActorsList);
            OnPropertyChanged("OtherActors");
        }

        private void ReloadDirectors()
        {
            if (SMovie.Directors != null)
            {
                Directors = new ObservableCollection<Director>(SMovie.Directors);
            }
            OnPropertyChanged("Directors");
            ReloadOtherDirectors();
            
        }

        private void ReloadOtherDirectors()
        {
            var allDirectorsList = directorRepository.GetAll();
            var diffDirectorsList = allDirectorsList.Except(this.Directors.ToList());
            this.OtherDirectors = new ObservableCollection<Director>(diffDirectorsList);
            OnPropertyChanged("OtherDirectors");
        }

        private void ReloadSMovieCollections()
        {
            ReloadGenres();
            ReloadActors();
            ReloadDirectors();
        }

        private void DoRemoveGenre(Object genre)
        {
            if (SMovie != null) {
                SMovie.Genres.Remove((Genre)genre);
            }
            ReloadGenres();
        }

        private void DoAddGenre(Object genre)
        {
            if (genre != null && SMovie != null)
            {
                SMovie.Genres.Add((Genre)genre);
            }
            ReloadGenres();
        }

        private void DoAddActor(Actor actor)
        {
            if (SMovie != null) {
                SMovie.Actors.Add((Actor)actor);
            }
            ReloadActors();
        }

        private void DoRemoveActor(Actor actor)
        {
            if (SMovie != null)
            {
                SMovie.Actors.Remove(actor);
            }
            ReloadActors();
        }

        private void DoAddDirector(Director director)
        {
            if (SMovie != null)
            {
                SMovie.Directors.Add((Director)director);
            }
            ReloadDirectors();
        }

        private void DoRemoveDirector(Director director)
        {
            if (SMovie != null)
            {
                SMovie.Directors.Remove(director);
            }
            ReloadDirectors();
        }
    }
}
