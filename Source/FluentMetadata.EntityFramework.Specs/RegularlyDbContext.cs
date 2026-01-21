using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    public class RegularlyDbContext : DbContext
    {
        public RegularlyDbContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var adapter = new EntityFrameworkAdapter();

            adapter.MapProperties(Entity<WebUser>(modelBuilder));//.MapSingleType();
            adapter.MapProperties(Entity<WebSite>(modelBuilder));//.MapSingleType();
            adapter.MapProperties(Entity<RemoteAction>(modelBuilder).HasKey(e => new { e.ObjectId, e.Action }));//.MapSingleType();

            adapter.MapProperties(ConfigureSetting(modelBuilder));//.MapSingleType();
            adapter.MapProperties(ConfigureTag(modelBuilder));//.MapSingleType();
            adapter.MapProperties(ConfigureMailTemplate(modelBuilder));//.MapSingleType();

            adapter.MapProperties(ConfigureContent<Content>(modelBuilder));//.MapHierarchy();
            adapter.MapProperties(ConfigureContentBase<ContentBase>(modelBuilder));//.MapHierarchy();
            adapter.MapProperties(ConfigureComment(modelBuilder));//.MapSingleType();
            adapter.MapProperties(ConfigurePicture(modelBuilder));//.MapSingleType();
            adapter.MapProperties(ConfigureGallery(modelBuilder));//.MapSingleType();
            adapter.MapProperties(ConfigureLayout(modelBuilder));//.MapSingleType();
            adapter.MapProperties(ConfigureNews(modelBuilder));//.MapSingleType();
            adapter.MapProperties(ConfigureArticle(modelBuilder));//.MapSingleType();
        }

        private EntityTypeConfiguration<Layout> ConfigureLayout(DbModelBuilder modelBuilder)
        {
            var configuration = Entity<Layout>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityTypeConfiguration<News> ConfigureNews(DbModelBuilder modelBuilder)
        {
            return ConfigureContentBase<News>(modelBuilder);
        }

        private EntityTypeConfiguration<Article> ConfigureArticle(DbModelBuilder modelBuilder)
        {
            return ConfigureContentBase<Article>(modelBuilder);
        }

        private EntityTypeConfiguration<T> ConfigureContentBase<T>(DbModelBuilder modelBuilder) where T : ContentBase
        {
            var configuration = Entity<T>(modelBuilder);
            //configuration.HasRequired(e => e.Layout);
            return ContentConfiguration(configuration);
        }

        private EntityTypeConfiguration<MailTemplate> ConfigureMailTemplate(DbModelBuilder modelBuilder)
        {
            var configuration = Entity<MailTemplate>(modelBuilder);
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }

        private EntityTypeConfiguration<Picture> ConfigurePicture(DbModelBuilder modelBuilder)
        {
            var configuration = Entity<Picture>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityTypeConfiguration<Comment> ConfigureComment(DbModelBuilder modelBuilder)
        {
            var configuration = Entity<Comment>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityTypeConfiguration<Setting> ConfigureSetting(DbModelBuilder modelBuilder)
        {
            var configuration = Entity<Setting>(modelBuilder);
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }

        private EntityTypeConfiguration<Tag> ConfigureTag(DbModelBuilder modelBuilder)
        {
            var configuration = Entity<Tag>(modelBuilder);
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }

        private EntityTypeConfiguration<Gallery> ConfigureGallery(DbModelBuilder modelBuilder)
        {
            var configuration = Entity<Gallery>(modelBuilder);
            configuration.HasMany(e => e.Pictures).WithMany();
            return ContentConfiguration(configuration);
        }

        private EntityTypeConfiguration<T> Entity<T>(DbModelBuilder modelBuilder) where T : class
        {
            return modelBuilder.Entity<T>();
        }

        private EntityTypeConfiguration<T> ConfigureContent<T>(DbModelBuilder modelBuilder) where T : Content
        {
            var configuration = Entity<T>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityTypeConfiguration<T> ContentConfiguration<T>(EntityTypeConfiguration<T> configuration) where T : Content
        {
            configuration.Property(e => e.Title).IsRequired();
            configuration.HasMany(e => e.Comments);
            configuration.HasMany(e => e.Tags).WithMany();
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }
    }
}