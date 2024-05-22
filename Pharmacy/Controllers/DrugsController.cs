using System;
using System.Collections.ObjectModel;
using Pharmacy.Interfaces;
using Pharmacy.Models;
using Pharmacy.Repositories;

namespace Pharmacy.Controllers
{
    /// <summary>
    /// Контроллер для управления данными о лекарствах.
    /// </summary>
    public class DrugsController : IController<DrugsModel>
    {
        private readonly DrugsRepository _repository;

        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        public ObservableCollection<DrugsModel> Items { get; set; }

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        public DrugsController()
        {
            _repository = new DrugsRepository();
            Items = new ObservableCollection<DrugsModel>();
            LoadData();
        }

        /// <summary>
        /// Загружает данные о лекарствах.
        /// </summary>
        public void LoadData()
        {
            Items.Clear();
            foreach (var item in _repository.GetAll())
            {
                Items.Add(item);
            }
        }

        /// <summary>
        /// Добавляет новое лекарство.
        /// </summary>
        /// <param name="item">Лекарство для добавления.</param>
        public void Add(DrugsModel item)
        {
            _repository.Add(item);
            LoadData();
        }

        /// <summary>
        /// Удаляет лекарство по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор лекарства для удаления.</param>
        public void Delete(int id)
        {
            _repository.Delete(id);
            LoadData();
        }

        /// <summary>
        /// Обновляет информацию о лекарстве.
        /// </summary>
        /// <param name="item">Лекарство для обновления.</param>
        public void Update(DrugsModel item)
        {
            _repository.Update(item);
            LoadData();
        }

        /// <summary>
        /// Создает новое лекарство.
        /// </summary>
        public void Create()
        {
            _repository.Create();
        }
    }
}