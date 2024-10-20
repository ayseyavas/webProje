using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using webProje.Utility;

namespace webProje.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDBContext _appDBContext;
        internal DbSet<T> dbSet;

        public Repository(AppDBContext appDBContext)
        {

            this._appDBContext = appDBContext;
            this.dbSet = _appDBContext.Set<T>();
            _appDBContext.Books.Include(k => k.BookType);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteBetween(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> expression, string? includeProps = null)
        {
            IQueryable<T> query = dbSet.Where(expression);

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }

            return query.FirstOrDefault();//Ya da return (T)query
        }

        //public T Get(Expression<Func<T, bool>> expression)
        //{
        //    IQueryable<T> query = dbSet.Where(expression); // Where ifadesini buraya taşıyoruz
        //    return query.FirstOrDefault(); // İlk eşleşen kaydı döndürüyoruz
        //}


        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries) )
                {
                    query=query.Include(includeProp);
                }
                
            }
            return query.ToList();

            //public void Update(T entity)
            //{
            //    throw new NotImplementedException();
            //}
        }
    }}
