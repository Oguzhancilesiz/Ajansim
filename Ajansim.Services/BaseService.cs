using Ajansim.Context;
using Ajansim.Core.Abstarcts;
using Ajansim.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Services
{
    public class BaseService<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AjansimDBContext _DbContext;
        private readonly DbSet<T> _table;
        public BaseService()
        {
            _DbContext = new AjansimDBContext();
            _table = _DbContext.Set<T>();
        }
        public void Add(T item)
        {
            _table.Add(item);
            Save();
        }

        public void Delete(T item)
        {
            _table.Remove(item);
            Save();
        }

        public IQueryable<T> GetAll()
        {
            return _table.AsNoTracking().Where(x => x.Status != Status.Deleted);
        }

        public List<T> GetAllFilter(Func<T, bool> exp, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = GetAll();
            if (include != null)
            {
                query = include(query);
            }

            return query.Where(exp).ToList();
        }

        public T GetBy(Func<T, bool> exp)
        {
            return _table.AsNoTracking().Where(exp).FirstOrDefault();
        }

        public T GetById(Guid id)
        {
            return _table.AsNoTracking().FirstOrDefault(x => x.ID == id);
        }

        public void Update(T item)
        {
            _table.Update(item);
            Save();
        }

        private void Save()
        {
            try
            {
                _DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
