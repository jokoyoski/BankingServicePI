using BankingAPI.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentState> PaymentStates { get; set; }



    }

}
