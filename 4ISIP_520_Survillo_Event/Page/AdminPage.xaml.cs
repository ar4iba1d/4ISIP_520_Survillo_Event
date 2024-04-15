using _4ISIP_520_Survillo_Event.Database;
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
using System.Windows.Shapes;

namespace _4ISIP_520_Survillo_Event.Page

{
    public partial class AdminPage : Window
    {
        private Entities db = new Entities();

        public AdminPage()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            lstEvents.ItemsSource = db.Event.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            DateTime searchDate;
            bool isDate = DateTime.TryParse(searchText, out searchDate);

            var filteredData = db.Event
                .Where(s => s.Name.ToLower().Contains(searchText) ||
                            s.Days.ToString().Contains(searchText) ||
                            (isDate && DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(searchDate)) ||
                            s.City.Name.ToLower().Contains(searchText))
                .ToList();
            lstEvents.ItemsSource = filteredData;
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            AddPage addEventWindow = new AddPage();
            addEventWindow.ShowDialog();
            LoadEvents();
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = (Event)lstEvents.SelectedItem;
            if (selectedEvent != null)
            {
                db.Event.Remove(selectedEvent);
                db.SaveChanges();
                LoadEvents();
            }
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = (Event)lstEvents.SelectedItem;
            if (selectedEvent != null)
            {
                EditPage editEventWindow = new EditPage(selectedEvent);
                editEventWindow.ShowDialog();
                LoadEvents();
            }
        }
    }
}
