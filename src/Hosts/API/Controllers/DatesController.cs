using AppServices.Dates.Services;
using Contracts.Dates;
using Contracts.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatesController : ControllerBase
    {
        private readonly IDateService _dateService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public DatesController(
            IDateService userService,
            IConfiguration configuration,
            IWebHostEnvironment environment
            )
        {
            _dateService = userService;
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// Возвращает список всех дат.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список всех дат по страницам <see cref="OkObjectResult"/>.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ResultWithPagination<DatesDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllDates([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            var result = await _dateService.GetDatesAsync(request, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Добавляет дату.
        /// </summary>
        /// <param name="request">Запрос на создание.</param>
        /// <param name="file">Файл хранящий фотографию.</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddDate([FromQuery] CreateDateRequest request, IFormFile file, CancellationToken cancellationToken)
        {
            if (file != null && file.Length > 0)
            {
                // Генерация уникального имени файла
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                // Путь для сохранения файла
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                // Убедитесь, что директория существует
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Сохранение файла
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream, cancellationToken);
                }

                // Сохранение данных в БД
                var dateId = await _dateService.AddAsync(request, fileName, filePath, cancellationToken);

                // Создание ответа
                return CreatedAtAction(nameof(AddDate), new { id = dateId });
            }

            return BadRequest("Файл не был загружен");
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <param name="request">Запрос обновления.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="OkObjectResult"/>.</returns>
        [HttpPut("id")]
        [ProducesResponseType(typeof(IEnumerable<DatesDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateDate([FromQuery] UpdateDateRequest request, CancellationToken cancellationToken)
        {


            var dateToUpdate = await _dateService.GetByIdAsync(request.Id, cancellationToken);
            if (dateToUpdate == null)
            {
                return NotFound("Date not found.");
            }

            //Проверки наличия поля, если нет, то в БД не меняем на Null
            if (!string.IsNullOrEmpty(request.Name))
            {
                dateToUpdate.Name = request.Name;
            }

            if (request.BirthDate == DateTime.MinValue)
            {
                dateToUpdate.BirthDate = request.BirthDate;
            }




            await _dateService.UpdateAsync(dateToUpdate, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет дату.
        /// </summary>
        /// <param name="id">Идентификатор даты</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><see cref="OkObjectResult"/>.</returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDate([FromQuery] Guid id,CancellationToken cancellationToken)
        {

            var user = await _dateService.GetByIdAsync(id, cancellationToken);
            if (user == null) return NotFound();
            if (System.IO.File.Exists(user.FotoUrl))
            {
                System.IO.File.Delete(user.FotoUrl);
            }

            await _dateService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }


        /// <summary>
        /// Возвращает список всех прошедших дат.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список всех прошедших дат по страницам <see cref="OkObjectResult"/>.</returns>
        [HttpGet("all-outdated")]
        [ProducesResponseType(typeof(ResultWithPagination<DatesDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllOutdatedDates([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            
            var result = await _dateService.GetOutdatedDatesAsync(request, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Возвращает список всех текущих дат.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список всех текущих дат по страницам <see cref="OkObjectResult"/>.</returns>
        [HttpGet("all-current")]
        [ProducesResponseType(typeof(ResultWithPagination<DatesDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllCurrentDates([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {

            var result = await _dateService.GetCurrentDatesAsync(request, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Возвращает список всех ближайших дат.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список всех ближайших дат по страницам <see cref="OkObjectResult"/>.</returns>
        [HttpGet("all-soon")]
        [ProducesResponseType(typeof(ResultWithPagination<DatesDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllSoonDates([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {

            var result = await _dateService.GetSoonDatesAsync(request, cancellationToken);

            return Ok(result);
        }



        private async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();

        }
    }
}
