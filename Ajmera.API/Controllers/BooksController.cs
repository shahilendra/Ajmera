using Ajmera.DTO;
using Ajmera.Service.Abstraction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ajmera.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;
        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IEnumerable<BookDTO>> Get()
        {
            try
            {
                _logger.LogInformation($"{nameof(BooksController)}.{nameof(Get)} Started");
                var result = await _bookService.Get();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BooksController)}.{nameof(Get)} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> Get(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BooksController)}.{nameof(Get)}/{id} Started");
                var result = await _bookService.Get(id);
                if (result == null)
                    return BadRequest();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BooksController)}.{nameof(Get)}/{id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Post([FromBody] BookDTO bookDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(BooksController)}.{nameof(Post)}/{bookDTO.Name} Started");
                if (bookDTO == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var book = await _bookService.GetByName(bookDTO.Name);
                if (book != null)
                {
                    ModelState.AddModelError("Name", "Book Name already in use");
                    return BadRequest(ModelState);
                }

                var result = await _bookService.Add(bookDTO);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BooksController)}.{nameof(Post)}/{bookDTO.Name} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> Put(int id, [FromBody] BookDTO bookDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(BooksController)}.{nameof(Get)}/{id} Started");

                if (bookDTO == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                bookDTO.Id = bookDTO.Id == 0 ? id : bookDTO.Id;
                await _bookService.Update(bookDTO);
                return await _bookService.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BooksController)}.{nameof(Get)}/{bookDTO.Name} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BooksController)}.{nameof(Get)}/{id} Started");

                var book = await _bookService.Get(id);
                if (book == null)
                {
                    ModelState.AddModelError("Id", $"Book Id: {id} dose not exist in db");
                    return BadRequest(ModelState);
                }

                await _bookService.Delete(id);
                return Ok($"Book Id {id} deleted successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BooksController)}.{nameof(Get)}/{id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
    }
}
