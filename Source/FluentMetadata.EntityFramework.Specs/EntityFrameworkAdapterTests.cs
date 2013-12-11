using System.Data.Entity.ModelConfiguration;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    //TODO reinclude tests after updating to newest version of Entity Framework
    public class EntityFrameworkAdapterTests
    {
        private ModelBuilder modelBuilder;
        private EntityFrameworkAdapter adapter;

        public EntityFrameworkAdapterTests()
        {
            modelBuilder = new ModelBuilder();
            adapter = new EntityFrameworkAdapter();
        }

        [Fact(Skip = "This test was written against an outdated version of Entity Framework")]
        public void Can_Map_WebUser()
        {
            var configuration = modelBuilder.Entity<WebUser>();
            adapter.MapProperties(typeof(WebUser),configuration);
        }

        [Fact(Skip = "This test was written against an outdated version of Entity Framework")]
        public void Can_Map_Content()
        {
            var configuration = modelBuilder.Entity<Content>();
            adapter.MapProperties(typeof(Content), configuration);
        }

        [Fact(Skip = "This test was written against an outdated version of Entity Framework")]
        public void Can_Map_Layout()
        {
            var configuration = modelBuilder.Entity<Layout>();
            adapter.MapProperties(typeof(Layout), configuration);
        }

    }
}