using PcClub.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace PcClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookedPage.xaml
    /// </summary>
    public partial class BookedPage : Page
    {
        public BookedPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadBookedPlaces();
        }

        public ObservableCollection<Booking> BookedPlaces { get; set; }

        private void LoadBookedPlaces()
        {
            using (var db = new PcClubEntities())
            {
                BookedPlaces = new ObservableCollection<Booking>(DBConnection.connection.Booking.Where(booking => (bool)booking.IsDelete != true).ToList());
                this.DataContext = this;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
