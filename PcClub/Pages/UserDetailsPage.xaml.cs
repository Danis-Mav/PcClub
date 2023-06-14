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
using PcClub.DB;

namespace PcClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserDetailsPage.xaml
    /// </summary>
    public partial class UserDetailsPage : Page
    {
        private User selectedUser;
        public UserDetailsPage(User user)
        {
            InitializeComponent();
            selectedUser = user;
            DisplayUserInfo();
            DisplayBookingHistory();
        }
        private void DisplayUserInfo()
        {
            lblFullName.Text = selectedUser.FullName;
            lblEmail.Text = selectedUser.Email;
            lblPhoneNumber.Text = selectedUser.PhoneNumber;
        }

        private void DisplayBookingHistory()
        {
            var bookings = DBConnection.connection.Booking.Where(b => b.IdUser == selectedUser.Id);
            lvBookingHistory.ItemsSource = bookings.ToList();
        }
    }
}
    