using Contracts.Dates;
using Contracts.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Dates.Services
{
    /// <summary>
    /// Сервис работы с пользователями.
    /// </summary>
    public interface IDateService
    {
        /// <summary>
        /// Возвращает все даты.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список дат <see cref="DatesDto"/>.</returns>
        Task<ResultWithPagination<DatesDto>> GetDatesAsync(PaginationRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все прошедшие даты.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список прошедших дат <see cref="DatesDto"/>.</returns>
        Task<ResultWithPagination<DatesDto>> GetOutdatedDatesAsync(PaginationRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все текущих дат.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список текущих дат <see cref="DatesDto"/>.</returns>
        Task<ResultWithPagination<DatesDto>> GetCurrentDatesAsync(PaginationRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все ближайших дат.
        /// </summary>
        /// <param name="request">Запрос на создание страниц.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список ближайших дат <see cref="DatesDto"/>.</returns>
        Task<ResultWithPagination<DatesDto>> GetSoonDatesAsync(PaginationRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает даты по имени.
        /// </summary>
        /// <param name="request1">Запрос на пагинацию</param>
        /// <param name="request2">Запрос по имени.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="UserDTO"/>.</returns>
        Task<ResultWithPagination<DatesDto>> GetDatesByNameAsync(PaginationRequest request1, DatesByNameRequest request2, CancellationToken cancellationToken);


        /// <summary>
        /// Возвращает дату по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="DatesDto"/>.</returns>
        ValueTask<DatesDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет запись.
        /// </summary>
        /// <param name="request">Запись.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="Guid"/>.</returns>
        Task<Guid> AddAsync(CreateDateRequest request, string fotoName, string fileUrl, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет записи.
        /// </summary>
        /// <param name="entity">Записи.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task UpdateAsync(DatesDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
