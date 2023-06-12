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
using System.Data.Entity.Infrastructure;
using PcClub.DB;


namespace PcClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPlacePage.xaml
    /// </summary>
    public partial class AddEditPlacePage : Page
    {
        private Place selectedPlace;

        public AddEditPlacePage(Place place)
        {
            InitializeComponent();
            this.Title = "Edit Place";
            selectedPlace = place;
            LoadTypes();
            LoadPlaceData();
        }
        private void LoadPlaceData()
        {
            if (selectedPlace != null)
            {
                txtName.Text = selectedPlace.Name;
                cmbType.SelectedValue = selectedPlace.IdType;
            }
        }
        private void LoadTypes()
        {
            using (var db = new PcClubEntities())
            {
                var types = db.Type.ToList();
                cmbType.ItemsSource = types;
                cmbType.DisplayMemberPath = "Name";
                cmbType.SelectedValuePath = "Id";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            int typeId = (int)cmbType.SelectedValue;
            if (selectedPlace == null)
            {
                // Добавление нового элемента в Place
                Place newPlace = new Place
                {
                    Name = name,
                    IdType = typeId,
                    IsBooking = false,
                };
                DBConnection.connection.Place.Add(newPlace);
                
            }
            else
            {
                // Редактирование существующего элемента в Place
                selectedPlace.Name = name;
                selectedPlace.IdType = typeId;
            }
            try
            { 
            DBConnection.connection.SaveChanges();
            NavigationService.Navigate(new PlacesPage());
            }
            catch { }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
