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
    public partial class AddPage : Window
    {
        private Entities db = new Entities();

        public AddPage()
        {
            InitializeComponent();
            city.ItemsSource = db.City.Select(g => g.Name).ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name.Text) ||
                dpDate.SelectedDate == null ||
                string.IsNullOrWhiteSpace(days.Text) ||
                city.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!int.TryParse(days.Text, out int numberOfVisitors))
            {
                MessageBox.Show("Пожалуйста, введите корректное число посетителей.");
                return;
            }

            if (city.SelectedItem != null)
            {
                string formOfEventName = city.SelectedItem.ToString();

                // Находим соответствующий объект FormOfEvents по имени
                var nameOfEvent = db.City.FirstOrDefault(f => f.Name == formOfEventName);

                if (nameOfEvent != null)
                {
                    // Создаем новое мероприятие и заполняем его данными
                    Event newEvent = new Event
                    {
                        Name = name.Text,
                        Date = (DateTime)dpDate.SelectedDate,
                        Days = int.Parse(days.Text),
                        CityId = nameOfEvent.Id
                    };

                    db.Event.Add(newEvent);
                    db.SaveChanges();
                    MessageBox.Show("Мероприятие добавлено успешно.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Выбранная форма мероприятия не найдена.");
                }
            }
            else
            {
                MessageBox.Show("Выберите форму мероприятия из списка.");
            }
        }
    }
}
