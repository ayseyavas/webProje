using webProje.Utility;

namespace webProje.Models
{
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
    {
        private AppDBContext _appDBContext;

        public BookTypeRepository(AppDBContext appDBContext) : base(appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public void Save()
        {
            _appDBContext.SaveChanges();
        }

        public void Update(BookType bookType)
        {
            _appDBContext.Update(bookType);
        }
    }
}
