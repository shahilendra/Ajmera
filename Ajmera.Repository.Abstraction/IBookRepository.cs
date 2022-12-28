using Ajmera.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ajmera.Repository.Abstraction
{
    public interface IBookRepository
    {
        Task<List<Book>> Get();
        Task<Book> Get(int id);
        Task<int> Add(Book book);
        Task Update(Book book);
        Task<int> Delete(int id);

    }
}
