using FluentMetadata.Builder;
using Xunit;

namespace FluentMetadata.Specs.Builder
{
    public class ShouldBuilderTests
    {
        private readonly IShouldProperty<DummyClass, string> shouldBuilder;
        private readonly Metadata metadata;

        public ShouldBuilderTests()
        {
            metadata = new Metadata();
            shouldBuilder = new ShouldBuilder<DummyClass, string>(new PropertyMetadataBuilder<DummyClass, string>(metadata));
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldShowDisplay()
        {
            Assert.False(metadata.ShowDisplay.HasValue);
        }

        [Fact]
        public void ShouldBuilder_ShowInDisplay_ShouldShowDisplay()
        {
            shouldBuilder.ShowInDisplay();
            Assert.True(metadata.ShowDisplay.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_ShowInDisplay_ShouldNotShowDisplay()
        {
            shouldBuilder.Not.ShowInDisplay();
            Assert.False(metadata.ShowDisplay.Value);
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldShowEditor()
        {
            Assert.False(metadata.ShowEditor.HasValue);
        }

        [Fact]
        public void ShouldBuilder_ShowInEditor_ShouldShowEditor()
        {
            shouldBuilder.ShowInEditor();
            Assert.True(metadata.ShowEditor.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_ShowInEditor_ShouldNotShowEditor()
        {
            shouldBuilder.Not.ShowInEditor();
            Assert.False(metadata.ShowEditor.Value);
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldNotHideSurroundingHtml()
        {
            Assert.False(metadata.HideSurroundingHtml.HasValue);
        }

        [Fact]
        public void ShouldBuilder_HideSurroundingHtml_ShouldHideSurroundingHtml()
        {
            shouldBuilder.HideSurroundingHtml();
            Assert.True(metadata.HideSurroundingHtml.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_HideSurroundingHtml_ShouldNotHideSurroundingHtml()
        {
            shouldBuilder.Not.HideSurroundingHtml();
            Assert.False(metadata.HideSurroundingHtml.Value);
        }

        [Fact]
        public void ShouldBuilder_Ctor_ShouldNotHiddenInput()
        {
            Assert.False(metadata.Hidden.HasValue);
        }

        [Fact]
        public void ShouldBuilder_HiddenInput__ShouldHiddenInput()
        {
            shouldBuilder.HiddenInput();
            Assert.True(metadata.Hidden.Value);
        }

        [Fact]
        public void ShouldBuilder_HiddenInput_ShouldHideSurroundingHtml()
        {
            shouldBuilder.HiddenInput();
            Assert.True(metadata.HideSurroundingHtml.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_HiddenInput__ShouldNotHiddenInput()
        {
            shouldBuilder.Not.HiddenInput();
            Assert.False(metadata.Hidden.Value);
        }

        [Fact]
        public void ShouldBuilder_Not_iddenInput_ShouldNotHideSurroundingHtml()
        {
            shouldBuilder.Not.HiddenInput();
            Assert.False(metadata.HideSurroundingHtml.Value);
        }
    }
}