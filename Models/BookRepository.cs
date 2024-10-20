using webProje.Utility;

namespace webProje.Models
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private AppDBContext _appDBContext;

        public BookRepository(AppDBContext appDBContext) : base(appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public void Save()
        {
            _appDBContext.SaveChanges();
        }

        public void Update(Book book)
        {
            _appDBContext.Update(book);
        }
    }
}
