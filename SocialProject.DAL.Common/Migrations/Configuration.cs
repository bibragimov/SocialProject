using System.Data.Entity.Migrations;

namespace SocialProject.DAL.Common.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SocialProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SocialProjectContext context)
        {
        }
    }
}