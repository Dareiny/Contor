using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain 
{
    /// <summary>
    /// Сущность для дней рождений.
    /// </summary>
    public class Dates : BaseEntity
    {
        /// <summary>
        /// Имя именинника.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата дня рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Имя файла изображения.
        /// </summary>
        public string FotoName { get; set; }

        /// <summary>
        /// URL изображения.
        /// </summary>
        public string FotoUrl { get; set; }

    }
}
