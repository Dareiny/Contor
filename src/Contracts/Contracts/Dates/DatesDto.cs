using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dates
{
    /// <summary>
    /// Модель дат.
    /// </summary>
    public class DatesDto
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public Guid Id { get; set; }

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
