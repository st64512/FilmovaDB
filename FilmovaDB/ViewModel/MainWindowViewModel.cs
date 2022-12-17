using FilmovaDB.Model;
using FilmovaDB.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly MovieRepository movieRepository;
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel(MovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public void LoadMovies() 
        {
            var data =  movieRepository.GetAll();

            Movies = new ObservableCollection<Movie>(data);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Movies)));
        }
    }
}
