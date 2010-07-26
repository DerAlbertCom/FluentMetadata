using Xunit;

namespace FluentMetadata.Specs
{
    public class ShouldBuilderTests
    {
        private readonly IShouldProperty<DummyClass, string> shouldBuilder;
        private readonly MetaData metaData;

        public ShouldBuilderTests()
        {
            metaData = new MetaData();
            shouldBuilder = new ShouldBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metaData));
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldShowDisplay()
        {
            Assert.False(metaData.ShowDisplay.HasValue);
        }

        [Fact]
        public void ShouldBuilder_ShowInDisplay_ShouldShowDisplay()
        {
            shouldBuilder.ShowInDisplay();
            Assert.True(metaData.ShowDisplay.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_ShowInDisplay_ShouldNotShowDisplay()
        {
            shouldBuilder.Not.ShowInDisplay();
            Assert.False(metaData.ShowDisplay.Value);
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldShowEditor()
        {
            Assert.False(metaData.ShowEditor.HasValue);
        }

        [Fact]
        public void ShouldBuilder_ShowInEditor_ShouldShowEditor()
        {
            shouldBuilder.ShowInEditor();
            Assert.True(metaData.ShowEditor.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_ShowInEditor_ShouldNotShowEditor()
        {
            shouldBuilder.Not.ShowInEditor();
            Assert.False(metaData.ShowEditor.Value);
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldNotHideSurroundingHtml()
        {
            Assert.False(metaData.HideSurroundingHtml.HasValue);
        }

        [Fact]
        public void ShouldBuilder_HideSurroundingHtml_ShouldHideSurroundingHtml()
        {
            shouldBuilder.HideSurroundingHtml();
            Assert.True(metaData.HideSurroundingHtml.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_HideSurroundingHtml_ShouldNotHideSurroundingHtml()
        {
            shouldBuilder.Not.HideSurroundingHtml();
            Assert.False(metaData.HideSurroundingHtml.Value);
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldNotHiddenInput()
        {
            Assert.False(metaData.Hidden.HasValue);
        }

        [Fact]
        public void ShouldBuilder_HiddenInput__ShouldHiddenInput()
        {
            shouldBuilder.HiddenInput();
            Assert.True(metaData.Hidden.Value);
        }

        [Fact]
        public void ShouldBuilder_HiddenInput_ShouldHideSurroundingHtml()
        {
            shouldBuilder.HiddenInput();
            Assert.True(metaData.HideSurroundingHtml.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_HiddenInput__ShouldNotHiddenInput()
        {
            shouldBuilder.Not.HiddenInput();
            Assert.False(metaData.Hidden.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_iddenInput_ShouldNotHideSurroundingHtml()
        {
            shouldBuilder.Not.HiddenInput();
            Assert.False(metaData.HideSurroundingHtml.Value);
        }
    }
}