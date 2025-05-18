using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Core.Abstarcts
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        T GetBy(Func<T, bool> exp);
        T GetById(object id);
        List<T> GetAll();
        List<T> GetAllFilter(Func<T, bool> exp);

    }
}
