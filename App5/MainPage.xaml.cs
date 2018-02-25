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
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Worker;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Workers
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Worker> z;
        List<Worker> l;
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
           

        }

        public void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
              var z= db.Workers.ToList();
              //  List<Worker> z = l.FindAll(delegate (Worker worker)
              //  { return worker.name.Contains("Петр"); });
               
                workersList.ItemsSource = z;
                //List<Worker> z = db.Workers.ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WorkerPage));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (workersList.SelectedItem != null)
            {
                Worker worker = workersList.SelectedItem as Worker;
                if (worker != null)
                    Frame.Navigate(typeof(WorkerPage), worker.Id);
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (workersList.SelectedItem != null)
            {
                ContentDialog deleteFileDialog = new ContentDialog()
                {
                    Title = "Подтверждение действия",
                    Content = "Вы действительно хотите удалить работника?",
                    PrimaryButtonText = "ОК",
                    SecondaryButtonText = "Отмена"
                };

                ContentDialogResult result = await deleteFileDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    if (workersList.SelectedItem != null)
                    {
                        Worker worker = workersList.SelectedItem as Worker;
                        if (worker != null)
                        {
                            using (MobileContext db = new MobileContext())
                            {

                                db.Workers.Remove(worker);
                                db.SaveChanges();
                                workersList.ItemsSource = db.Workers.ToList();

                            }
                        }
                    }

                }
                else if (result == ContentDialogResult.Secondary)
                {

                }
            }

        }

        private void refreshButtion_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Grapic));
        }

        private void workersList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (workersList.SelectedItem != null)
            {
                Worker worker = workersList.SelectedItem as Worker;
                if (worker != null)
                    Frame.Navigate(typeof(WorkerPageInfo), worker.Id);
            }

        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            {
                if (workersList.SelectedItem != null)
                {
                    Worker worker = workersList.SelectedItem as Worker;
                    if (worker != null)
                        Frame.Navigate(typeof(WorkerPageInfo), worker.Id);
                }

            }

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
                //  List<Worker> z = l.FindAll(delegate (Worker worker)
                //  { return  worker.name.Contains("Петр"); });
                List<Worker> l = new List<Worker>(db.Workers);
                string ur = textBoxFamaly.Text;
                
              
                List<Worker> p = l.FindAll(t => t.name.Contains(ur));
                // or

                workersList.ItemsSource = p;
            }
        }

        private void textBoxFamaly_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (MobileContext db = new MobileContext())
            {
                //  List<Worker> z = l.FindAll(delegate (Worker worker)
                //  { return  worker.name.Contains("Петр"); });
                List<Worker> l = new List<Worker>(db.Workers);
                string ur = textBoxFamaly.Text;
                List<Worker> p = l.FindAll(t => t.family.Contains(ur));
                // or

                workersList.ItemsSource = p;
            }


        }
    }

}
