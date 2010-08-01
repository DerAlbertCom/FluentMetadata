using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class ExpressionGenerator_with_WebUser_Tests
    {
        private readonly ExpressionGenerator generator;

        public ExpressionGenerator_with_WebUser_Tests()
        {
            generator=new ExpressionGenerator();
        }

        [Fact]
        public void Generate_for_Username()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(WebUser),"Username"));
        }

        [Fact]
        public void Wont_Generate_for_Dummy()
        {
            Assert.Null(generator.CreateExpressionForProperty(typeof(WebUser), "Dummy"));
        }

        [Fact]
        public void Generate_for_BountCount()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(WebUser), "Confirmed"));
        }

        [Fact]
        public void Generate_for_LastLogin()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(WebUser), "LastLogin"));
        }

        [Fact]
        public void Generate_for_ConfirmationKey()
        {
            Assert.NotNull(generator.CreateExpressionForProperty(typeof(WebUser), "ConfirmationKey"));
        }

    }
}