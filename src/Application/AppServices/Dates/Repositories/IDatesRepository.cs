using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Specifications;
using Contracts.Dates;
using Contracts.General;

namespace AppServices.Dates.Repositories
{
    public interface IDatesRepository
    {
        /// <summary>
        /// Возвращает даты по заданной спецификацией логике.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="specification">Спецификация.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        /// <returns>Список всех пользователей <see cref="UserInfoDTO"/>.</returns>
        Task<ResultWithPagination<DatesDto>> GetAllBySpecification(PaginationRequest request, Specification<Domain.Dates> specification, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает дату по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="UserDTO"/>.</returns>
        Task<DatesDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет дату.
        /// </summary>
        /// <param name="entity">Запись.</param>
        /// <returns><see cref="Guid"/>.</returns>
        Task AddAsync(Domain.Dates entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет дату.
        /// </summary>
        /// <param name="entity">Запись.</param>
        /// <returns></returns>
        Task UpdateAsync(DatesDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет дату.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
