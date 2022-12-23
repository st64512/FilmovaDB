using FilmovaDB.ViewModel;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Shapes;

namespace FilmovaDB.Views
{
    /// <summary>
    /// Interakční logika pro DirectorDialog.xaml
    /// </summary>
    public partial class DirectorDialog : Window
    {
        public DirectorDialog()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<DirectorViewModel>();
        }
    }
}
