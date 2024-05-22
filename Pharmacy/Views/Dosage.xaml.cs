using System.Windows;
using System;
using System.Windows.Controls;
using Pharmacy.Controllers;
using Pharmacy.Models;
using Pharmacy.Repositories;

namespace Pharmacy.Views
{
    public partial class Dosage : UserControl
    {
        private readonly DosageController _controller;
        private readonly DrugsRepository _drugsRepository;

        public Dosage()
        {
            InitializeComponent();
            _controller = new DosageController();
            _drugsRepository = new DrugsRepository();
            FillComboBoxDrugs();
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
                if (!ValidateDosageInput())
                {
                    return;
                }

                // Создание новой дозировки из данных формы
                DosageModel newDosage = new DosageModel
                {
                    Name = dosageNameTextBox.Text,
                    DrugsModel = _drugsRepository.GetByName(drugsComboBox.SelectedItem.ToString()),
                    Quantity = int.Parse(quantityTextBox.Text),
                    MeasurementUnit = measurementUnitTextBox.Text,
                    DosageValue = double.Parse(dosageValueTextBox.Text)
                };

                // Добавление новой дозировки через контроллер
                _controller.Add(newDosage);
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
                DosageModel selectedDosage = (DosageModel)dosageDataGrid.SelectedItem;

                if (selectedDosage != null)
                {
                    // Удаление дозировки через контроллер
                    _controller.Delete(selectedDosage.DosageID);
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
                // Получение выбранной дозировки из списка
                DosageModel selectedDosage = (DosageModel)dosageDataGrid.SelectedItem;

                if (selectedDosage != null)
                {
                    selectedDosage.DrugsModel = _drugsRepository.GetByName(drugsComboBox.SelectedItem.ToString());
                    selectedDosage.Name = dosageNameTextBox.Text;
                    selectedDosage.Quantity = int.Parse(quantityTextBox.Text);
                    selectedDosage.MeasurementUnit = measurementUnitTextBox.Text;
                    selectedDosage.DosageValue = double.Parse(dosageValueTextBox.Text);
                    // Обновление дозировки через контроллер
                    _controller.Update(selectedDosage);
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

        private void FilterByDrugButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (drugsComboBox.SelectedItem != null)
                {
                    string drugsName = drugsComboBox.SelectedItem.ToString();

                    // Получение ID выбранного места назначения по его имени
                    int drugId = _drugsRepository.GetByName(drugsName).DrugsID;

                    _controller.FilterByDrug(drugId);

                    MessageBox.Show($"Лекарства успешно отфильтрованы по: {drugsName}");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите место назначения для фильтрации.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Неверный формат ввода ID назначения. Пожалуйста, введите целое число.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void FillComboBoxDrugs()
        {
            try
            {
                var drugs = _drugsRepository.GetAll();
                drugsComboBox.Items.Clear();

                // Проходимся по списку мест назначения и добавляем их имена в ComboBox
                foreach (var drug in drugs)
                {
                    drugsComboBox.Items.Add(drug.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при заполнении ComboBox: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateDosageInput()
        {
            // Проверка наличия значения в поле "Название"
            if (string.IsNullOrWhiteSpace(dosageNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название дозировки.");
                return false;
            }

            // Проверка выбора препарата из списка
            if (drugsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите препарат.");
                return false;
            }

            // Проверка корректности ввода количества
            if (!int.TryParse(quantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество.");
                return false;
            }

            // Проверка наличия значения в поле "Единица измерения"
            if (string.IsNullOrWhiteSpace(measurementUnitTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите единицу измерения.");
                return false;
            }

            // Проверка корректности ввода значения дозировки
            if (!double.TryParse(dosageValueTextBox.Text, out double dosageValue) || dosageValue <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное значение дозировки.");
                return false;
            }

            return true;
        }
    }
}
