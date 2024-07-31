using AppServices.Dates.Repositories;
using AppServices.Dates.Specifications;
using AppServices.Specifications;
using AutoMapper;
using Contracts.Dates;
using Contracts.General;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Dates.Services
{
    public class DateService : IDateService
    {
        private readonly IDatesRepository _dateRepository;
        private readonly IMapper _mapper;


        /// <summary>
        /// Инициализирует экземпляр <see cref="DateService"/>.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public DateService(IDatesRepository dateRepository, IMapper mapper)
        {
            _dateRepository = dateRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(CreateDateRequest request, string fotoName, string fileUrl, CancellationToken cancellationToken)
        {

            var entity = new Domain.Dates();
            entity.Id = Guid.NewGuid();
            entity.Name = request.Name;
            entity.BirthDate = request.BirthDate.ToUniversalTime();
            entity.FotoName = fotoName;
            entity.FotoUrl = fileUrl;
            await _dateRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _dateRepository.DeleteAsync(id, cancellationToken);
        }

        public async ValueTask<DatesDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dateRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<ResultWithPagination<DatesDto>> GetDatesByNameAsync(PaginationRequest request1, DatesByNameRequest request2, CancellationToken cancellationToken)
        {
            var specification = new ByNameSpecification(request2.Name);

            return await _dateRepository.GetAllBySpecification(request1, specification, cancellationToken);
        }

        public async Task<ResultWithPagination<DatesDto>> GetDatesAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            var specification = new TrueSpecification<Domain.Dates>();
            var dates = await _dateRepository.GetAllBySpecification(request, specification, cancellationToken);

            return dates;
        }

        public async Task UpdateAsync(DatesDto entity, CancellationToken cancellationToken)
        {
            await _dateRepository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<ResultWithPagination<DatesDto>> GetOutdatedDatesAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            var specification = new ByOutdatedSpecification(DateTime.UtcNow);

            return await _dateRepository.GetAllBySpecification(request, specification, cancellationToken);
        }

        public async Task<ResultWithPagination<DatesDto>> GetCurrentDatesAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            var specification = new ByCurrentDatesSpecification(DateTime.UtcNow);

            return await _dateRepository.GetAllBySpecification(request, specification, cancellationToken);
        }

        public async Task<ResultWithPagination<DatesDto>> GetSoonDatesAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            var specification = new BySoonSpecification(DateTime.UtcNow);

            return await _dateRepository.GetAllBySpecification(request, specification, cancellationToken);
        }
    }
}
