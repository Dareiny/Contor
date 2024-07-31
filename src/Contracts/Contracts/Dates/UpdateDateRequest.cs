using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dates
{
    public class UpdateDateRequest
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
    }
}
