using FluentMetadata.EntityFramework.Internal;
using FluentMetadata.EntityFramework.Specs.DomainObjects;
using Xunit;

namespace FluentMetadata.EntityFramework.Specs
{
    public class ExpressionGenerator_with_WebUser_Tests
    {
        [Fact]
        public void Generate_for_Username()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "Username"));
        }

        [Fact]
        public void Wont_Generate_for_Dummy()
        {
            Assert.Null(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "Dummy"));
        }

        [Fact]
        public void Generate_for_BountCount()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "Confirmed"));
        }

        [Fact]
        public void Generate_for_LastLogin()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "LastLogin"));
        }

        [Fact]
        public void Generate_for_ConfirmationKey()
        {
            Assert.NotNull(ExpressionGenerator.CreateExpressionForProperty(typeof(WebUser), "ConfirmationKey"));
        }
    }
}