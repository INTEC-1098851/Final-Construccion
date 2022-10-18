using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetOneAsync(T entity);
        Task<bool> ExistsAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(T entity);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> ChangeStatusAsync(T entity);

    }
}
