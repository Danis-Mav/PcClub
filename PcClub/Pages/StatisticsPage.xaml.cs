using PcClub.DB;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using System.Text;
using System.IO;
using ClosedXML.Excel;

namespace PcClub.Pages
{
    public partial class StatisticsPage : Page, INotifyPropertyChanged
    {
        public SeriesCollection PlaceTypes { get; set; }
        public SeriesCollection EventVisits { get; set; }


        private DateTime? selectedStartDate;
        private DateTime? selectedEndDate;

        public DateTime? SelectedStartDate
        {
            get { return selectedStartDate; }
            set
            {
                selectedStartDate = value;
                LoadChartData();
            }
        }

        public DateTime? SelectedEndDate
        {
            get { return selectedEndDate; }
            set
            {
                selectedEndDate = value;
                LoadChartData();
            }
        }

        public StatisticsPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadChartData();
        }

        private void LoadChartData()
        {
            using (var context = new PcClubEntities())
            {
                var placeTypeData = context.Booking
                    .Where(b => b.DateTimeStart >= selectedStartDate && b.DateTimeEnd <= selectedEndDate)
                    .GroupBy(b => b.Place.Type.Name)
                    .Select(g => new
                    {
                        TypeName = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                PlaceTypes = new SeriesCollection();
                var bookingData = context.Booking
    .Where(b => b.DateTimeStart >= selectedStartDate && b.DateTimeEnd <= selectedEndDate)
    .Select(b => new
    {
        PlaceName = b.Place.Name,
        DateTimeStart = b.DateTimeStart,
        DateTimeEnd = b.DateTimeEnd,
        UserName = b.User.FullName
    })
    .ToList();

                foreach (var data in placeTypeData)
                {
                    PlaceTypes.Add(new PieSeries
                    {
                        Title = data.TypeName,
                        Values = new ChartValues<int> { data.Count },
                        DataLabels = true
                    });
                }
                OnPropertyChanged(nameof(PlaceTypes));

            }
            using (var db = new PcClubEntities())
            {
                var eventVisitsData = db.Event.Where(e => e.Date >= selectedStartDate && e.Date <= selectedEndDate).Select(e => new{EventName = e.Name,UserCount = e.EventUser.Count()}).ToList();


                EventVisits = new SeriesCollection();
                foreach (var data in eventVisitsData)
                {
                    EventVisits.Add(new PieSeries
                    {
                        Title = data.EventName,
                        Values = new ChartValues<int> { data.UserCount },
                        DataLabels = true
                    });
                }

                OnPropertyChanged(nameof(EventVisits));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnTPExport_Click(object sender, RoutedEventArgs e)
        {
            if (PlaceTypes != null && PlaceTypes.Any())
            {

                XLWorkbook wb = new XLWorkbook();

                var ws = wb.Worksheets.Add("PlaceTypes");

                ws.Cell("A1").Value = "Дата с:";
                ws.Cell("B1").Value = selectedStartDate?.ToString("dd.MM.yyyy");
                ws.Cell("A2").Value = "по:";
                ws.Cell("B2").Value = selectedEndDate?.ToString("dd.MM.yyyy");

                // Заголовки данных PlaceTypes
                ws.Cell("A4").Value = "Тип места";
                ws.Cell("B4").Value = "Количество";

                int placeTypesRow = 5;
                foreach (var data in PlaceTypes)
                {
                    ws.Cell(placeTypesRow, 1).Value = data.Title.ToString();
                    ws.Cell(placeTypesRow, 2).Value = data.Values[0]?.ToString();
                    placeTypesRow++;
                }

                using (var context = new PcClubEntities())
                {
                    var bookingData = context.Booking
                    .Where(b => b.DateTimeStart >= selectedStartDate && b.DateTimeEnd <= selectedEndDate)
                    .Select(b => new
                    {
                        PlaceName = b.Place.Name,
                        DateTimeStart = b.DateTimeStart,
                        DateTimeEnd = b.DateTimeEnd,
                        UserName = b.User.FullName
                    })
                    .ToList();
                
                if (bookingData != null && bookingData.Any())
                {
                    var wsBooking = wb.Worksheets.Add("Booking");

                        // Заголовки данных Booking
                        wsBooking.Cell("A1").Value = "Место";
                        wsBooking.Cell("B1").Value = "Дата и время начала";
                        wsBooking.Cell("C1").Value = "Дата и время окончания";
                        wsBooking.Cell("D1").Value = "Пользователь";

                    int bookingRow = 2;
                    foreach (var booking in bookingData)
                    {
                        wsBooking.Cell(bookingRow, 1).Value = booking.PlaceName;
                        wsBooking.Cell(bookingRow, 2).Value = booking.DateTimeStart?.ToString("dd.MM.yyyy HH:mm");
                        wsBooking.Cell(bookingRow, 3).Value = booking.DateTimeEnd?.ToString("dd.MM.yyyy HH:mm");
                        wsBooking.Cell(bookingRow, 4).Value = booking.UserName;
                        bookingRow++;
                    }
                    }
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "place_types_and_booking.xlsx");

                wb.SaveAs(filePath);

                MessageBox.Show("Экспорт успешно выполнен.");
            }
            else
            {
                MessageBox.Show("Нет данных для экспорта.");
            }
        }

        private void btnEUExport_Click(object sender, RoutedEventArgs e)
        {
            if (EventVisits != null && EventVisits.Any())
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Event Visits");

                ws.Cell("A1").Value = "Название события";
                ws.Cell("B1").Value = "Количество учавствующих пользователей";

                int row = 5;
                foreach (var data in EventVisits)
                {
                    ws.Cell(row, 1).Value = data.Title.ToString();
                    ws.Cell(row, 2).Value = data.Values[0]?.ToString();
                    row++;
                }
                
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "event_visits.xlsx");
                wb.SaveAs(filePath);

                MessageBox.Show("Данные успешно экспортированы в файл event_visits.xlsx");
            }
            else
            {
                MessageBox.Show("Нет данных для экспорта");
            }
        }

    }
}
