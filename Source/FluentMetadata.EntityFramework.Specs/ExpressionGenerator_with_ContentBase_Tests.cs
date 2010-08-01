using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class ExpressionGenerator_with_ContentBase_Tests
    {
        private readonly ExpressionGenerator generator;

        public ExpressionGenerator_with_ContentBase_Tests()
        {
            generator=new ExpressionGenerator();
        }


        [Fact]
        public void Generate_for_Id()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(ContentBase), "Id"));
        }

        [Fact]
        public void Generate_for_Created()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(ContentBase), "Created"));
        }

        [Fact]
        public void Generate_for_Title()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(ContentBase), "Title"));
        }

        [Fact]
        public void Generate_for_Author()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(ContentBase), "Author"));
        }

        [Fact]
        public void Generate_for_Comments()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(ContentBase), "Comments"));
        }

        [Fact]
        public void Generate_for_Layout()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(ContentBase), "Layout"));
        }
    }
}