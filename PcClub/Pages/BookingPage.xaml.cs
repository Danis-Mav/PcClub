using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Threading;
using PcClub.DB;

namespace PcClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
        private Place selectedPlace;
       
        private User selectedUser;
        private DateTime bookingEndTime;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public ObservableCollection<Place> Table { get; set; }

        public BookingPage()
        {
            InitializeComponent();
            LoadData();
            ShowTable();
            cmbPlace.SelectionChanged += cmbPlace_SelectionChanged;
            LViewTable.SelectionChanged += LViewTable_SelectionChanged;
        }

        private void LoadData()
        {
            using (var db = new PcClubEntities())
            {
                var users = db.User.Where(u => !db.Booking.Any(booking => booking.IdUser == u.Id && booking.IsDelete != true)).ToList();
                cmbUser.ItemsSource = users;
                cmbUser.DisplayMemberPath = "FullName";
                cmbUser.SelectedValuePath = "Id";

                var places = db.Place.Where(p => p.IsBooking != true).ToList();
                cmbPlace.ItemsSource = places;
                cmbPlace.DisplayMemberPath = "Name";
                cmbPlace.SelectedValuePath = "Id";
            }
        }
        private void ShowTable()
        {

            Table = new ObservableCollection<Place>(DBConnection.connection.Place.Where(x => x.IsDeleted != true).ToList());
            DataContext = this;
            LViewTable.ItemsSource = Table;
            LViewTable.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }
        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
           
        }
       

       

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPlace == null)
            {
                MessageBox.Show("Пожалуйста, выберите место.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(txtHours.Text, out double hours) || hours <= 0 || hours > 24)
            {
                MessageBox.Show("Введите допустимое количество часов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddSeconds(hours);
            bookingEndTime = endDate;

            using (var db = new PcClubEntities())
            {
                // Проверить, свободно ли место на выбранный период времени
                bool isPlaceAvailable = !db.Booking.Any(b => b.IdPlace == selectedPlace.Id && ((startDate >= b.DateTimeStart && startDate < b.DateTimeEnd) || (endDate > b.DateTimeStart && endDate <= b.DateTimeEnd) || (startDate <= b.DateTimeStart && endDate >= b.DateTimeEnd)));

                if (!isPlaceAvailable)
                {
                    MessageBox.Show("Выбранное место уже забронировано на указанный период времени.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                double hour;
                bool isValid = double.TryParse(txtHours.Text, out hour);
                if (isValid)
                {

                }
                else
                {
                    MessageBox.Show("Введите допустимое количество часов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Booking newBooking = new Booking
                {
                    DateTimeStart = startDate,
                    DateTimeEnd = endDate,
                    IdPlace = selectedPlace.Id,
                    IdUser = selectedUser.Id,
                    Hour = hour,
                };
                selectedPlace.IsBooking = true; 
                db.Booking.Add(newBooking);
                db.SaveChanges();

                MessageBox.Show("Бронирование успешно создано.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                CheckBookingStatus();
            }
        }

        private void cmbPlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlace = cmbPlace.SelectedItem as Place;
            CheckBookingStatus();

            // Выбор соответствующего элемента в ListView
            LViewTable.SelectedItem = selectedPlace;
        }

        private void LViewTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlace = LViewTable.SelectedItem as Place;
            CheckBookingStatus();

            // Выбор соответствующего элемента в ComboBox
            cmbPlace.SelectedItem = selectedPlace;
        }

        private void cmbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = cmbUser.SelectedItem as User;
            CheckBookingStatus();
        }

        private void CheckBookingStatus()
        {
            if (selectedPlace != null && selectedUser != null && bookingEndTime != default && DateTime.Now >= bookingEndTime)
            {
                MessageBox.Show($"Booking for place '{selectedPlace.Name}' and user '{selectedUser.Email}' has ended.", "Booking Ended", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BookedPage());
        }
    }
}
