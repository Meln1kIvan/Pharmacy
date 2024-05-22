using System;
using System.Collections.ObjectModel;
using Pharmacy.Interfaces;
using Pharmacy.ModelsDAO;
using Pharmacy.Models;
using Pharmacy.Repositories;
using System.Linq;

namespace Pharmacy.Controllers
{
    /// <summary>
    /// Контроллер для управления данными о дозировках лекарств.
    /// </summary>
    public class DosageController : IController<DosageModel>
    {
        private readonly DosageRepository _repository;
        private readonly DrugsRepository _drugsRepository;

        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        public ObservableCollection<DosageModel> Items { get; set; }

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        public DosageController()
        {
            _repository = new DosageRepository();
            _drugsRepository = new DrugsRepository();
            Items = new ObservableCollection<DosageModel>();
            LoadData();
        }

        /// <summary>
        /// Загружает данные о дозировках лекарств.
        /// </summary>
        public void LoadData()
        {
            Items.Clear();
            foreach (var item in _repository.GetAll())
            {
                DosageModel model = new DosageModel
                {
                    DosageID = item.DosageID,
                    Name = item.Name,
                    DrugsModel = _drugsRepository.GetById(item.DrugID),
                    Quantity = item.Quantity,
                    MeasurementUnit = item.MeasurementUnit,
                    DosageValue = item.DosageValue
                };
                Items.Add(model);
            }
        }

        /// <summary>
        /// Добавляет новую дозировку лекарства.
        /// </summary>
        /// <param name="item">Дозировка лекарства для добавления.</param>
        public void Add(DosageModel item)
        {
            var daoItem = new DosageModelDAO
            {
                DosageID = item.DosageID,
                Name = item.Name,
                DrugID = item.DrugsModel.DrugsID,
                Quantity = item.Quantity,
                MeasurementUnit = item.MeasurementUnit,
                DosageValue = item.DosageValue,
            };

            _repository.Add(daoItem);
            LoadData();
        }

        /// <summary>
        /// Удаляет дозировку лекарства по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор дозировки лекарства для удаления.</param>
        public void Delete(int id)
        {
            _repository.Delete(id);
            LoadData();
        }

        /// <summary>
        /// Обновляет информацию о дозировке лекарства.
        /// </summary>
        /// <param name="item">Дозировка лекарства для обновления.</param>
        public void Update(DosageModel item)
        {
            var daoItem = new DosageModelDAO
            {
                DosageID = item.DosageID,
                Name = item.Name,
                DrugID = item.DrugsModel.DrugsID,
                Quantity = item.Quantity,
                MeasurementUnit = item.MeasurementUnit,
                DosageValue = item.DosageValue,
            };
            _repository.Update(daoItem);
            LoadData();
        }

        public void FilterByDrug(int drugId)
        {
            Items.Clear();
            foreach (var item in _repository.GetAll().Where(drug => drug.DrugID == drugId))
            {
                DosageModel model = new DosageModel
                {
                    DosageID = item.DosageID,
                    Name = item.Name,
                    DrugsModel = _drugsRepository.GetById(item.DrugID),
                    Quantity = item.Quantity,
                    MeasurementUnit = item.MeasurementUnit,
                    DosageValue = item.DosageValue
                };
                Items.Add(model);
            }
        }

        /// <summary>
        /// Создает новую дозировку лекарства.
        /// </summary>
        public void Create()
        {
            _repository.Create();
        }
    }
}
