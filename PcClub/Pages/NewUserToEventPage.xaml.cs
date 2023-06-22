using PcClub.DB;
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

namespace PcClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewUserToEventPage.xaml
    /// </summary>
    public partial class NewUserToEventPage : Page
    {
        private Event selectedEvent;
        private List<User> users;


        public NewUserToEventPage(Event selectedEvent)
        {
            InitializeComponent();
            this.selectedEvent = selectedEvent;
            LoadUsers();
            LoadEvent();
            LoadEventUsers();
        }
        private void LoadEventUsers()
        {
            if (selectedEvent != null)
            {
                using (var db = new PcClubEntities())
                {
                    var eventUsers = db.EventUser
                        .Where(u => u.IdEvent == selectedEvent.Id)
                        .Select(u => u.User.FullName)
                        .ToList();
                    lstEventUsers.ItemsSource = eventUsers;
                }
            }
        }
        private void LoadUsers()
        {
            using (var db = new PcClubEntities())
            {
                users = db.User.ToList();
                if (users != null)
                {
                    cmbUsers.ItemsSource = users;
                    cmbUsers.DisplayMemberPath = "FullName";
                    cmbUsers.SelectedValuePath = "Id";
                    cmbUsers.Items.Refresh();
                }
            }
        }

        private void LoadEvent()
        {
            selectedEvent = GetSelectedEvent();
        }

        private Event GetSelectedEvent()
        {
             return selectedEvent;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEvent != null)
            {
                User selectedUser = (User)cmbUsers.SelectedItem;
                if (selectedUser != null)
                {
                    
                    using (var db = new PcClubEntities())
                    {
                        bool isUserAlreadyRegistered = db.EventUser
                    .Any(u => u.IdEvent == selectedEvent.Id && u.IdUser == selectedUser.Id);

                        if (isUserAlreadyRegistered)
                        {
                            MessageBox.Show("Данный пользователь уже участвует в этом событии.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        EventUser eventUser = new EventUser
                        {

                            IdEvent = selectedEvent.Id,
                            IdUser = selectedUser.Id
                        };

                        db.EventUser.Add(eventUser);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadEventUsers();
        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEvent != null)
            {
                User selectedUser = (User)cmbUsers.SelectedItem;
                if (selectedUser != null)
                {
                    using (var db = new PcClubEntities())
                    {
                        EventUser eventUser = db.EventUser.FirstOrDefault(u => u.IdEvent == selectedEvent.Id && u.IdUser == selectedUser.Id);
                        if (eventUser != null)
                        {
                            db.EventUser.Remove(eventUser);
                            db.SaveChanges();
                            LoadEventUsers();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
        }
    }
}
