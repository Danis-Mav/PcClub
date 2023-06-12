using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PcClub.DB;

namespace PcClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlacesPage.xaml
    /// </summary>
    public partial class PlacesPage : Page
    {
        public static ObservableCollection<Place> places { get; set; }
        private Place selectedPlace;
        
        public PlacesPage()
        {
            
            InitializeComponent();
            LoadPlaces();

        }
        private void lvPlaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlace = lvPlaces.SelectedItem as Place;
        }

        private void LoadPlaces()
        {
            using (var db = new PcClubEntities())
            {
                places = new ObservableCollection<Place>(DBConnection.connection.Place.Where(p => p.IsDeleted != true).ToList());
                this.DataContext = this;
            }
        }

        private void AddPlace_Click(object sender, RoutedEventArgs e)
        {
            selectedPlace = null;
            NavigationService.Navigate(new AddEditPlacePage(selectedPlace));
        }

        private void EditPlace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPlacePage(selectedPlace));
        }

        private void DeletePlace_Click(object sender, RoutedEventArgs e)
        {
            if (lvPlaces.SelectedItem != null)
            {
                Place selectedPlace = lvPlaces.SelectedItem as Place;
                selectedPlace.IsDeleted = true;
                DBConnection.connection.SaveChanges();
                LoadPlaces();
            }
        }
    }
}
