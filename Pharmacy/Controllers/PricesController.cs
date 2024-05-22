using System;
using System.Collections.ObjectModel;
using Pharmacy.Interfaces;
using Pharmacy.ModelsDAO;
using Pharmacy.Repositories;
using Pharmacy.Models;
using System.Linq;

namespace Pharmacy.Controllers
{
    /// <summary>
    /// Контроллер для управления данными о ценах на лекарства.
    /// </summary>
    public class PricesController : IController<PricesModel>
    {
        private readonly PricesRepository _repository;
        private readonly DrugsRepository _drugsRepository;

        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        public ObservableCollection<PricesModel> Items { get; set; }

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        public PricesController()
        {
            _repository = new PricesRepository();
            _drugsRepository = new DrugsRepository();
            Items = new ObservableCollection<PricesModel>();
            LoadData();
        }

        /// <summary>
        /// Загружает данные о ценах на лекарства.
        /// </summary>
        public void LoadData()
        {
            Items.Clear();
            foreach (var item in _repository.GetAll())
            {
                PricesModel model = new PricesModel
                {
                    PricesID = item.PricesID,
                    Name = item.Name,
                    DrugsModel = _drugsRepository.GetById(item.DrugID),
                    Price = item.Price
                };
                Items.Add(model);
            }
        }

        /// <summary>
        /// Добавляет новую цену на лекарство.
        /// </summary>
        /// <param name="item">Цена на лекарство для добавления.</param>
        public void Add(PricesModel item)
        {
            var daoItem = new PricesModelDAO
            {
                PricesID = item.PricesID,
                Name = item.Name,
                DrugID = item.DrugsModel.DrugsID,
                Price = item.Price
            };

            _repository.Add(daoItem);
            LoadData();
        }

        /// <summary>
        /// Удаляет цену на лекарство по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор цены на лекарство для удаления.</param>
        public void Delete(int id)
        {
            _repository.Delete(id);
            LoadData();
        }

        /// <summary>
        /// Обновляет информацию о цене на лекарство.
        /// </summary>
        /// <param name="item">Цена на лекарство для обновления.</param>
        public void Update(PricesModel item)
        {
            var daoItem = new PricesModelDAO
            {
                PricesID = item.PricesID,
                Name = item.Name,
                DrugID = item.DrugsModel.DrugsID,
                Price = item.Price
            };

            _repository.Update(daoItem);
            LoadData();
        }

        public void FilterByDrug(int drugId)
        {
            Items.Clear();
            foreach (var item in _repository.GetAll().Where(drug => drug.DrugID == drugId))
            {
                PricesModel model = new PricesModel
                {
                    PricesID = item.PricesID,
                    Name = item.Name,
                    DrugsModel = _drugsRepository.GetById(item.DrugID),
                    Price = item.Price
                };
                Items.Add(model);
            }
        }

        public void FilterByPriceRange(decimal minPrice, decimal maxPrice)
        {
            Items.Clear();
            foreach (var item in _repository.GetAll().Where(price => price.Price >= minPrice && price.Price <= maxPrice))
            {
                PricesModel model = new PricesModel
                {
                    PricesID = item.PricesID,
                    Name = item.Name,
                    DrugsModel = _drugsRepository.GetById(item.DrugID),
                    Price = item.Price
                };
                Items.Add(model);
            }
        }

        /// <summary>
        /// Создает новую цену на лекарство.
        /// </summary>
        public void Create()
        {
            _repository.Create();
        }
    }
}