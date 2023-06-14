using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для NewUserPage.xaml
    /// </summary>
    public partial class NewUserPage : Page
    {
        public NewUserPage()
        {
            InitializeComponent();
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                FullName = txtFullName.Text,
                PhoneNumber = txtNumber.Text,
                Email = txtEmail.Text,
            };

            // Добавление нового пользователя в базу данных
            DBConnection.connection.User.Add(newUser);
            DBConnection.connection.SaveChanges();

            // Очистка полей ввода после добавления пользователя
            txtFullName.Text = string.Empty;
            txtNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;

            MessageBox.Show("Пользователь успешно добавлен.");

            NavigationService?.Navigate(new AllUserPage());
        }
        private void txtInput_LettersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[а-яА-Я]+$");
        }

        private void txtInput_NumbersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length >= 10 && e.Text.Length > 0)
            {
                e.Handled = true;
            }
        }
    }
}
