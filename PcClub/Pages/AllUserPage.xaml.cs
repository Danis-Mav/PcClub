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
    /// Логика взаимодействия для AllUserPage.xaml
    /// </summary>
    public partial class AllUserPage : Page
    {
        public AllUserPage()
        {
            InitializeComponent();

            List<User> users = DBConnection.connection.User.ToList();
            lvUsers.ItemsSource = users;

            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            List<User> filteredUsers = DBConnection.connection.User.Where(u =>
                u.Email.ToLower().Contains(searchText) ||
                u.FullName.ToLower().Contains(searchText)).ToList();

            lvUsers.ItemsSource = filteredUsers;
        }

        private void btnAddUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewUserPage());
        }
        private void userListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User selectedUser = (User)lvUsers.SelectedItem;
            if (selectedUser != null)
            {
                UserDetailsPage userDetailsPage = new UserDetailsPage(selectedUser);
                NavigationService.Navigate(userDetailsPage);
            }
        }
    }
}
