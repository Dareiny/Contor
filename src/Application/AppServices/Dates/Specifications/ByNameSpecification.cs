using AppServices.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Dates.Specifications
{
    /// <summary>
    /// Спецификация для проверки имени именниника.
    /// </summary>
    public class ByNameSpecification : Specification<Domain.Dates>
    {
        private readonly string _name;
        public ByNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Domain.Dates, bool>> ToExpression()
        {
            return dates => dates.Name == _name;
        }
    }
}