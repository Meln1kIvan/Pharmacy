using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using Pharmacy.Interfaces;
using Pharmacy.ModelsDAO;
using Pharmacy.Connection;

namespace Pharmacy.Repositories
{
    /// <summary>
    /// Репозиторий для работы с данными о дозировках лекарств.
    /// </summary>
    public class DosageRepository : IRepository<DosageModelDAO>
    {
        private readonly string _connectionString = DatabaseSettings.Instance.ConnectionString;

        /// <summary>
        /// Получить все объекты из репозитория.
        /// </summary>
        /// <returns>Список всех объектов.</returns>
        public List<DosageModelDAO> GetAll()
        {
            List<DosageModelDAO> dosagesList = new List<DosageModelDAO>();

            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DosageModelDAO> dosages = context.GetTable<DosageModelDAO>();
                    dosagesList = dosages.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о дозировках: {ex.Message}");
            }

            return dosagesList;
        }

        /// <summary>
        /// Получить объект из репозитория по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns>Объект с указанным идентификатором или null, если объект не найден.</returns>
        public DosageModelDAO GetById(int id)
        {
            DosageModelDAO dosage = null;

            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DosageModelDAO> dosages = context.GetTable<DosageModelDAO>();
                    dosage = dosages.SingleOrDefault(d => d.DosageID == id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о дозировке: {ex.Message}");
            }

            return dosage;
        }

        /// <summary>
        /// Добавить новый объект в репозиторий.
        /// </summary>
        /// <param name="item">Добавляемый объект.</param>
        public void Add(DosageModelDAO item)
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DosageModelDAO> dosages = context.GetTable<DosageModelDAO>();
                    dosages.InsertOnSubmit(item);
                    context.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении дозировки: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить существующий объект в репозитории.
        /// </summary>
        /// <param name="item">Обновляемый объект.</param>
        public void Update(DosageModelDAO item)
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<DosageModelDAO> dosages = context.GetTable<DosageModelDAO>();
                    DosageModelDAO existingDosage = dosages.SingleOrDefault(d => d.DosageID == item.DosageID);
                    if (existingDosage != null)
                    {
                        existingDosage.Name = item.Name;
                        existingDosage.DrugID = item.DrugID;
                        existingDosage.Quantity = item.Quantity;
                        existingDosage.MeasurementUnit = item.MeasurementUnit;
                        existingDosage.DosageValue = item.DosageValue;
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении дозировки: {ex.Message}");
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
                    Table<DosageModelDAO> dosages = context.GetTable<DosageModelDAO>();
                    DosageModelDAO dosageToDelete = dosages.SingleOrDefault(d => d.DosageID == id);
                    if (dosageToDelete != null)
                    {
                        dosages.DeleteOnSubmit(dosageToDelete);
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении дозировки: {ex.Message}");
            }
        }

        /// <summary>
        /// Создать таблицу "Dosage" в базе данных.
        /// </summary>
        public void Create()
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    context.ExecuteCommand(@"CREATE TABLE Dosage (
                                        DosageID INT IDENTITY(1, 1) PRIMARY KEY,
                                        Name NVARCHAR(50) NOT NULL,
                                        DrugID INT NOT NULL,
                                        Quantity INT NOT NULL,
                                        MeasurementUnit NVARCHAR(20) NOT NULL,
                                        DosageValue FLOAT NOT NULL
                                    )");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании таблицы Dosage: {ex.Message}");
            }
        }
    }
}
