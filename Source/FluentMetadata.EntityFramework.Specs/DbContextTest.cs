using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class DbContextTest : IUseFixture<MetadataSetup>
    {
        public DbContextTest()
        {
            Database.SetInitializer(new AlwaysRecreateDatabase<RegularlyDbContext>());
        }

        //TODO refactor this test. It breaks the build if Entity Framework is not installed
        [Fact(Skip = "This test requires Entity Framework to be installed")]
        public void CanCreateDbContext()
        {
            if (File.Exists("TestDatabase.sdf"))
            {
                File.Delete("TestDatabase.sdf");
            }
            var context = new RegularlyDbContext();
        }

        public void SetFixture(MetadataSetup data)
        {
        }
    }

    public class NoDatabaseCreate<T> : IDatabaseInitializer<T> where T : DbContext
    {
        public void InitializeDatabase(T context)
        {
        }
    }
}