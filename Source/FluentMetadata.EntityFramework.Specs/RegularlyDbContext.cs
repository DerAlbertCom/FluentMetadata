using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using FluentMetadata.EntityFramework.Specs.DomainObjects;

namespace FluentMetadata.EntityFramework.Specs
{
    public class RegularlyDbContext : DbContext
    {
        public RegularlyDbContext()
        {
            ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Entity<WebUser>(modelBuilder).MapSingleType();
            Entity<WebSite>(modelBuilder).MapSingleType();
            Entity<RemoteAction>(modelBuilder).HasKey(e => new {e.ObjectId, e.Action}).MapSingleType();

            ConfigureSetting(modelBuilder).MapSingleType();
            ConfigureTag(modelBuilder).MapSingleType();
            ConfigureMailTemplate(modelBuilder).MapSingleType();

            ConfigureContent<Content>(modelBuilder).MapHierarchy();
            ConfigureContentBase<ContentBase>(modelBuilder).MapHierarchy();
            ConfigureComment(modelBuilder).MapSingleType();
            ConfigurePicture(modelBuilder).MapSingleType();
            ConfigureGallery(modelBuilder).MapSingleType();
            ConfigureLayout(modelBuilder).MapSingleType();
            ConfigureNews(modelBuilder).MapSingleType();
            ConfigureArticle(modelBuilder).MapSingleType();
            MetadataConfiguration(modelBuilder.Configurations);
        }

        private EntityConfiguration<Layout> ConfigureLayout(ModelBuilder modelBuilder)
        {
            var configuration = Entity<Layout>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityConfiguration<News> ConfigureNews(ModelBuilder modelBuilder)
        {
            return ConfigureContentBase<News>(modelBuilder);
        }

        private EntityConfiguration<Article> ConfigureArticle(ModelBuilder modelBuilder)
        {
            return ConfigureContentBase<Article>(modelBuilder);
        }

        private EntityConfiguration<T> ConfigureContentBase<T>(ModelBuilder modelBuilder) where T : ContentBase
        {
            var configuration = Entity<T>(modelBuilder);
            //configuration.HasRequired(e => e.Layout);
            return ContentConfiguration(configuration);
        }

        private EntityConfiguration<MailTemplate> ConfigureMailTemplate(ModelBuilder modelBuilder)
        {
            var configuration = Entity<MailTemplate>(modelBuilder);
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }

        private EntityConfiguration<Picture> ConfigurePicture(ModelBuilder modelBuilder)
        {
            var configuration = Entity<Picture>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityConfiguration<Comment> ConfigureComment(ModelBuilder modelBuilder)
        {
            var configuration = Entity<Comment>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityConfiguration<Setting> ConfigureSetting(ModelBuilder modelBuilder)
        {
            var configuration = Entity<Setting>(modelBuilder);
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }

        private EntityConfiguration<Tag> ConfigureTag(ModelBuilder modelBuilder)
        {
            var configuration = Entity<Tag>(modelBuilder);
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }

        private EntityConfiguration<Gallery> ConfigureGallery(ModelBuilder modelBuilder)
        {
            var configuration = Entity<Gallery>(modelBuilder);
            configuration.HasMany(e => e.Pictures).WithMany();
            return ContentConfiguration(configuration);
        }

        private EntityConfiguration<T> Entity<T>(ModelBuilder modelBuilder)
        {
            return modelBuilder.Entity<T>();
        }

        private void MetadataConfiguration(IEnumerable<StructuralTypeConfiguration> configurations)
        {
            var convert = new EntityFrameworkAdapter();
            foreach (var configuration in configurations)
            {
                Type genericType = configuration.GetType().GetGenericArguments()[0];
                convert.MapProperties(genericType, configuration);
            }
        }

        private EntityConfiguration<T> ConfigureContent<T>(ModelBuilder modelBuilder) where T : Content
        {
            var configuration = Entity<T>(modelBuilder);
            return ContentConfiguration(configuration);
        }

        private EntityConfiguration<T> ContentConfiguration<T>(EntityConfiguration<T> configuration) where T : Content
        {
            configuration.HasMany(e => e.Comments);
            configuration.HasMany(e => e.Tags).WithMany();
            configuration.HasRequired(e => e.WebSite);
            return configuration;
        }
    }
}