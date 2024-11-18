using System;
using System.Windows;
using System.Windows.Controls;

namespace TrainingApp
{
    public partial class WorkoutSetupWindow : Window
    {
        public WorkoutSetupWindow()
        {
            InitializeComponent();
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name) ||
                    !int.TryParse(AgeTextBox.Text, out int age) || age <= 0 ||
                    !double.TryParse(WeightTextBox.Text, out double weight) || weight <= 0)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка");
                    return;
                }

                string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (string.IsNullOrEmpty(gender))
                {
                    MessageBox.Show("Выберите пол.", "Ошибка");
                    return;
                }

                string goal = "Сила"; // Укажите цель
                string recommendation = $"Рекомендуемая нагрузка: {weight * 0.5:F1} кг";

                // Добавляем пользователя и тренировку
                int userId = DatabaseHelper.AddUser(name, age, weight, gender);
                if (userId != -1)
                {
                    DatabaseHelper.AddWorkout(userId, goal, recommendation);
                    MessageBox.Show("Данные успешно сохранены!", "Успех");

                    // Отображение рекомендаций
                    RecommendationTextBlock.Text = recommendation;
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении данных в базу.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }
    }
}
