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
using PcClub.Pages;
using PcClub.DB;
using System.Windows.Threading;

namespace PcClub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new BookingPage());
            StartTimer();
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Интервал проверки - 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckBookingStatusRT();
        }

        public void CheckBookingStatusRT()
        {
            using (var db = new PcClubEntities())
            {
                var expiredBookings = db.Booking.Where(b => !(b.IsDelete.HasValue && b.IsDelete.Value) && b.DateTimeEnd <= DateTime.Now).ToList();

                foreach (var booking in expiredBookings)
                {
                    var user = booking.User;
                    var place = booking.Place;

                    DateTime currentDateTime = DateTime.Now;
                    DateTime bookingEndDateTime = booking.DateTimeEnd.Value;

                    if (bookingEndDateTime.CompareTo(currentDateTime) <= 0)
                    {
                        MessageBox.Show($"У пользователя {user.FullName}, который бронировал стол {place.Name}, закончилось время бронирования.", "Завершение бронирования", MessageBoxButton.OK, MessageBoxImage.Information);
                        booking.IsDelete = true;
                        place.IsBooking = false;
                    }
                }

                db.SaveChanges();
            }
        }
        private void ToggleMenuVisibility(object sender, RoutedEventArgs e)
        {
            if (menuPanel.Visibility == Visibility.Collapsed)
                menuPanel.Visibility = Visibility.Visible;
            else
                menuPanel.Visibility = Visibility.Collapsed;
        }
        private void NavigateToBookingPage()
        {
            BookingPage bookingPage = new BookingPage();
            MainFrame.Navigate(bookingPage);
        }

        private void NavigateToPlacesPage()
        {
            PlacesPage placesPage = new PlacesPage();
            MainFrame.Navigate(placesPage);
        }

        private void NavigateToAllUserPage()
        {
            AllUserPage allUserPage = new AllUserPage();
            MainFrame.Navigate(allUserPage);
        }
        private void NavigateToEventPage()
        {
            EventPage  eventPage = new EventPage();
            MainFrame.Navigate(eventPage);
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToBookingPage();
        }

        private void PlacesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPlacesPage();
        }


        private void AllUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToAllUserPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateToEventPage();
        }
    }
}