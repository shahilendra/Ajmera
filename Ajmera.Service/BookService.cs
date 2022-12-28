using Ajmera.DTO;
using Ajmera.Model;
using Ajmera.Repository.Abstraction;
using Ajmera.Service.Abstraction;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajmera.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, ILogger<BookService> logger, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<BookDTO>> Get()
        {
            try
            {
                _logger.LogInformation($"{nameof(BookService)}.{nameof(Get)} Started");
                var result = await _bookRepository.Get();
                return _mapper.Map<List<BookDTO>>(result);
                //return result.Select(x => new BookDTO
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    AuthorName = x.AuthorName
                //}).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookService)}.{nameof(Get)} getting error Message: {ex.Message}", ex);
                throw;
            }

        }

        public async Task<BookDTO> Get(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookService)}.{nameof(Get)}/{id} Started");
                var result = await _bookRepository.Get(id);
                if (result != null)
                    return _mapper.Map<BookDTO>(result);
                else
                    return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookService)}.{nameof(Get)}/{id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<BookDTO> GetByName(string name)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookService)}.{nameof(Get)}/{name} Started");
                var result = await this.Get();
                return result.Where(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookService)}.{nameof(Get)}/{name} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<BookDTO> Add(BookDTO bookDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookService)}.{nameof(Add)}/{bookDTO.Name} Started");
                var book= _mapper.Map<Book>(bookDTO);
                var result = await _bookRepository.Add(book);
                return await this.Get(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookService)}.{nameof(Add)}/{bookDTO.Name} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
        public async Task Update(BookDTO bookDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookService)}.{nameof(Update)}/{bookDTO.Id} Started");
                var book = _mapper.Map<Book>(bookDTO);
                //var book = new Book();
                //book.Id = bookDTO.Id;
                //book.Name = bookDTO.Name;
                //book.AuthorName = bookDTO.AuthorName;
                await _bookRepository.Update(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookService)}.{nameof(Update)}/{bookDTO.Id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookService)}.{nameof(Delete)}/{id} Started");
                return await _bookRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookService)}.{nameof(Delete)}/{id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }

    }
}
