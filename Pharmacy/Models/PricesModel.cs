using System.Data.Linq.Mapping;

namespace Pharmacy.Models
{
    /// <summary>
    /// Модель для представления цен на лекарства.
    /// </summary>
    [Table(Name = "Prices")]
    public class PricesModel
    {
        /// <summary>
        /// Уникальный идентификатор цены.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int PricesID { get; set; }

        /// <summary>
        /// Название цены.
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор лекарства.
        /// </summary>
        [Column]
        public DrugsModel DrugsModel { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [Column]
        public decimal Price { get; set; }
    }
}