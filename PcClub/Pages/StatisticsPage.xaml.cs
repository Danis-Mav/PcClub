using PcClub.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace PcClub.Pages
{
    /// <summary>
    /// Логика взаимодействия для StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        private List<dynamic> eventParticipants;
        private List<dynamic> activeUsers;
        public StatisticsPage()
        {
            InitializeComponent();
            CalculateStatistics();
        }

        private void CalculateStatistics()
        {
            using (var db = new PcClubEntities())
            {
                // Количество пользователей участвующих в каждом событии
                var eventParticipants = db.EventUser.GroupBy(eu => eu.IdEvent)
                    .Select(g => new { EventId = g.Key, ParticipantCount = g.Count() })
                    .ToList();

                // Количество пользователей не участвующих нигде
                var usersWithoutEvents = db.User.Where(u => !db.EventUser.Any(eu => eu.IdUser == u.Id))
                    .Count();

                // Самый активный пользователь (выделить первые три)
                var activeUsers = db.EventUser.GroupBy(eu => eu.IdUser)
                    .Select(g => new { UserId = g.Key, EventCount = g.Count() })
                    .OrderByDescending(g => g.EventCount)
                    .Take(3)
                    .ToList();               

                // Общее количество зарегистрированных пользователей
                var totalUsersCount = db.User.Count();

                // Сколько выручки получили (booking.Hour * booking.IdPlace.IdType.Price)
                var totalRevenue = db.Booking.Sum(booking => booking.Hour * booking.Place.Type.Price);

                // Самые популярные места среди пользователей (выделить первые три)
                var popularPlaces = db.Booking.GroupBy(booking => booking.IdPlace).Select(g => new { PlaceId = g.Key, BookingCount = g.Count() }).OrderByDescending(g => g.BookingCount).Take(3).ToList();

                // Самый насыщенный день за этот месяц
                var currentDate = DateTime.Now;
                var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var busiestDay = db.Booking.Where(booking => booking.DateTimeStart >= startDate && booking.DateTimeEnd <= endDate).GroupBy(booking => DbFunctions.TruncateTime(booking.DateTimeStart)).Select(g => new { Date = g.Key, BookingCount = g.Count() }).OrderByDescending(g => g.BookingCount).FirstOrDefault();

                // Отобразить данные на странице
                lblEventParticipants.Text = eventParticipants.Count.ToString();
                lblUsersWithoutEvents.Text = usersWithoutEvents.ToString();
                lblActiveUsers.Text = string.Join(", ", activeUsers.Select(u => u.UserId.ToString()).ToArray());
                lblTotalUsersCount.Text = totalUsersCount.ToString();
                lblTotalRevenue.Text = totalRevenue.ToString();
                lblPopularPlaces.Text = string.Join(", ", popularPlaces.Select(p => p.PlaceId.ToString()).ToArray());
                lblBusiestDay.Text = busiestDay.ToString();

            }
        }

    }
}
