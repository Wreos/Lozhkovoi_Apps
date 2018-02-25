using Workers;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Worker
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class WorkerPageInfo : Page
    {
        Workers.Worker worker;
        public WorkerPageInfo()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                int id = (int)e.Parameter;
                using (MobileContext db = new MobileContext())
                {
                    worker = db.Workers.FirstOrDefault(c => c.Id == id);
                }
            }
            if (worker != null)
            {

                headerBlock.Text = worker.name + " " + worker.family;
                nameBox.Text = worker.name+" "+worker.family;
                hoursBox.Text = (worker.hours).ToString()+" часов";
                tarifBox.Text = Convert.ToString(worker.Tariff)+ " рублей в час";
                okladboxw.Text = Convert.ToString(worker.salary)+ " рублей";
                okladbox.Text = Convert.ToString(worker.salaryw) + " рублей";
                nds.Text = Convert.ToString(worker.salary - worker.salaryw) + " рублей";
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

