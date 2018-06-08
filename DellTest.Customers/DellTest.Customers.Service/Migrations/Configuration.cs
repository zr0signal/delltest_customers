using System.Data.Entity.Migrations;

namespace DellTest.Customers.Service.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CustomerServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CustomerServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}