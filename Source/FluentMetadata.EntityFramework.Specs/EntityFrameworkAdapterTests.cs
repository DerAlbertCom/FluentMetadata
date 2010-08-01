using System.Data.Entity.ModelConfiguration;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class EntityFrameworkAdapterTests
    {
        private ModelBuilder modelBuilder;
        private EntityFrameworkAdapter adapter;

        public EntityFrameworkAdapterTests()
        {
            modelBuilder = new ModelBuilder();
            adapter = new EntityFrameworkAdapter();
        }

        [Fact]
        public void Can_Map_WebUser()
        {
            var configuration = modelBuilder.Entity<WebUser>();
            adapter.MapProperties(typeof(WebUser),configuration);
        }

        [Fact]
        public void Can_Map_Content()
        {
            var configuration = modelBuilder.Entity<Content>();
            adapter.MapProperties(typeof(Content), configuration);
        }

        [Fact]
        public void Can_Map_Layout()
        {
            var configuration = modelBuilder.Entity<Layout>();
            adapter.MapProperties(typeof(Layout), configuration);
        }

    }
}