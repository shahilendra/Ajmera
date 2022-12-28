using Ajmera.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ajmera.Service.Abstraction
{
    public interface IBookService
    {
        Task<List<BookDTO>> Get();
        Task<BookDTO> Get(int id);
        Task<BookDTO> GetByName(string name);
        Task<BookDTO> Add(BookDTO book);
        Task Update(BookDTO book);
        Task<int> Delete(int id);
    }
}
