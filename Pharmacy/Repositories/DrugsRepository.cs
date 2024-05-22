using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using Pharmacy.Connection;
using Pharmacy.Interfaces;
using Pharmacy.Models;

namespace Pharmacy.Repositories
{
    /// <summary>
    /// Репозиторий для работы с данными о лекарствах.
    /// </summary>
    public class DrugsRepository : IRepository<DrugsModel>
    {
        private readonly string _connectionString = DatabaseSettings.Instance.ConnectionString;

        /// <summary>
        /// Получить все объекты из репозитория.
        /// </summary>
        /// <returns>Список всех объектов.</returns>
        public List<DrugsModel> GetAll()
        {
            List<DrugsModel> drugsList = new List<DrugsModel>();

            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DrugsModel> drugs = context.GetTable<DrugsModel>();
                    drugsList = drugs.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о лекарствах: {ex.Message}");
            }

            return drugsList;
        }

        /// <summary>
        /// Получить объект из репозитория по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns>Объект с указанным идентификатором или null, если объект не найден.</returns>
        public DrugsModel GetById(int id)
        {
            DrugsModel drug = null;

            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DrugsModel> drugs = context.GetTable<DrugsModel>();
                    drug = drugs.SingleOrDefault(d => d.DrugsID == id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о лекарстве: {ex.Message}");
            }

            return drug;
        }

        /// <summary>
        /// Добавить новый объект в репозиторий.
        /// </summary>
        /// <param name="item">Добавляемый объект.</param>
        public void Add(DrugsModel item)
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DrugsModel> drugs = context.GetTable<DrugsModel>();
                    drugs.InsertOnSubmit(item);
                    context.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении лекарства: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить существующий объект в репозитории.
        /// </summary>
        /// <param name="item">Обновляемый объект.</param>
        public void Update(DrugsModel item)
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DrugsModel> drugs = context.GetTable<DrugsModel>();
                    DrugsModel existingDrug = drugs.SingleOrDefault(d => d.DrugsID == item.DrugsID);
                    if (existingDrug != null)
                    {
                        existingDrug.Name = item.Name;
                        existingDrug.Form = item.Form;
                        existingDrug.Manufacturer = item.Manufacturer;
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении лекарства: {ex.Message}");
            }
        }

        /// <summary>
        /// Удалить объект из репозитория по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        public void Delete(int id)
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DrugsModel> drugs = context.GetTable<DrugsModel>();
                    DrugsModel drugToDelete = drugs.SingleOrDefault(d => d.DrugsID == id);
                    if (drugToDelete != null)
                    {
                        drugs.DeleteOnSubmit(drugToDelete);
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении лекарства: {ex.Message}");
            }
        }

        /// <summary>
        /// Создать таблицу "Drugs" в базе данных.
        /// </summary>
        public void Create()
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    context.ExecuteCommand(@"CREATE TABLE Drugs (
                                        DrugsID INT IDENTITY(1, 1) PRIMARY KEY,
                                        Name NVARCHAR(50) NOT NULL,
                                        Form NVARCHAR(50) NOT NULL,
                                        Manufacturer NVARCHAR(100) NOT NULL
                                    )");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании таблицы Drugs: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить объекты из репозитория по их названию.
        /// </summary>
        /// <param name="name">Название лекарства.</param>
        /// <returns>Список объектов с указанным названием.</returns>
        public DrugsModel GetByName(string drugName)
        {
            DrugsModel drug = null;

            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DrugsModel> drugs = context.GetTable<DrugsModel>();
                    drug = drugs.SingleOrDefault(d => d.Name == drugName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о лекарстве: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return drug;
        }
    }
}