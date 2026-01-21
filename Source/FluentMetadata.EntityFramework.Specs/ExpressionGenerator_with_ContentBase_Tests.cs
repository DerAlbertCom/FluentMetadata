using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class ExpressionGenerator_with_ContentBase_Tests
    {
        [Fact]
        public void Generate_for_Id()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Id"));
        }

        [Fact]
        public void Generate_for_Created()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Created"));
        }

        [Fact]
        public void Generate_for_Title()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Title"));
        }

        [Fact]
        public void Generate_for_Author()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Author"));
        }

        [Fact]
        public void Generate_for_Comments()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Comments"));
        }

        [Fact]
        public void Generate_for_Layout()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(ContentBase), "Layout"));
        }
    }
}