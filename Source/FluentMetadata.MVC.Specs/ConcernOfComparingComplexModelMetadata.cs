﻿using System;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace FluentMetadata.MVC.Specs
{
    public abstract class ConcernOfComparingMetadata : InstanceContextSpecification<FluentMetadataProvider>,
                                                       IUseFixture<FluentMetadataFixture>
    {
        protected readonly ModelMetadataProvider OriginalProvider = new DataAnnotationsModelMetadataProvider();

        protected ModelMetadata Fluent;
        protected ModelMetadata Expected;

        protected override void Because()
        {
            CreateMetadata();
        }

        public void SetFixture(FluentMetadataFixture data)
        {
        }

        public abstract void CreateMetadata();


        [Observation]
        public void Equals_ModelMetadata_Properties_Count()
        {
            Console.WriteLine(Expected.Properties.ToList().Count);
            Assert.Equal(Expected.Properties.ToList().Count, Fluent.Properties.ToList().Count);
        }

        [Observation]
        public void Equals_PropertyName()
        {
            Console.WriteLine(Expected.PropertyName);
            Assert.Equal(Expected.PropertyName, Fluent.PropertyName);
        }

        [Observation]
        public void Equals_DisplayName()
        {
            Console.WriteLine(Expected.DisplayName);
            Assert.Equal(Expected.DisplayName, Fluent.DisplayName);
        }

        [Observation]
        public void Equals_Model()
        {
            Console.WriteLine(Expected.Model);
            Assert.Equal(Expected.Model, Fluent.Model);
        }

        [Observation]
        public void Equals_ContainerType()
        {
            Console.WriteLine(Expected.ContainerType);
            Assert.Equal(Expected.ContainerType, Fluent.ContainerType);
        }

        [Observation]
        public void Equals_ConvertEmptyStringToNull()
        {
            Console.WriteLine(Expected.ConvertEmptyStringToNull);
            Assert.Equal(Expected.ConvertEmptyStringToNull, Fluent.ConvertEmptyStringToNull);
        }

        [Observation]
        public void Equals_DataTypeName()
        {
            Console.WriteLine(Expected.DataTypeName);
            Assert.Equal(Expected.DataTypeName, Fluent.DataTypeName);
        }

        [Observation]
        public void Equals_DispalyFormatString()
        {
            Console.WriteLine(Expected.DisplayFormatString);
            Assert.Equal(Expected.DisplayFormatString, Fluent.DisplayFormatString);
        }

        [Observation]
        public void Equals_Description()
        {
            Console.WriteLine(Expected.Description);
            Assert.Equal(Expected.Description, Fluent.Description);
        }

        [Observation]
        public void Equals_EditFormatString()
        {
            Console.WriteLine(Expected.EditFormatString);
            Assert.Equal(Expected.EditFormatString, Fluent.EditFormatString);
        }

        [Observation]
        public void Equals_HideSurroundingHtml()
        {
            Console.WriteLine(Expected.HideSurroundingHtml);
            Assert.Equal(Expected.HideSurroundingHtml, Fluent.HideSurroundingHtml);
        }

    }
}