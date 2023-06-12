using PcClub.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для EventPage.xaml
    /// </summary>
    public partial class EventPage : Page
    {
        public Event SelectedEvent { get; set; }
        public List<Event> Events { get; set; }
        DateTime currentDate = DateTime.Now;

        public EventPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadEvents();
        }

        private void LoadEvents()
        {
            using (var db = new PcClubEntities())
            {
                Events = db.Event.Where(e => e.IsDeleted != true).ToList();
                DataContext = this;
            }   
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewEventPage());
        }
        private void lvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEvent = (Event)lvEvents.SelectedItem;
        }
        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewUserToEventPage(SelectedEvent));
        }
        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEvent != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить событие?", "Удаление события", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new PcClubEntities())
                    {
                        // Найти событие в базе данных
                        Event eventToDelete = db.Event.FirstOrDefault(ev => ev.Id == SelectedEvent.Id);
                        if (eventToDelete != null)
                        {
                            eventToDelete.IsDeleted = true;
                            db.SaveChanges();
                            // Очистить выбранное событие
                            SelectedEvent = null;
                            // Обновить список событий
                            LoadEvents();
                            // Вывести сообщение об успешном удалении
                            MessageBox.Show("Событие успешно удалено.", "Удаление события", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите событие для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = (Event)lvEvents.SelectedItem;
            if (selectedEvent != null)
            {
                using (var db = new PcClubEntities())
                {
                    List<string> emailList = db.EventUser.Where(u => u.User.Email != null).Select(u => u.User.Email).ToList();

                    string fromMail = "pcclubclient@gmail.com";
                    string fromPassword = "kfkwqtfqarsifyjw";
                    string subject = selectedEvent.Name;
                    foreach (string email in emailList)
                    {
                        User user = db.User.FirstOrDefault(u => u.Email == email);
                        if (user != null)
                        {
                            string emailBody = $"Привет, {user.FullName}!\n\n";
                            emailBody += $"{selectedEvent.Name} будет проводиться {(selectedEvent.Date.HasValue ? selectedEvent.Date.Value.ToShortDateString() : "неизвестная дата")}.\n\n{selectedEvent.Description}";

                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(fromMail);
                            message.Subject = subject;
                            message.To.Add(new MailAddress(email));
                            message.Body = emailBody;
                            message.IsBodyHtml = true;

                            var smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(fromMail, fromPassword),
                                EnableSsl = true,
                            };

                            smtpClient.Send(message);
                        }
                    }

                }
                MessageBox.Show("Сообщения успешно отправлены.", "Отправка сообщений", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите событие.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

    }
}