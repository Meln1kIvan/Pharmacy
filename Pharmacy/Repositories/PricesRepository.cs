using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using Pharmacy.Connection;
using Pharmacy.Interfaces;
using Pharmacy.ModelsDAO;

namespace Pharmacy.Repositories
{
    /// <summary>
    /// Репозиторий для работы с данными о ценах на лекарства.
    /// </summary>
    public class PricesRepository : IRepository<PricesModelDAO>
    {
        private readonly string _connectionString = DatabaseSettings.Instance.ConnectionString;

        /// <summary>
        /// Получить все объекты из репозитория.
        /// </summary>
        /// <returns>Список всех объектов.</returns>
        public List<PricesModelDAO> GetAll()
        {
            List<PricesModelDAO> pricesList = new List<PricesModelDAO>();

            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<PricesModelDAO> prices = context.GetTable<PricesModelDAO>();
                    pricesList = prices.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о ценах: {ex.Message}");
            }

            return pricesList;
        }

        /// <summary>
        /// Получить объект из репозитория по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns>Объект с указанным идентификатором или null, если объект не найден.</returns>
        public PricesModelDAO GetById(int id)
        {
            PricesModelDAO price = null;

            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<PricesModelDAO> prices = context.GetTable<PricesModelDAO>();
                    price = prices.SingleOrDefault(p => p.PricesID == id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о цене: {ex.Message}");
            }

            return price;
        }

        /// <summary>
        /// Добавить новый объект в репозиторий.
        /// </summary>
        /// <param name="item">Добавляемый объект.</param>
        public void Add(PricesModelDAO item)
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<PricesModelDAO> prices = context.GetTable<PricesModelDAO>();
                    prices.InsertOnSubmit(item);
                    context.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении цены: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновить существующий объект в репозитории.
        /// </summary>
        /// <param name="item">Обновляемый объект.</param>
        public void Update(PricesModelDAO item)
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    Table<PricesModelDAO> prices = context.GetTable<PricesModelDAO>();
                    PricesModelDAO existingPrice = prices.SingleOrDefault(p => p.PricesID == item.PricesID);
                    if (existingPrice != null)
                    {
                        existingPrice.Name = item.Name;
                        existingPrice.DrugID = item.DrugID;
                        existingPrice.Price = item.Price;
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении цены: {ex.Message}");
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
                    Table<PricesModelDAO> prices = context.GetTable<PricesModelDAO>();
                    PricesModelDAO priceToDelete = prices.SingleOrDefault(p => p.PricesID == id);
                    if (priceToDelete != null)
                    {
                        prices.DeleteOnSubmit(priceToDelete);
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении цены: {ex.Message}");
            }
        }

        /// <summary>
        /// Создать таблицу "Prices" в базе данных.
        /// </summary>
        public void Create()
        {
            try
            {
                using (DataContext context = new DataContext(_connectionString))
                {
                    context.ExecuteCommand(@"CREATE TABLE Prices (
                                        PricesID INT IDENTITY(1, 1) PRIMARY KEY,
                                        Name NVARCHAR(50) NOT NULL,
                                        DrugID INT NOT NULL,
                                        Price DECIMAL(18, 2) NOT NULL
                                    )");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании таблицы Prices: {ex.Message}");
            }
        }
    }
}