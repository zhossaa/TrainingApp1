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
using System.Data;


namespace TrainingApp
{
    public partial class WorkoutsWindow : Window
    {
        public WorkoutsWindow()
        {
            InitializeComponent();
            LoadWorkouts();
        }

        private void LoadWorkouts()
        {
            try
            {
                // Загрузка данных о тренировках
                DataTable workoutsTable = DatabaseHelper.GetAllWorkouts();
                WorkoutsDataGrid.ItemsSource = workoutsTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки тренировок: {ex.Message}", "Ошибка");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
