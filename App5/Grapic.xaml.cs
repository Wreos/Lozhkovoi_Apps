using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Workers;


// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Workers
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Grapic : Page
    {
        public Grapic()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            LoadChartContents();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
          //  using (MobileContext db = new MobileContext())
          //  {
           //     workersList.ItemsSource = db.Workers.ToList();
          //  }
        }

        private void LoadChartContents()
        {
            using (MobileContext db = new MobileContext())
            {
                List<Worker> wrk = new List<Worker>(db.Workers) ;
               // wrk.Add(new Worker() { name = "Том",salaryw=5600 });
               (PieChart.Series[0] as PieSeries).ItemsSource = wrk; 
                (ColumnChart.Series[0] as ColumnSeries).ItemsSource = wrk; 
              (lineChart.Series[0] as LineSeries).ItemsSource = wrk ;
                 
            }
           
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            GoToMainPage();
        }

        private void GoToMainPage()
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(MainPage));
        }
    }
}
