using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dates
{
    public class DatesByNameRequest
    {
        /// <summary>
        /// Имя именнинека.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
