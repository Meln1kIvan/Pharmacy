using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Получить все объекты из репозитория.
        /// </summary>
        /// <returns>Список всех объектов.</returns>
        List<T> GetAll();

        /// <summary>
        /// Получить объект из репозитория по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns>Объект с указанным идентификатором или null, если объект не найден.</returns>
        T GetById(int id);

        /// <summary>
        /// Добавить новый объект в репозиторий.
        /// </summary>
        /// <param name="item">Добавляемый объект.</param>
        void Add(T item);

        /// <summary>
        /// Обновить существующий объект в репозитории.
        /// </summary>
        /// <param name="item">Обновляемый объект.</param>
        void Update(T item);

        /// <summary>
        /// Удалить объект из репозитория по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        void Delete(int id);

        /// <summary>
        /// Создать таблицу в базе данных.
        /// </summary>
        void Create();
    }
}
