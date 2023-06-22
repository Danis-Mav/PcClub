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

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //if (PlaceTypes != null && PlaceTypes.Any())
            //{
            //    var wb = new XLWorkbook();

            //    foreach (var data in PlaceTypes)
            //    {
            //        var ws = wb.Worksheets.Add(data.TypeName);

            //        ws.Cell("A1").Value = "Type Name";
            //        ws.Cell("B1").Value = "Count";

            //        int row = 2;
            //        foreach (var value in data.Values)
            //        {
            //            ws.Cell(row, 1).Value = data.Title.ToString();
            //            ws.Cell(row, 2).Value = value.ToString();
            //            row++;
            //        }
            //    }


            //    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //    string filePath = Path.Combine(desktopPath, "place_types.xlsx");
            //    wb.SaveAs(filePath);

            //    MessageBox.Show("Данные успешно экспортированы в файл place_types.xlsx");
            //}
            //else
            //{
            //    MessageBox.Show("Нет данных для экспорта");
            //}
        }
    }
}
