using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class ExpressionGenerator_with_Content_Tests
    {
        private readonly ExpressionGenerator generator;

        public ExpressionGenerator_with_Content_Tests()
        {
            generator=new ExpressionGenerator();
        }


        [Fact]
        public void Generate_for_Id()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(Content), "Id"));
        }

        [Fact]
        public void Generate_for_Created()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(Content), "Created"));
        }

        [Fact]
        public void Generate_for_Title()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(Content), "Title"));
        }



        [Fact]
        public void Generate_for_Author()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(Content),"Author"));
        }

        [Fact]
        public void Generate_for_Comments()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(Content), "Comments"));
        }

        [Fact]
        public void Generate_for_WebSite()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(Content), "WebSite"));
        }

    }
}