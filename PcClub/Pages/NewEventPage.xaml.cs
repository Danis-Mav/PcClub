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
    /// Логика взаимодействия для NewEventPage.xaml
    /// </summary>
    public partial class NewEventPage : Page
    {
        public NewEventPage()
        {
            InitializeComponent();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            string name = txtName.Text;
            string description = txtDescription.Text;
            DateTime date = dpDate.SelectedDate ?? DateTime.Now;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(txtMaxUsers.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int maxUsers = int.Parse(txtMaxUsers.Text);
            

            if (!int.TryParse(txtMaxUsers.Text, out maxUsers))
            {
                MessageBox.Show("Некорректное значение для максимального количества пользователей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new PcClubEntities())
            {
                Event newEvent = new Event
                {
                    Name = name,
                    Description = description,
                    Date = date,
                    MaxUser = maxUsers
                };

                db.Event.Add(newEvent);
                db.SaveChanges();
            }

            NavigationService.Navigate(new EventPage());
        }
    }
}
