using System;
using System.Data.Linq.Mapping;

namespace Pharmacy.ModelsDAO
{
    /// <summary>
    /// Модель для представления дозировки лекарственных средств.
    /// </summary>
    [Table(Name = "Dosage")]
    public class DosageModelDAO
    {
        /// <summary>
        /// Уникальный идентификатор дозировки.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int DosageID { get; set; }

        /// <summary>
        /// Название дозировки.
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор лекарства.
        /// </summary>
        [Column]
        public int DrugID { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        [Column]
        public int Quantity { get; set; }

        /// <summary>
        /// Единица измерения.
        /// </summary>
        [Column]
        public string MeasurementUnit { get; set; }

        /// <summary>
        /// Значение дозировки.
        /// </summary>
        [Column]
        public double DosageValue { get; set; }
    }
}
