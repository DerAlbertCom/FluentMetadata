using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class ExpressionGenerator_with_Content_Tests
    {
        [Fact]
        public void Generate_for_Id()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Id"));
        }

        [Fact]
        public void Generate_for_Created()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Created"));
        }

        [Fact]
        public void Generate_for_Title()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Title"));
        }

        [Fact]
        public void Generate_for_Author()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Author"));
        }

        [Fact]
        public void Generate_for_Comments()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "Comments"));
        }

        [Fact]
        public void Generate_for_WebSite()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(Content), "WebSite"));
        }
    }
}