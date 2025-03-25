using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils.Interfaces
{
    public interface IGenericService<D, M>
        where D : class
        where M : class
    {
        // Các phương thức đồng bộ
        IEnumerable<M> GetAll();
        M GetById(int id);
        void Insert(M entity);
        void Update(M entity);
        void Delete(int id);


        // Các phương thức bất đồng bộ
        Task<IEnumerable<M>> GetAllAsync();
        Task<M> GetByIdAsync(int id);
        Task InsertAsync(M entity);
        Task UpdateAsync(M entity);
        Task DeleteAsync(int id);
    }
}
