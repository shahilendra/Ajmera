using Ajmera.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Ajmera.Repository.Abstraction;

namespace Ajmera.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly AjmeraContext _context;
        private readonly ILogger<BookRepository> _logger;
        public BookRepository(AjmeraContext context, ILogger<BookRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<Book>> Get()
        {
            try
            {
                _logger.LogInformation($"{nameof(BookRepository)}.{nameof(Get)} Started");
                return await _context.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookRepository)}.{nameof(Get)} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<Book> Get(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookRepository)}.{nameof(Get)}/{id} Started");
                return await _context.Books.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookRepository)}.{nameof(Get)}/{id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<int> Add(Book book)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookRepository)}.{nameof(Add)}/{book.Name} Started");
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                return book.Id;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookRepository)}.{nameof(Add)}/{book.Name} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
        public async Task Update(Book book)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookRepository)}.{nameof(Update)}/{book.Id} Started");
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookRepository)}.{nameof(Update)}/{book.Id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<int> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(BookRepository)}.{nameof(Delete)}/{id} Started");
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                }
                return book.Id;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BookRepository)}.{nameof(Delete)}/{id} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
    }
}
