using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Workers
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class WorkerPage : Page
    {
        double zpp = 0;
        Worker worker;

        public WorkerPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           if (e.Parameter!=null)
            {
                int id= (int)e.Parameter;
                using (MobileContext db = new MobileContext())
                {
                    worker = db.Workers.FirstOrDefault(c => c.Id == id);
                }
            }
           if (worker!=null)
            {
                headerBlock.Text = "Редактирование сотрудника";
                nameBox.Text = worker.name;
                familyBox.Text = worker.family;
                hoursBox.Text = (worker.hours).ToString();
                tarifBox.Text =Convert.ToString(worker.Tariff);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MobileContext db = new MobileContext())
                {

                    if (worker != null)
                    {
                        if (worker.hours > 144)
                        {
                            worker.name = nameBox.Text;
                            worker.family = familyBox.Text;
                            worker.hours = Convert.ToDouble(hoursBox.Text);
                            worker.Tariff = Convert.ToDouble(tarifBox.Text);
                            zpp = worker.hours - 144;
                            worker.salary = (144 * worker.Tariff) + (zpp * (worker.Tariff) * 2);
                            worker.salaryw = worker.salary * 0.87;
                            db.Workers.Update(worker);
                            var itsok = new MessageDialog("Информация изменена!");
                            await itsok.ShowAsync();
                        }

                        else
                        {
                            worker.name = nameBox.Text;
                            worker.family = familyBox.Text;
                            worker.hours = Convert.ToDouble(hoursBox.Text);
                            worker.Tariff = Convert.ToDouble(tarifBox.Text);

                            worker.salary = worker.hours * worker.Tariff;
                            worker.salaryw = worker.salary * 0.87;
                            db.Workers.Update(worker);
                            var itsok = new MessageDialog("Информация изменена!");
                            await itsok.ShowAsync();
                        }
                    }
                    else
                    {
                        double tar = Convert.ToDouble(tarifBox.Text);
                        double or = Convert.ToDouble(hoursBox.Text);
                        if (or > 144)
                        {

                            zpp = or - 144;

                            double salary = (144 * tar) + ((zpp * tar) * 2);
                            double salaryw = salary * 0.87;
                            double lol = salaryw;
                            db.Workers.Add(new Worker { name = nameBox.Text, family = familyBox.Text, hours = Convert.ToDouble(hoursBox.Text), Tariff = Convert.ToDouble(tarifBox.Text),salary=salary, salaryw = lol });
                            var itsok = new MessageDialog("Информация сохранена!");
                            await itsok.ShowAsync();

                        }

                        else
                        {


                            double z = Convert.ToDouble(hoursBox.Text);
                            double p = Convert.ToDouble(tarifBox.Text);
                            double zp0 = z * p;
                            double zp = (z * p) * 0.87;
                            db.Workers.Add(new Worker { name = nameBox.Text, family = familyBox.Text, hours = Convert.ToDouble(hoursBox.Text), Tariff = Convert.ToDouble(tarifBox.Text),salary=zp0, salaryw = zp });
                            var itsok = new MessageDialog("Информация сохранена!");
                            await itsok.ShowAsync();
                        }


                    }
                    db.SaveChanges();
                }
                GoToMainPage();
            } 
                 catch (FormatException)
            {
                var dialog = new MessageDialog("Неверные данные");
                await dialog.ShowAsync();
            }
        }
            
        

        private void Cancel_Click(object sender, RoutedEventArgs e)
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
