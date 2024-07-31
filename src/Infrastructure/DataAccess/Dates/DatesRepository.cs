using AppServices.Dates.Repositories;
using AppServices.Specifications;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Dates;
using Contracts.General;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Dates
{
    public class DatesRepository : IDatesRepository
    {
        private readonly IRepository<Domain.Dates> _repository;
        private readonly IMapper _mapper;
        public DatesRepository(IRepository<Domain.Dates> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(Domain.Dates entity, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<ResultWithPagination<DatesDto>> GetAllBySpecification(PaginationRequest request, Specification<Domain.Dates> specification, CancellationToken cancellationToken)
        {
            var result = new ResultWithPagination<DatesDto>();

            var query = _repository.GetAll().Where(specification.ToExpression());

            var elementsCount = await query.CountAsync(cancellationToken);
            result.AvailablePages = elementsCount / request.BatchSize;
            if (elementsCount % request.BatchSize > 0) result.AvailablePages++;

            var paginationQuery = await query
               .OrderBy(user => user.BirthDate)
               .Skip(request.BatchSize * (request.PageNumber - 1))
               .Take(request.BatchSize)
               .ProjectTo<DatesDto>(_mapper.ConfigurationProvider)
               .ToListAsync();

            result.Result = paginationQuery;
            return result;
        }

        public async Task<DatesDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var date = await _repository.GetByIdAsync(id, cancellationToken);

            var result = _mapper.Map<DatesDto>(date);

            return result;
        }

        public async Task UpdateAsync(DatesDto entity, CancellationToken cancellationToken)
        {
            var date = await _repository.GetByIdAsync(entity.Id, cancellationToken);
            date.BirthDate = entity.BirthDate;
            date.Name = entity.Name;
            await _repository.UpdateAsync(date, cancellationToken);
        }
    }
}
