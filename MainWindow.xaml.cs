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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace TrainingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetupWorkout_Click(object sender, RoutedEventArgs e)
        {
            WorkoutSetupWindow setupWindow = new WorkoutSetupWindow();
            setupWindow.Show();
            this.Close();
        }

        private void ShowWorkouts_Click(object sender, RoutedEventArgs e)
        {
            WorkoutsWindow workoutsWindow = new WorkoutsWindow();
            workoutsWindow.Show();
            this.Close();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
