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
    public partial class UserPage : Window
    {
        private Entities db = new Entities();

        public UserPage()
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

        private void lstEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
