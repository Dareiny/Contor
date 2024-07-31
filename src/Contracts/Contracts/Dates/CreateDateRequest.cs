using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dates
{
    public class CreateDateRequest
    {
        /// <summary>
        /// Имя именинника.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Дата дня рождения.
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
