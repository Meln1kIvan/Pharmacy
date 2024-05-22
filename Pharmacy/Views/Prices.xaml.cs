using System;
using System.Data.Linq;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Pharmacy.Controllers;
using Pharmacy.Interfaces;
using Pharmacy.Models;
using Pharmacy.Repositories;

namespace Pharmacy.Views
{
    public partial class Prices : UserControl
    {
        private readonly PricesController _controller;
        private readonly DrugsRepository _drugsRepository;

        public Prices()
        {
            InitializeComponent();
            _controller = new PricesController();
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание новой цены на лекарство из данных формы
                PricesModel newPrice = new PricesModel
                {
                    Name = nameTextBox.Text,
                    DrugsModel = _drugsRepository.GetByName(drugsComboBox.SelectedItem.ToString()),
                    Price = decimal.Parse(priceTextBox.Text)
                };

                // Добавление новой цены на лекарство через контроллер
                _controller.Add(newPrice);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении цены на лекарство: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение выбранной цены на лекарство из списка
                PricesModel selectedPrice = (PricesModel)pricesDataGrid.SelectedItem;

                if (selectedPrice != null)
                {
                    // Удаление цены на лекарство через контроллер
                    _controller.Delete(selectedPrice.PricesID);
                }
                else
                {
                    MessageBox.Show("Выберите цену на лекарство для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении цены на лекарство: {ex.Message}");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение выбранной цены на лекарство из списка
                PricesModel selectedPrice = (PricesModel)pricesDataGrid.SelectedItem;

                if (selectedPrice != null)
                {
                    // Модификация данных выбранной цены на лекарство
                    selectedPrice.DrugsModel = _drugsRepository.GetByName(drugsComboBox.SelectedItem.ToString());
                    selectedPrice.Price = decimal.Parse(priceTextBox.Text);
                    _controller.Update(selectedPrice);
                }
                else
                {
                    MessageBox.Show("Выберите цену на лекарство для обновления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении цены на лекарство: {ex.Message}");
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

        private void FilterByPriceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(minPriceTextBox.Text) && !string.IsNullOrEmpty(maxPriceTextBox.Text))
                {
                    decimal minPrice = decimal.Parse(minPriceTextBox.Text);
                    decimal maxPrice = decimal.Parse(maxPriceTextBox.Text);

                    // Проверка на правильность ввода минимальной и максимальной цен
                    if (minPrice <= maxPrice)
                    {
                        _controller.FilterByPriceRange(minPrice, maxPrice);
                        MessageBox.Show($"Лекарства успешно отфильтрованы по цене: от {minPrice} до {maxPrice}");
                    }
                    else
                    {
                        MessageBox.Show("Минимальная цена должна быть меньше или равна максимальной цене.");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите минимальную и максимальную цену для фильтрации.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Неверный формат ввода цены. Пожалуйста, введите число.");
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
    }
}