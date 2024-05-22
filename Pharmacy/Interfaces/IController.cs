using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
    /// <summary>
    /// Интерфейс контроллера для управления элементами типа T.
    /// </summary>
    /// <typeparam name="T">Тип элемента, с которым работает контроллер.</typeparam>
    public interface IController<T>
    {
        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        ObservableCollection<T> Items { get; set; }

        /// <summary>
        /// Загружает данные.
        /// </summary>
        void LoadData();

        /// <summary>
        /// Добавляет элемент.
        /// </summary>
        /// <param name="item">Элемент для добавления.</param>
        void Add(T item);

        /// <summary>
        /// Удаляет элемент по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента для удаления.</param>
        void Delete(int id);

        /// <summary>
        /// Обновляет элемент.
        /// </summary>
        /// <param name="item">Элемент для обновления.</param>
        void Update(T item);

        /// <summary>
        /// Создает новый элемент.
        /// </summary>
        void Create();
    }
}
