using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FilmovaDB.Interface;
using FilmovaDB.Model;
using FilmovaDB.Repository;
using FilmovaDB.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace FilmovaDB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public new static App Current => (App)Application.Current;

        public App() 
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IGenericRepository<Movie>, MovieRepository>();
            services.AddSingleton<IGenericRepository<Actor>, ActorRepository>();
            services.AddSingleton<IGenericRepository<Director>, DirectorRepository>();

            services.AddTransient<SelectedMovieViewModel>();
            services.AddTransient<MovieViewModel>();
            services.AddTransient<ActorViewModel>();
            services.AddTransient<DirectorViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
