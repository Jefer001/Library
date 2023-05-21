using Microsoft.EntityFrameworkCore;

namespace Library_Loan.DAL
{
    public class DataBaseContex : DbContext
    {
        #region Builder
        public DataBaseContext(DbContextOptions<DataBaseContext> option) : base(option)
        {
        }
        #endregion
    }
}
