using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmovaDB.Model;
using FilmovaDB.MovieEnums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmovaDB.ViewModel
{
    public class SelectedMovieViewModel : ObservableObject
    {
        private Movie? sMovie;
        public Movie? SMovie { 
            get => sMovie;
            set 
            {
                SetProperty(ref sMovie, value);
            }
        }

        private Genre? selectedGenre;

        public IRelayCommand RemoveGenreCommand { get; }

        public Genre? SelectedGenre {
            get => selectedGenre;
            set
            {
                SetProperty(ref selectedGenre, value);
            }
        }

        public SelectedMovieViewModel(Movie? selectedMovie) 
        {
            this.SMovie = selectedMovie;
            RemoveGenreCommand = new RelayCommand(DoRemoveGenre);
        }

        private void DoRemoveGenre()
        {
            if (SMovie != null) {
                if (SelectedGenre != null) {
                    SMovie.Genres.Remove((Genre)SelectedGenre);
                    SelectedGenre = null;
                }
            }
        }
    }
}
