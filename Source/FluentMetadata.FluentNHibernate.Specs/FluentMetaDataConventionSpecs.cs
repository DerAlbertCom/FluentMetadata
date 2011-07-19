using System;
using System.Linq;
using System.Linq.Expressions;
using FluentMetadata.FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils.Reflection;
using Xunit;

namespace FluentMetadata.FluentNHibernate.Specs
{
    public class FluentMetaDataConventionSpecs
    {
        readonly FluentMetaDataConvention sut;

        public FluentMetaDataConventionSpecs()
        {
            FluentMetadataBuilder.Reset();
            FluentMetadataBuilder.ForAssemblyOfType<TestClassMetadata>();
            sut = new FluentMetaDataConvention();
        }

        [Fact]
        public void AppliesNotNullToRequiredProperties()
        {
            var idMapping = GetPropertyMapping<TestClass>(t => t.Id);

            sut.Apply(new PropertyInstance(idMapping));

            Assert.True(idMapping.Columns.Single().NotNull);
        }

        [Fact]
        public void AppliesNullToNonRequiredProperties()
        {
            var optionalMapping = GetPropertyMapping<TestClass>(t => t.NullableNumber);

            sut.Apply(new PropertyInstance(optionalMapping));

            Assert.False(optionalMapping.Columns.Single().NotNull);
        }

        [Fact]
        public void AppliesMaximumStringLengthToProperties()
        {
            var optionalMapping = GetPropertyMapping<TestClass>(t => t.SomeString);

            sut.Apply(new PropertyInstance(optionalMapping));

            Assert.Equal(42, optionalMapping.Columns.Single().Length);
        }

        static PropertyMapping GetPropertyMapping<T>(Expression<Func<T, object>> propertyExpression)
        {
            var propertyMapping = new PropertyMapping
            {
                Member = ReflectionHelper.GetMember<T>(propertyExpression),
                ContainingEntityType = typeof(T)
            };
            propertyMapping.AddDefaultColumn(new ColumnMapping());
            return propertyMapping;
        }
    }

    public class TestClass
    {
        public int Id { get; protected set; }
        public int? NullableNumber { get; set; }
        public string SomeString { get; set; }
    }

    public class TestClassMetadata : ClassMetadata<TestClass>
    {
        public TestClassMetadata()
        {
            Property(t => t.Id)
                .Is.Required();
            Property(t => t.SomeString)
                .Length(42);
        }
    }
}