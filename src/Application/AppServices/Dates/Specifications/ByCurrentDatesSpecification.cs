﻿using AppServices.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Dates.Specifications
{
    public class ByCurrentDatesSpecification : Specification<Domain.Dates>
    {
        private DateTime _date;
        public ByCurrentDatesSpecification(DateTime date)
        {
            _date = date;
        }

        public override Expression<Func<Domain.Dates, bool>> ToExpression()
        {
            return dates => dates.BirthDate == _date;
        }
    }
}
