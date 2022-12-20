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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmovaDB.Views
{
    /// <summary>
    /// Interakční logika pro MovieView.xaml
    /// </summary>
    public partial class MovieView : UserControl
    {
        public MovieView()
        {
            InitializeComponent();
        }

        private void Genres_ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;

            for (var rowIndex = 0; rowIndex < genresListView.SelectedItems.Count; rowIndex++)
            {
                genresListView.Items.Remove(genresListView.SelectedItems[rowIndex]);
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var mydata = this.DataContext;
            var pepa = mydata;
        }
    }
}
