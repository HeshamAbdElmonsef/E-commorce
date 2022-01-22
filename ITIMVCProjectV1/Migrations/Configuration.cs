namespace ITIMVCProjectV1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ITIMVCProjectV1.Models.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ITIMVCProjectV1.Models.DB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Admins.Add(new Models.Admin
            {
                Name = "Hesham Mohammed",
                UserName = "Admin@gmail.com",
                password = "12345678"
            });
            base.Seed(context);
        }
    }
}
