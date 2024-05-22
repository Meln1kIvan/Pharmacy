using System;
using System.Data.Linq.Mapping;

namespace Pharmacy.Models
{
    /// <summary>
    /// Модель для представления лекарственных средств.
    /// </summary>
    [Table(Name = "Drugs")]
    public class DrugsModel
    {
        /// <summary>
        /// Уникальный идентификатор лекарства.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int DrugsID { get; set; }

        /// <summary>
        /// Название лекарства.
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// Форма выпуска лекарства.
        /// </summary>
        [Column]
        public string Form { get; set; }

        /// <summary>
        /// Производитель лекарства.
        /// </summary>
        [Column]
        public string Manufacturer { get; set; }
    }
}