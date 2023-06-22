using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private string confirmationCode;
        public NewUserPage()
        {
            InitializeComponent();
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            string digitsOnly = "";
            foreach (char c in txtNumber.Text)
            {
                if (Char.IsDigit(c))
                {
                    digitsOnly += c;
                }
            }
            string name = txtFullName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Пожалуйста, введите фио.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (digitsOnly.Length < 10)
            {
                MessageBox.Show("Номер телефона должен содержать не менее 10 цифр.");
                return;
            }
            confirmationCode = GenerateConfirmationCode();

            // Отправка кода подтверждения на электронную почту
            SendConfirmationCode(txtEmail.Text, confirmationCode, txtFullName.Text);

            // Открытие окна для ввода кода подтверждения
            ConfirmationWindow confirmationWindow = new ConfirmationWindow();
            confirmationWindow.ShowDialog();

            // Проверка введенного кода подтверждения
            if (confirmationWindow.ConfirmationCode == confirmationCode)
            {
                User newUser = new User
                {
                    FullName = txtFullName.Text,
                    PhoneNumber = digitsOnly,
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
            else
            {
                MessageBox.Show("Неверный код подтверждения.");
            }
        }
        private void txtInput_LettersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[а-яА-Я]+$");
        }
        private string GenerateConfirmationCode()
        {
            // Генерация кода подтверждения состоящего из 6 символов
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task SendConfirmationCode(string email, string code, string fullName)
        {
            using (var db = new PcClubEntities())
            {
                string fromMail = "pcclubclient@gmail.com";
                string fromPassword = "kfkwqtfqarsifyjw";
                string subject = txtEmail.Text;

                string emailBody = $"Привет, {fullName}!\n\n";
                emailBody += $"Ваш код подтверждения: {code}";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = subject;
                message.To.Add(new MailAddress(email));
                message.Body = emailBody;
                message.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(message);
                }
            }
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
