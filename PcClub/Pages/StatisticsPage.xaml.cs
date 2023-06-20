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

                var eventData = context.Event
     .Select(e => new
     {
         EventName = e.Name,
         UserCount = e.EventUser.Count()
     })
     .ToList();


                EventVisits = new SeriesCollection();

                foreach (var data in eventData)
                {
                    EventVisits.Add(new ColumnSeries
                    {
                        Title = data.EventName,
                        Values = new ChartValues<int> { data.UserCount },
                        DataLabels = true
                    });
                }

                EventVisits = new SeriesCollection();

                foreach (var data in eventData)
                {
                    EventVisits.Add(new ColumnSeries
                    {
                        Title = data.EventName,
                        Values = new ChartValues<int> { data.UserCount },
                        DataLabels = true
                    });
                }

                EventVisits = new SeriesCollection();

                foreach (var data in eventData)
                {
                    EventVisits.Add(new ColumnSeries
                    {
                        Title = data.EventName,
                        Values = new ChartValues<int> { data.UserCount },
                        DataLabels = true
                    });
                }


                OnPropertyChanged(nameof(PlaceTypes));
                OnPropertyChanged(nameof(EventVisits));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
