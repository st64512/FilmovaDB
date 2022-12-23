using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmovaDB.Interface;
using FilmovaDB.Model;
using FilmovaDB.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FilmovaDB.ViewModel
{
    public class DirectorViewModel : ObservableObject
    {
        private readonly DirectorRepository directorRepository;
        private readonly MovieRepository movieRepository;
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand<Window> CloseAndSaveCommand { get; }
        public IRelayCommand<string> SearchCommand { get; }
        public IRelayCommand ClearFormCommand { get; }

        public ObservableCollection<Director> Directors { get; set; } = new ObservableCollection<Director>();

        private Director selectedDirector;
        public Director SelectedDirector
        {
            get => selectedDirector;
            set
            {
                SetProperty(ref selectedDirector, value);
                RemoveCommand.NotifyCanExecuteChanged();
                AddCommand.NotifyCanExecuteChanged();
            }
        }

        public DirectorViewModel(IGenericRepository<Director> directorRepository, IGenericRepository<Movie> movieRepository)
        {
            this.directorRepository = (DirectorRepository)directorRepository;
            this.movieRepository = (MovieRepository)movieRepository;

            AddCommand = new RelayCommand(DoAdd, () => SelectedDirector?.Id == 0);
            RemoveCommand = new RelayCommand(DoRemove, () => SelectedDirector != null && SelectedDirector?.Id != 0);
            SearchCommand = new RelayCommand<string>(DoSearch);
            ClearFormCommand = new RelayCommand(DoClearForm);
            CloseAndSaveCommand = new RelayCommand<Window>(DoCloseAndSave);

            LoadDirectors();
        }

        public void LoadDirectors()
        {
            var data = directorRepository.GetAll();

            Directors = new ObservableCollection<Director>(data);
            DoClearForm();

            OnPropertyChanged("Directors");
        }

        private void DoSearch(string? searchQuery)
        {
            var collection = CollectionViewSource.GetDefaultView(Directors);

            if (!string.IsNullOrWhiteSpace(searchQuery))
                collection.Filter = (a) => ((Director)a).FullName.ToLower().Contains(searchQuery.ToLower());
            else
                collection.Filter = null;

            DoClearForm();
        }

        private void DoAdd()
        {
            if (SelectedDirector != null)
            {
                Directors.Add(SelectedDirector);
                OnPropertyChanged("Directors");
            }
            DoClearForm();
        }

        private void DoRemove()
        {
            if (SelectedDirector != null)
            {
                Directors.Remove(SelectedDirector);
                OnPropertyChanged("Directors");
            }
            DoClearForm();
        }

        private void DoClearForm()
        {
            SelectedDirector = new Director();
        }

        private void DoCloseAndSave(Window window)
        {
            var beforeCommitDirectors = directorRepository.GetAll();
            var deletedDirectors = beforeCommitDirectors.Except(Directors);
            var addedDirectors = Directors.Except(beforeCommitDirectors);
            var otherDirectors = Directors.Except(deletedDirectors).Except(addedDirectors);

            var allMovies = movieRepository.GetAll();

            foreach (var director in deletedDirectors)
            {
                foreach (var movie in allMovies)
                {
                    if (movie.Directors.Remove(director))
                    {
                        movieRepository.Update(movie);
                    }
                }
                directorRepository.Delete(director.Id);
            }

            foreach (var director in addedDirectors)
            {
                directorRepository.Insert(director);
            }

            foreach (var director in otherDirectors)
            {
                directorRepository.Update(director);
            }

            if (window != null)
            {
                window.Close();
            }
        }
    }
}
