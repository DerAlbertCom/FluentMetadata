using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class DbContextTest : IUseFixture<MetadataSetup>
    {
        public DbContextTest()
        {
            Database.SetInitializer(new AlwaysRecreateDatabase<RegularlyDbContext>());
        }

        [Fact]
        public void CanCreateDbContext()
        {
            var context = new RegularlyDbContext();
            context.Database.Create();
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