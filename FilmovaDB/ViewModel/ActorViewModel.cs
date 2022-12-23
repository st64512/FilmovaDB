using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmovaDB.Interface;
using FilmovaDB.Model;
using FilmovaDB.Repository;
using FilmovaDB.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FilmovaDB.ViewModel
{
    public class ActorViewModel : ObservableObject
    {
        private readonly ActorRepository actorRepository;
        private readonly MovieRepository movieRepository;
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand<Window> CloseAndSaveCommand { get; }
        public IRelayCommand<string> SearchCommand { get; }
        public IRelayCommand ClearFormCommand { get; }

        public ObservableCollection<Actor> Actors { get; set; } = new ObservableCollection<Actor>();

        private Actor selectedActor;
        public Actor SelectedActor 
        {
            get => selectedActor;
            set 
            {
                SetProperty(ref selectedActor, value);
                RemoveCommand.NotifyCanExecuteChanged();
                AddCommand.NotifyCanExecuteChanged();
            }
        }

        public ActorViewModel(IGenericRepository<Actor> actorRepository, IGenericRepository<Movie> movieRepository)
        {
            this.actorRepository = (ActorRepository) actorRepository;
            this.movieRepository = (MovieRepository) movieRepository;
            
            AddCommand = new RelayCommand(DoAdd, () => SelectedActor?.Id == 0);
            RemoveCommand = new RelayCommand(DoRemove, () => SelectedActor != null && SelectedActor?.Id != 0);
            SearchCommand = new RelayCommand<string>(DoSearch);
            ClearFormCommand = new RelayCommand(DoClearForm);
            CloseAndSaveCommand = new RelayCommand<Window>(DoCloseAndSave);

            LoadActors();
        }

        public void LoadActors()
        {
            var data = actorRepository.GetAll();

            Actors = new ObservableCollection<Actor>(data);
            DoClearForm();

            OnPropertyChanged("Actors");
        }

        private void DoSearch(string? searchQuery)
        {
            var collection = CollectionViewSource.GetDefaultView(Actors);

            if (!string.IsNullOrWhiteSpace(searchQuery))
                collection.Filter = (a) => ((Actor)a).FullName.ToLower().Contains(searchQuery.ToLower());
            else
                collection.Filter = null;

            DoClearForm();
        }

        private void DoAdd() 
        {
            if (SelectedActor != null)
            {
                Actors.Add(SelectedActor);
                OnPropertyChanged("Actors");
            }
            DoClearForm();
        }

        private void DoRemove()
        {
            if (SelectedActor != null) 
            {
                Actors.Remove(SelectedActor);
                OnPropertyChanged("Actors");
            }
            DoClearForm();
        }

        private void DoClearForm() 
        {
            SelectedActor = new Actor();
        }

        private void DoCloseAndSave(Window window)
        {
            var beforeCommitActors = actorRepository.GetAll();
            var deletedActors = Utility<Actor>.utilityExcept(beforeCommitActors, Actors.ToList());
            var addedActors = Utility<Actor>.utilityExcept(Actors.ToList(), beforeCommitActors);
            var otherActors = Utility<Actor>.utilityExcept( Utility<Actor>.utilityExcept(Actors.ToList(), deletedActors), addedActors);

            var allMovies = movieRepository.GetAll();

            foreach (var actor in deletedActors)
            {
                foreach (var movie in allMovies)
                {
                    if (movie.Actors.Remove(actor))
                    { 
                        movieRepository.Update(movie);
                    }
                }
                actorRepository.Delete(actor.Id);
            }

            foreach (var actor in addedActors) {
                actorRepository.Insert(actor);
            }

            foreach (var actor in otherActors)
            {
                actorRepository.Update(actor);
            }
            
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
