using System.Data.Entity;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    //TODO reinclude tests after updating to newest version of Entity Framework
    public class EntityFrameworkAdapterTests
    {
        private DbModelBuilder modelBuilder;
        private EntityFrameworkAdapter adapter;

        public EntityFrameworkAdapterTests()
        {
            modelBuilder = new DbModelBuilder();
            adapter = new EntityFrameworkAdapter();
        }

        [Fact(Skip = "This test was written against an outdated version of Entity Framework")]
        public void Can_Map_WebUser()
        {
            adapter.MapProperties(modelBuilder.Entity<WebUser>());
        }

        [Fact(Skip = "This test was written against an outdated version of Entity Framework")]
        public void Can_Map_Content()
        {
            adapter.MapProperties(modelBuilder.Entity<Content>());
        }

        [Fact(Skip = "This test was written against an outdated version of Entity Framework")]
        public void Can_Map_Layout()
        {
            adapter.MapProperties(modelBuilder.Entity<Layout>());
        }
    }
}