using webProje.Utility;

namespace webProje.Models
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        private AppDBContext _appDBContext;

        public RentRepository(AppDBContext appDBContext) : base(appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public void Save()
        {
            _appDBContext.SaveChanges();
        }

        public void Update(Rent rent)
        {
            _appDBContext.Update(rent);
        }
    }
}
