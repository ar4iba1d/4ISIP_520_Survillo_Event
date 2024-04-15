using _4ISIP_520_Survillo_Event.Database;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace _4ISIP_520_Survillo_Event.Page
{
    public partial class EditPage : Window
    {
        private Entities db = new Entities();
        private Event selectedEvent;

        public EditPage(Event selectedEvent)
        {
            InitializeComponent();
            this.selectedEvent = selectedEvent;
            LoadEventData();
            city.ItemsSource = db.City.Select(g => g.Name).ToList();
        }

        private void LoadEventData()
        {
            name.Text = selectedEvent.Name;
            dpDate.SelectedDate = selectedEvent.Date;
            days.Text = selectedEvent.Days.ToString();
            city.SelectedItem = selectedEvent.City.Name;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                selectedEvent.Name = name.Text;
                selectedEvent.Date = (DateTime)dpDate.SelectedDate;
                selectedEvent.Days = int.Parse(days.Text);
                string selectedFormOfEventName = city.SelectedItem as string;
                var formOfEvent = db.City.FirstOrDefault(g => g.Name == selectedFormOfEventName);
                selectedEvent.City = formOfEvent;

                db.SaveChanges();
                MessageBox.Show("Изменения сохранены успешно.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }
    }
}
