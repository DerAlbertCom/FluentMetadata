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
        readonly FluentMetadataConvention sut;

        public FluentMetaDataConventionSpecs()
        {
            FluentMetadataBuilder.Reset();
            FluentMetadataBuilder.ForAssemblyOfType<TestClassMetadata>();
            sut = new FluentMetadataConvention();
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

        [Fact]
        public void DoesNotThrowAnExceptionForNotFoundProperties()
        {
            var privateMapping = GetPropertyMapping<TestClass>(TestClass.Expressions.SomePrivateField);
            Exception exception = null;

            try
            {
                sut.Apply(new PropertyInstance(privateMapping));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Null(exception);
        }

        [Fact]
        public void AppliesNotNullToRequiredReferences()
        {
            var requiredMapping = GetPropertyMapping<TestClass>(t => t.RequiredReference);

            sut.Apply(new PropertyInstance(requiredMapping));

            Assert.True(requiredMapping.Columns.Single().NotNull);
        }

        [Fact]
        public void AppliesNullToNonRequiredReferences()
        {
            var optionalMapping = GetPropertyMapping<TestClass>(t => t.NullableReference);

            sut.Apply(new PropertyInstance(optionalMapping));

            Assert.False(optionalMapping.Columns.Single().NotNull);
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
        int somePrivateField = 42;

        public int Id { get; protected set; }
        public int? NullableNumber { get; set; }
        public string SomeString { get; set; }
        public Reference RequiredReference { get; set; }
        public Reference NullableReference { get; set; }

        public class Expressions
        {
            /* this represents a way to map private members using Fluent NHibernate;
             * see http://wiki.fluentnhibernate.org/Fluent_mapping_private_properties */
            public static Expression<Func<TestClass, object>> SomePrivateField = t => t.somePrivateField;
        }
    }

    public class Reference
    {
        public int Id { get; protected set; }
    }

    public class TestClassMetadata : ClassMetadata<TestClass>
    {
        public TestClassMetadata()
        {
            Property(t => t.Id)
                .Is.Required();
            Property(t => t.SomeString)
                .Length(42);
            Property(t => t.RequiredReference)
                .Is.Required();
        }
    }
}