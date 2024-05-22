using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Connection
{
    /// <summary>
    /// Singleton класс для хранения настроек базы данных.
    /// </summary>
    public class DatabaseSettings
    {
        private static DatabaseSettings _instance;
        private string _connectionString;

        /// <summary>
        /// Приватный конструктор для создания единственного экземпляра класса и установки строки подключения к базе данных.
        /// </summary>
        private DatabaseSettings()
        {
            _connectionString = @"Data Source=LEGION\SQLEXPRESS;Initial Catalog=Pharmacy;Integrated Security=True";
        }

        /// <summary>
        /// Получает единственный экземпляр класса DatabaseSettings.
        /// </summary>
        public static DatabaseSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseSettings();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Получает строку подключения к базе данных.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
        }
    }
}
