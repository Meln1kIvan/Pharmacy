using System.Windows;
using System;
using System.Windows.Controls;
using Pharmacy.Controllers;
using Pharmacy.Models;
using Pharmacy.Repositories;

namespace Pharmacy.Views
{
    public partial class Drugs : UserControl
    {
        private readonly DrugsController _controller;

        public Drugs()
        {
            InitializeComponent();
            _controller = new DrugsController();
            DataContext = _controller;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _controller.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        // Обработчик нажатия кнопки добавления
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateDrugInput())
                {
                    return;
                }

                // Создание новой дозировки из данных формы
                DrugsModel newDrug = new DrugsModel
                {
                    Name = drugNameTextBox.Text,
                    Form = drugFormTextBox.Text,
                    Manufacturer = drugManufacturerTextBox.Text
                };


                // Добавление новой дозировки через контроллер
                _controller.Add(newDrug);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при добавлении дозировки
                MessageBox.Show($"Ошибка при добавлении дозировки: {ex.Message}");
            }
        }

        // Обработчик нажатия кнопки удаления
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение выбранной дозировки из списка
                DrugsModel selectedDosage = (DrugsModel)drugsDataGrid.SelectedItem;

                if (selectedDosage != null)
                {
                    // Удаление дозировки через контроллер
                    _controller.Delete(selectedDosage.DrugsID);
                }
                else
                {
                    MessageBox.Show("Выберите дозировку для удаления.");
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при удалении дозировки
                MessageBox.Show($"Ошибка при удалении дозировки: {ex.Message}");
            }
        }

        // Обработчик нажатия кнопки обновления
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateDrugInput())
                {
                    return;
                }

                // Получение выбранной дозировки из списка
                DrugsModel selectedDrug = (DrugsModel)drugsDataGrid.SelectedItem;

                if (selectedDrug != null)
                {
                    // Проверяем, можно ли преобразовать строку в число
                    if (int.TryParse(drugFormTextBox.Text, out int drugsID))
                    {
                        // Если преобразование успешно, присваиваем значение
                        selectedDrug.DrugsID = drugsID;
                    }
                    else
                    {
                        MessageBox.Show("Некорректный формат ID препарата.");
                        return;
                    }

                    // Обновляем остальные поля модели
                    selectedDrug.Form = drugFormTextBox.Text;
                    selectedDrug.Manufacturer = drugManufacturerTextBox.Text;

                    // Обновление дозировки через контроллер
                    _controller.Update(selectedDrug);
                }
                else
                {
                    MessageBox.Show("Выберите дозировку для обновления.");
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при обновлении дозировки
                MessageBox.Show($"Ошибка при обновлении дозировки: {ex.Message}");
            }
        }

        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.Create();
        }

        private bool ValidateDrugInput()
        {
            // Проверка наличия значения в поле "Название"
            if (string.IsNullOrWhiteSpace(drugNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название препарата.");
                return false;
            }

            // Проверка наличия значения в поле "Форма"
            if (string.IsNullOrWhiteSpace(drugFormTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите форму препарата.");
                return false;
            }

            // Проверка наличия значения в поле "Производитель"
            if (string.IsNullOrWhiteSpace(drugManufacturerTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите производителя препарата.");
                return false;
            }

            return true;
        }

    }
}
